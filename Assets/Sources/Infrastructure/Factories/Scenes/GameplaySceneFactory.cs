using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using Sources.Constants;
using Sources.Controllers.Scenes;
using Sources.Controllers.Scenes.Gameplay;
using Sources.Domain.Constructs;
using Sources.Domain.Credits;
using Sources.Domain.Systems.Aggressive;
using Sources.Domain.Systems.EnemySpawn;
using Sources.Domain.Systems.Progresses;
using Sources.Domain.Systems.Upgrades;
using Sources.Domain.Weapons;
using Sources.Domain.Zombies;
using Sources.Infrastructure.Assessors;
using Sources.Infrastructure.Factories.Controllers.Bullets;
using Sources.Infrastructure.Factories.Controllers.Constructions;
using Sources.Infrastructure.Factories.Controllers.Constructs;
using Sources.Infrastructure.Factories.Controllers.Systems;
using Sources.Infrastructure.Factories.Controllers.Turrets;
using Sources.Infrastructure.Factories.Controllers.Weapons;
using Sources.Infrastructure.Factories.Controllers.Zombies;
using Sources.Infrastructure.Factories.Domain.Bullets;
using Sources.Infrastructure.Factories.Domain.Systems;
using Sources.Infrastructure.Factories.Domain.Turrets;
using Sources.Infrastructure.Factories.Domain.Weapons;
using Sources.Infrastructure.Factories.Domain.Zombies;
using Sources.Infrastructure.Factories.Handlers;
using Sources.Infrastructure.Factories.Presentation.Systems;
using Sources.Infrastructure.Factories.Presentation.Ui;
using Sources.Infrastructure.Factories.Presentation.Ui.Systems;
using Sources.Infrastructure.Factories.Presentation.Views;
using Sources.Infrastructure.Handlers.Pointers;
using Sources.Infrastructure.Repositories;
using Sources.Infrastructure.Resource;
using Sources.Infrastructure.Services.Cameras;
using Sources.Infrastructure.Services.Payments;
using Sources.Infrastructure.Services.Pointers;
using Sources.Infrastructure.Services.Raycasts;
using Sources.Infrastructure.Services.Tilemaps;
using Sources.Infrastructure.Services.Times;
using Sources.InfrastructureInterfaces.Factories.Controllers;
using Sources.InfrastructureInterfaces.Factories.Scenes;
using Sources.Presentation.Previews.Constructions;
using Sources.Presentation.Ui;
using Sources.Presentation.Ui.Constructs;
using Sources.Presentation.Ui.Systems.Aggressive;
using Sources.Presentation.Ui.Systems.Progresses;
using Sources.Presentation.Ui.Systems.Upgrades;
using Sources.Presentation.Views.Cameras;
using Sources.Presentation.Views.Systems.Spawn;
using Sources.Presentation.Views.Systems.TargetTrackers;
using Sources.Presentation.Views.Turrets;
using Sources.Presentation.Views.Weapons;
using Sources.Presentation.Views.Zombies;
using Sources.PresentationInterfaces.Views.Enemies;
using UnityEngine;
using UnityEngine.Tilemaps;
using Object = UnityEngine.Object;

namespace Sources.Infrastructure.Factories.Scenes
{
    public class GameplaySceneFactory : ISceneFactory
    {
        public async UniTask<IScene> Create(object payload)
        {
            ResourceService resourceService = new ResourceService();

            Money money = new Money(220);

            await LoadResourcesAsync(resourceService);

            #region Resources

            AggressiveLevelCollection aggressiveLevelCollection =
                resourceService.Load<AggressiveLevelCollection>("Fabs/Systems/Aggressive/AggressiveLevelCollectionFab");

            ConstructButtonCollection constructButtonCollection =
                resourceService.Load<ConstructButtonCollection>("Fabs/Buttons/Constructs/CollectionFab");

            EnemySpawnWaveCollectionFab enemySpawnWaveCollectionFab =
                resourceService.Load<EnemySpawnWaveCollectionFab>("Fabs/Systems/Spawn/EnemySpawnWaveCollectionFab");

            Dictionary<string, TurretConstructionPreview> turretConstructionViews =
                new Dictionary<string, TurretConstructionPreview>()
                {
                    [nameof(LaserGun)] = Object.Instantiate(
                        resourceService.Load<TurretConstructionPreview>("Previews/Weapons/LaserGunPreview")
                    ),
                    [nameof(DoubleLaserGun)] = Object.Instantiate(
                        resourceService.Load<TurretConstructionPreview>("Previews/Weapons/DoubleLaserGunPreview")
                    ),
                    [nameof(DoubleLaserTwiceGun)] = Object.Instantiate(
                        resourceService.Load<TurretConstructionPreview>("Previews/Weapons/DoubleLaserTwiceGunPreview")
                    ),
                    [nameof(MiniTwiceGun)] = Object.Instantiate(
                        resourceService.Load<TurretConstructionPreview>("Previews/Weapons/MiniTwiceGunPreview")
                    ),
                    [nameof(RocketTwiceGun)] = Object.Instantiate(
                        resourceService.Load<TurretConstructionPreview>("Previews/Weapons/RocketTwiceGunPreview")
                    ),
                    [nameof(SingleGun)] = Object.Instantiate(
                        resourceService.Load<TurretConstructionPreview>("Previews/Weapons/SingleGunPreview")
                    ),
                    [nameof(DoubleGun)] = Object.Instantiate(
                        resourceService.Load<TurretConstructionPreview>("Previews/Weapons/DoubleGunPreview")
                    ),
                    [nameof(TripleGun)] = Object.Instantiate(
                        resourceService.Load<TurretConstructionPreview>("Previews/Weapons/TripleGunPreview")
                    ),
                    [nameof(QuadGun)] = Object.Instantiate(
                        resourceService.Load<TurretConstructionPreview>("Previews/Weapons/QuadGunPreview")
                    ),
                };

            UpgradeSystemUiContainer upgradeSystemUiContainer =
                Object.Instantiate(
                    resourceService.Load<UpgradeSystemUiContainer>("Ui/Systems/UpgradeSystemUiContainer")
                );

            #endregion

            #region Scene Dependencies

            Tilemap tilemap = Object.FindObjectOfType<Tilemap>();
            Hud hud = Object.FindObjectOfType<Hud>();
            GameplayCamera gameplayCamera = Object.FindObjectOfType<GameplayCamera>();
            BaseView baseView = Object.FindObjectOfType<BaseView>();
            SpawnSystemView spawnSystemView = Object.FindObjectOfType<SpawnSystemView>();

            #endregion

            #region Repositories

            EnemyRepository enemyRepository = new EnemyRepository();
            TileRepository tileRepository = new TileRepository(tilemap);

            #endregion

            #region Assessors

            EnemyAssessor enemyDeathAggressiveAssessor = new EnemyAssessor(
                new Dictionary<Type, int>()
                {
                    [typeof(Zombie)] = 50,
                }
            );

            EnemyAssessor enemyDeathProgressAssessor = new EnemyAssessor(
                new Dictionary<Type, int>()
                {
                    [typeof(Zombie)] = 100,
                }
            );

            #endregion

            #region Systems

            AggressiveSystem aggressiveSystem = new AggressiveSystemFactory().Create(aggressiveLevelCollection);
            ProgressSystem progressSystem = new ProgressSystem();
            UpgradeSystem laserUpgradeSystem = new UpgradeSystem();
            UpgradeSystem bulletUpgradeSystem = new UpgradeSystem();
            UpgradeSystem rocketUpgradeSystem = new UpgradeSystem();
            TargetTrackerSystem targetTrackerSystem =
                new TargetTrackerSystem(2000, Layers.Enemy, Layers.Obstacle);

            #endregion

            #region Services

            PaymentService paymentService = new PaymentService(money);
            RaycastService raycastService = new RaycastService(gameplayCamera, Layers.GameplayGrid);
            TilemapService tilemapService = new TilemapService(tilemap);
            GameplayCameraService gameplayCameraService = new GameplayCameraService(gameplayCamera);
            PointerService pointerService = new PointerService();
            TimeService timeService = new TimeService();

            #endregion

            #region Domain Factories

            ZombieFactory zombieFactory = new ZombieFactory(enemyRepository, aggressiveSystem);

            LaserFactory laserFactory = new LaserFactory(laserUpgradeSystem);
            BulletFactory bulletFactory = new BulletFactory(bulletUpgradeSystem);
            RocketFactory rocketFactory = new RocketFactory(rocketUpgradeSystem);

            LaserGunFactory laserGunFactory = new LaserGunFactory(resourceService, laserFactory, timeService, laserUpgradeSystem);
            DoubleLaserGunFactory doubleLaserGunFactory =
                new DoubleLaserGunFactory(resourceService, laserFactory, timeService, laserUpgradeSystem);
            DoubleLaserTwiceGunFactory doubleLaserTwiceGunFactory =
                new DoubleLaserTwiceGunFactory(resourceService, laserFactory, timeService, laserUpgradeSystem);
            MiniTwiceGunFactory miniTwiceGunFactory =
                new MiniTwiceGunFactory(resourceService, bulletFactory, timeService, bulletUpgradeSystem);
            RocketTwiceGunFactory rocketTwiceGunFactory =
                new RocketTwiceGunFactory(resourceService, rocketFactory, timeService, rocketUpgradeSystem);

            SingleGunFactory singleGunFactory = new SingleGunFactory(resourceService, bulletFactory, timeService, bulletUpgradeSystem);
            DoubleGunFactory doubleGunFactory = new DoubleGunFactory(resourceService, bulletFactory, timeService, bulletUpgradeSystem);
            TripleGunFactory tripleGunFactory = new TripleGunFactory(resourceService, bulletFactory, timeService, bulletUpgradeSystem);
            QuadGunFactory quadGunFactory = new QuadGunFactory(resourceService, bulletFactory, timeService, bulletUpgradeSystem);

            TurretFactory turretFactory = new TurretFactory(tileRepository);

            #endregion

            #region StateMachine Factories

            WeaponStateMachineFactory weaponStateMachineFactory =
                new WeaponStateMachineFactory(
                    new Dictionary<Type, IWeaponStateMachineFactory>()
                    {
                        [typeof(LaserGun)] = new LaserGunStateMachineFactory(),
                        [typeof(DoubleLaserGun)] = new DoubleLaserGunStateMachineFactory(),
                        [typeof(DoubleLaserTwiceGun)] = new DoubleLaserTwiceGunStateMachineFactory(),
                        [typeof(MiniTwiceGun)] = new MiniTwiceGunStateMachineFactory(),
                        [typeof(RocketTwiceGun)] = new RocketTwiceGunStateMachineFactory(),
                        [typeof(SingleGun)] = new SingleGunStateMachineFactory(),
                        [typeof(DoubleGun)] = new DoubleGunStateMachineFactory(),
                        [typeof(TripleGun)] = new TripleGunStateMachineFactory(),
                        [typeof(QuadGun)] = new QuadGunStateMachineFactory(),
                    }
                );

            ZombieStateMachineFactory zombieStateMachineFactory = new ZombieStateMachineFactory(
                progressSystem,
                aggressiveSystem,
                enemyRepository,
                enemyDeathAggressiveAssessor,
                enemyDeathProgressAssessor
            );

            #endregion

            #region Presenter Factories

            BulletPresenterFactory bulletPresenterFactory = new BulletPresenterFactory();
            TurretPresenterFactory turretPresenterFactory = new TurretPresenterFactory();
            MovementSystemPresenterFactory movementSystemPresenterFactory = new MovementSystemPresenterFactory();
            DamageableSystemPresenterFactory damageableSystemPresenterFactory = new DamageableSystemPresenterFactory();

            AggressiveSystemPresenterFactory aggressiveSystemPresenterFactory =
                new AggressiveSystemPresenterFactory(enemyRepository);

            ProgressSystemPresenterFactory progressSystemPresenterFactory = new ProgressSystemPresenterFactory();

            UpgradeSystemPresenterFactory upgradeSystemPresenterFactory = new UpgradeSystemPresenterFactory();

            #endregion

            #region Pointer Handler Factories

            TilemapUntouchablePointerHandlerFactory tilemapUntouchablePointerHandlerFactory =
                new TilemapUntouchablePointerHandlerFactory(
                    raycastService,
                    tilemapService
                );

            #endregion

            #region View Factories

            BulletViewFactory bulletViewFactory = new BulletViewFactory(bulletPresenterFactory);

            WeaponViewFactory weaponViewFactory = new WeaponViewFactory(
                resourceService,
                weaponStateMachineFactory, 
                bulletViewFactory, 
                targetTrackerSystem
            );

            TurretViewFactory turretViewFactory = new TurretViewFactory(resourceService, turretPresenterFactory, weaponViewFactory);

            MovementSystemViewFactory movementSystemViewFactory =
                new MovementSystemViewFactory(movementSystemPresenterFactory);

            DamageableSystemViewFactory damageableSystemViewFactory =
                new DamageableSystemViewFactory(damageableSystemPresenterFactory);

            ZombieViewFactory zombieViewFactory = new ZombieViewFactory(
                resourceService,
                zombieStateMachineFactory, movementSystemViewFactory,
                damageableSystemViewFactory, baseView
            );

            EnemyViewFactory enemyViewFactory = new EnemyViewFactory(
                new Dictionary<string, Func<Vector3, IEnemyView>>()
                {
                    ["Zombie"] = position => zombieViewFactory.Create(zombieFactory.Create(), position),
                }
            );

            #endregion

            #region Constuction Presenter And View Factories

            TurretConstructionPresenterFactory turretConstructionPresenterFactory =
                new TurretConstructionPresenterFactory(tilemapService, turretViewFactory, turretFactory);

            TurretConstructionViewFactory turretConstructionViewFactory =
                new TurretConstructionViewFactory(turretConstructionPresenterFactory, turretConstructionViews);

            ConstructButtonPresenterFactory constructButtonPresenterFactory =
                new ConstructButtonPresenterFactory(
                    turretConstructionViewFactory,
                    tilemapUntouchablePointerHandlerFactory,
                    tileRepository,
                    paymentService,
                    pointerService,
                    tilemapService,
                    gameplayCamera,
                    laserGunFactory,
                    doubleLaserGunFactory,
                    doubleLaserTwiceGunFactory,
                    miniTwiceGunFactory,
                    rocketTwiceGunFactory,
                    singleGunFactory,
                    doubleGunFactory,
                    tripleGunFactory,
                    quadGunFactory
                );

            ConstructButtonUiFactory constructButtonUiFactory =
                new ConstructButtonUiFactory(resourceService,constructButtonPresenterFactory);

            #endregion

            #region Spawn System Factories

            SpawnSystemPresenterFactory spawnSystemPresenterFactory = new SpawnSystemPresenterFactory(enemyViewFactory);
            SpawnSystemViewFactory spawnSystemViewFactory = new SpawnSystemViewFactory(spawnSystemPresenterFactory);

            #endregion

            #region Ui Factories

            MoneyUiFactory moneyUiFactory = new MoneyUiFactory(resourceService);

            AggressiveSystemUiFactory aggressiveSystemUiFactory =
                new AggressiveSystemUiFactory(resourceService, aggressiveSystemPresenterFactory);

            ProgressSystemUiFactory progressSystemUiFactory =
                new ProgressSystemUiFactory(resourceService, progressSystemPresenterFactory);

            UpgradeSystemUiFactory upgradeSystemUiFactory = new UpgradeSystemUiFactory(upgradeSystemPresenterFactory);

            #endregion

            pointerService.RegisterHandler(1, new CameraRotationPointerHandler(gameplayCameraService));

            spawnSystemViewFactory.Create(spawnSystemView, enemySpawnWaveCollectionFab);
            turretConstructionViews.Values.ToList().ForEach(view => view.Hide());

            upgradeSystemUiFactory.Create(upgradeSystemUiContainer.Laser, laserUpgradeSystem);
            //      upgradeSystemUiFactory.Create(upgradeSystemUiContainer.Bullet, bulletUpgradeSystem);
            //    upgradeSystemUiFactory.Create(upgradeSystemUiContainer.Rocket, rocketUpgradeSystem);

            hud.TopLeft.AddChild(progressSystemUiFactory.Create(progressSystem));
            hud.TopCenter.AddChild(moneyUiFactory.Create(money));
            hud.TopRight.AddChild(aggressiveSystemUiFactory.Create(aggressiveSystem));

            hud.MiddleLeft.AddChild(upgradeSystemUiContainer);


            foreach (ConstructButton constructButton in constructButtonCollection.ConstructButtons)
                hud.Footer.AddChild(constructButtonUiFactory.Create(constructButton));

            return new GameplayScene(resourceService, pointerService, gameplayCameraService, aggressiveSystem);
        }

        private async UniTask LoadResourcesAsync(ResourceService resourceService) =>
            await resourceService
                .Register<AggressiveLevelCollection>("Fabs/Systems/Aggressive/AggressiveLevelCollectionFab")
                .Register<ConstructButtonCollection>("Fabs/Buttons/Constructs/CollectionFab")
                .Register<EnemySpawnWaveCollectionFab>("Fabs/Systems/Spawn/EnemySpawnWaveCollectionFab")
                .Register<UpgradeSystemUiContainer>("Ui/Systems/UpgradeSystemUiContainer")
                .Register<TurretView>("Views/Turrets/TurretView")
                .Register<ZombieView>("Views/Zombies/ZombieView")
                .Register<ConstructButtonUi>("Ui/Buttons/Constructs/ConstructButtonUi")
                .Register<TextUi>("Ui/Credits/MoneyUi")
                .Register<AggressiveSystemUi>("Ui/Systems/AggressiveSystemUi")
                .Register<ProgressSystemUi>("Ui/Systems/ProgressSystemUi")
                .RegisterInterfaceImplementationsByType<TurretConstructionPreview, IWeapon>("Previews/Weapons/{0}Preview")
                .RegisterInterfaceImplementationsByType<CompositeWeaponView, IWeapon>("Views/Weapons/{0}View")
                .RegisterInterfaceImplementationsByType<WeaponFab, IWeapon>("Fabs/Weapons/{0}Fab")
                .LoadAllAsync();
    }
}