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
using Sources.Domain.Systems.Spawn;
using Sources.Domain.Systems.Upgrades;
using Sources.Domain.Weapons;
using Sources.Domain.Weapons.Bullets;
using Sources.Domain.Weapons.Lasers;
using Sources.Domain.Weapons.Rockets;
using Sources.Domain.Zombies;
using Sources.Infrastructure.Assessors;
using Sources.Infrastructure.Factories.Controllers.Audio;
using Sources.Infrastructure.Factories.Controllers.Constructions;
using Sources.Infrastructure.Factories.Controllers.Constructs;
using Sources.Infrastructure.Factories.Controllers.HealthPoints;
using Sources.Infrastructure.Factories.Controllers.Projectiles;
using Sources.Infrastructure.Factories.Controllers.Systems;
using Sources.Infrastructure.Factories.Controllers.Systems.PathDraw;
using Sources.Infrastructure.Factories.Controllers.Turrets;
using Sources.Infrastructure.Factories.Controllers.Weapons;
using Sources.Infrastructure.Factories.Controllers.Weapons.Bullets;
using Sources.Infrastructure.Factories.Controllers.Weapons.Lasers;
using Sources.Infrastructure.Factories.Controllers.Weapons.Rockets;
using Sources.Infrastructure.Factories.Controllers.Zombies;
using Sources.Infrastructure.Factories.Domain.Projectiles;
using Sources.Infrastructure.Factories.Domain.Systems;
using Sources.Infrastructure.Factories.Domain.Turrets;
using Sources.Infrastructure.Factories.Domain.Weapons.Bullets;
using Sources.Infrastructure.Factories.Domain.Weapons.Lasers;
using Sources.Infrastructure.Factories.Domain.Weapons.Rockets;
using Sources.Infrastructure.Factories.Domain.Zombies;
using Sources.Infrastructure.Factories.Handlers;
using Sources.Infrastructure.Factories.Presentation.Audio;
using Sources.Infrastructure.Factories.Presentation.Systems;
using Sources.Infrastructure.Factories.Presentation.Systems.PathDraw;
using Sources.Infrastructure.Factories.Presentation.Ui;
using Sources.Infrastructure.Factories.Presentation.Ui.Systems;
using Sources.Infrastructure.Factories.Presentation.Views;
using Sources.Infrastructure.Factories.Presentation.Views.Bullets;
using Sources.Infrastructure.Handlers.Pointers;
using Sources.Infrastructure.Repositories;
using Sources.Infrastructure.Resource;
using Sources.Infrastructure.Services.Cameras;
using Sources.Infrastructure.Services.NavMeshes;
using Sources.Infrastructure.Services.Payments;
using Sources.Infrastructure.Services.Pointers;
using Sources.Infrastructure.Services.Raycasts;
using Sources.Infrastructure.Services.Tilemaps;
using Sources.Infrastructure.Services.Times;
using Sources.InfrastructureInterfaces.Factories.Controllers;
using Sources.InfrastructureInterfaces.Factories.Scenes;
using Sources.InfrastructureInterfaces.Services.Scenes;
using Sources.Presentation.Audio;
using Sources.Presentation.Previews.Constructions;
using Sources.Presentation.Ui;
using Sources.Presentation.Ui.Constructs;
using Sources.Presentation.Ui.Systems.Aggressive;
using Sources.Presentation.Ui.Systems.Progresses;
using Sources.Presentation.Ui.Systems.Spawn;
using Sources.Presentation.Ui.Systems.Upgrades;
using Sources.Presentation.Views.Bullets;
using Sources.Presentation.Views.Cameras;
using Sources.Presentation.Views.Systems.PathDraw;
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
        private readonly ISceneChangeService _sceneChangeService;

        public GameplaySceneFactory(ISceneChangeService sceneChangeService) =>
            _sceneChangeService = sceneChangeService;

        public async UniTask<IScene> Create(object payload)
        {
            ResourceService resourceService = new ResourceService();

            Money money = new Money(22220);

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

            EnemyAssessor enemyRewardAssessor = new EnemyAssessor(
                new Dictionary<Type, int>()
                {
                    [typeof(Zombie)] = 1,
                }
            );

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

            SpawnSystem spawnSystem = new SpawnSystem();

            #endregion

            #region Services

            PaymentService paymentService = new PaymentService(money);
            ScreenRaycastService screenRaycastService = new ScreenRaycastService(gameplayCamera, Layers.GameplayGrid);
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

            LaserGunFactory laserGunFactory = new LaserGunFactory(resourceService, timeService, laserUpgradeSystem);

            DoubleLaserGunFactory doubleLaserGunFactory =
                new DoubleLaserGunFactory(resourceService, timeService, laserUpgradeSystem);
            DoubleLaserTwiceGunFactory doubleLaserTwiceGunFactory =
                new DoubleLaserTwiceGunFactory(resourceService, timeService, laserUpgradeSystem);
            MiniTwiceGunFactory miniTwiceGunFactory =
                new MiniTwiceGunFactory(resourceService, timeService, bulletUpgradeSystem);
            RocketTwiceGunFactory rocketTwiceGunFactory =
                new RocketTwiceGunFactory(resourceService, timeService, rocketUpgradeSystem);

            SingleGunFactory singleGunFactory = new SingleGunFactory(resourceService, timeService, bulletUpgradeSystem);
            DoubleGunFactory doubleGunFactory = new DoubleGunFactory(resourceService, timeService, bulletUpgradeSystem);
            TripleGunFactory tripleGunFactory = new TripleGunFactory(resourceService, timeService, bulletUpgradeSystem);
            QuadGunFactory quadGunFactory = new QuadGunFactory(resourceService, timeService, bulletUpgradeSystem);
            TurretFactory turretFactory = new TurretFactory(tileRepository);

            #endregion

            AudioMixerView audioMixer = new AudioMixerViewFactory(new AudioMixerPresenterFactory()).Create();
            
            #region StateMachine Factories
            
            LaserViewFactory laserViewFactory = new LaserViewFactory(new LaserPresenterFactory(audioMixer), resourceService);
            BulletViewFactory bulletViewFactory = new BulletViewFactory(new BulletPresenterFactory(audioMixer), resourceService);
            RocketViewFactory rocketViewFactory = new RocketViewFactory(new RocketPresenterFactory(audioMixer), resourceService);

            WeaponStateMachineFactory weaponStateMachineFactory =
                new WeaponStateMachineFactory(
                    new Dictionary<Type, IWeaponStateMachineFactory>()
                    {
                        [typeof(LaserGun)] = new LaserWeaponGunStateMachineFactory(laserViewFactory, laserFactory),
                        [typeof(DoubleLaserGun)] = new DoubleLaserWeaponGunStateMachineFactory(laserViewFactory, laserFactory),
                        [typeof(DoubleLaserTwiceGun)] = new DoubleLaserWeaponTwiceGunStateMachineFactory(laserViewFactory, laserFactory),
                        [typeof(MiniTwiceGun)] = new MiniTwiceGunStateMachineFactory(bulletViewFactory, bulletFactory),
                        [typeof(SingleGun)] = new SingleGunStateMachineFactory(bulletViewFactory, bulletFactory),
                        [typeof(DoubleGun)] = new DoubleGunStateMachineFactory(bulletViewFactory, bulletFactory),
                        [typeof(TripleGun)] = new TripleGunStateMachineFactory(bulletViewFactory, bulletFactory),
                        [typeof(QuadGun)] = new QuadGunStateMachineFactory(bulletViewFactory, bulletFactory),
                        [typeof(RocketTwiceGun)] = new RocketTwiceGunStateMachineFactory(rocketViewFactory, rocketFactory),
                    }
                );

            ZombieAfterLifeStateMachineFactory zombieAfterLifeStateMachineFactory =
                new ZombieAfterLifeStateMachineFactory();

            ZombieStateMachineFactory zombieStateMachineFactory = new ZombieStateMachineFactory(
                zombieAfterLifeStateMachineFactory,
                progressSystem,
                aggressiveSystem,
                enemyRepository,
                enemyDeathAggressiveAssessor,
                enemyDeathProgressAssessor,
                enemyRewardAssessor,
                paymentService
            );

            #endregion

            #region Presenter Factories

            BulletPresenterFactory bulletPresenterFactory = new BulletPresenterFactory(audioMixer);
            RocketPresenterFactory rocketPresenterFactory = new RocketPresenterFactory(audioMixer);
            TurretPresenterFactory turretPresenterFactory = new TurretPresenterFactory();
            MovementSystemPresenterFactory movementSystemPresenterFactory = new MovementSystemPresenterFactory();
            DamageableSystemPresenterFactory damageableSystemPresenterFactory = new DamageableSystemPresenterFactory();

            AggressiveSystemPresenterFactory aggressiveSystemPresenterFactory =
                new AggressiveSystemPresenterFactory(enemyRepository);

            ProgressSystemPresenterFactory progressSystemPresenterFactory = new ProgressSystemPresenterFactory();
            UpgradeSystemPresenterFactory upgradeSystemPresenterFactory = new UpgradeSystemPresenterFactory();
            HealthPresenterFactory healthPresenterFactory = new HealthPresenterFactory();
            PathDrawSystemPresenterFactory pathDrawSystemPresenterFactory = new PathDrawSystemPresenterFactory();
            PathDrawSystemPointPresenterFactory pathDrawSystemPointPresenterFactory =
                new PathDrawSystemPointPresenterFactory();

            #endregion

            #region Pointer Handler Factories

            TilemapUntouchablePointerHandlerFactory tilemapUntouchablePointerHandlerFactory =
                new TilemapUntouchablePointerHandlerFactory(
                    screenRaycastService,
                    tilemapService
                );

            #endregion

            #region View Factories


            WeaponViewFactory weaponViewFactory = new WeaponViewFactory(
                resourceService,
                weaponStateMachineFactory,
                targetTrackerSystem
            );

            HealthViewFactory healthViewFactory = new HealthViewFactory(healthPresenterFactory);

            TurretViewFactory turretViewFactory = new TurretViewFactory(
                resourceService, turretPresenterFactory, weaponViewFactory
            );

            MovementSystemViewFactory movementSystemViewFactory =
                new MovementSystemViewFactory(movementSystemPresenterFactory);

            DamageableSystemViewFactory damageableSystemViewFactory =
                new DamageableSystemViewFactory(damageableSystemPresenterFactory);

            ZombieViewFactory zombieViewFactory = new ZombieViewFactory(
                healthViewFactory,
                resourceService,
                zombieStateMachineFactory,
                movementSystemViewFactory,
                damageableSystemViewFactory,
                gameplayCamera,
                baseView
            );

            EnemyViewFactory enemyViewFactory = new EnemyViewFactory(
                new Dictionary<string, Func<Vector3, IEnemyView>>()
                {
                    ["Zombie"] = position => zombieViewFactory.Create(zombieFactory.Create(), position),
                }
            );

            PathDrawSystemPointViewFactory pathDrawSystemPointViewFactory =
                new PathDrawSystemPointViewFactory(resourceService, pathDrawSystemPointPresenterFactory);

            PathDrawSystemViewFactory pathDrawSystemViewFactory =
                new PathDrawSystemViewFactory(
                    resourceService, pathDrawSystemPresenterFactory, pathDrawSystemPointViewFactory
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
                new ConstructButtonUiFactory(resourceService, constructButtonPresenterFactory);

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

            #region Path Draw System Presenter And View Factories

            NavMeshService navMeshService = new NavMeshService();

            #endregion

            pointerService.RegisterHandler(1, new CameraRotationPointerHandler(gameplayCameraService));

            SpawnNotifierUi spawnNotifierUi =
                Object.Instantiate(Resources.Load<SpawnNotifierUi>("Ui/Systems/Spawn/SpawnNotifierUi"));

            spawnSystemViewFactory.Create(spawnSystemView, spawnSystem, enemySpawnWaveCollectionFab, spawnNotifierUi);
            turretConstructionViews.Values.ToList().ForEach(view => view.Hide());

            upgradeSystemUiFactory.Create(upgradeSystemUiContainer.Laser, laserUpgradeSystem);
            //      upgradeSystemUiFactory.Create(upgradeSystemUiContainer.Bullet, bulletUpgradeSystem);
            //    upgradeSystemUiFactory.Create(upgradeSystemUiContainer.Rocket, rocketUpgradeSystem);

            hud.TopLeft.AddChild(progressSystemUiFactory.Create(progressSystem));
            hud.TopCenter.AddChild(moneyUiFactory.Create(money));
            hud.TopRight.AddChild(aggressiveSystemUiFactory.Create(aggressiveSystem));
            hud.MiddleCenter.AddChild(spawnNotifierUi);

            hud.MiddleLeft.AddChild(upgradeSystemUiContainer);

            foreach (ConstructButton constructButton in constructButtonCollection.ConstructButtons)
                hud.Footer.AddChild(constructButtonUiFactory.Create(constructButton));

            pathDrawSystemViewFactory.Create(navMeshService);

            return new GameplayScene(
                resourceService, pointerService, gameplayCameraService, spawnSystem, spawnNotifierUi
            );
        }

        private async UniTask LoadResourcesAsync(ResourceService resourceService) =>
            await resourceService
                .Register<AggressiveLevelCollection>("Fabs/Systems/Aggressive/AggressiveLevelCollectionFab")
                .Register<ConstructButtonCollection>("Fabs/Buttons/Constructs/CollectionFab")
                .Register<EnemySpawnWaveCollectionFab>("Fabs/Systems/Spawn/EnemySpawnWaveCollectionFab")
                .Register<UpgradeSystemUiContainer>("Ui/Systems/UpgradeSystemUiContainer")
                .Register<TurretView>("Views/Turrets/TurretView")
                .Register<ZombieView>("Views/Zombies/ZombieView")
                .Register<LaserView>("Views/Bullets/LaserView")
                .Register<BulletView>("Views/Bullets/BulletView")
                .Register<RocketView>("Views/Bullets/RocketView")
                .Register<ConstructButtonUi>("Ui/Buttons/Constructs/ConstructButtonUi")
                .Register<TextUi>("Ui/Credits/MoneyUi")
                .Register<AggressiveSystemUi>("Ui/Systems/AggressiveSystemUi")
                .Register<PathDrawSystemView>("Systems/PathDraw/PathDrawSystemView")
                .Register<PathDrawSystemPointView>("Systems/PathDraw/PathDrawSystemPointView")
                .Register<ProgressSystemUi>("Ui/Systems/ProgressSystemUi")
                .RegisterInterfaceImplementationsByType<TurretConstructionPreview, IWeapon>(
                    "Previews/Weapons/{0}Preview"
                )
                .RegisterInterfaceImplementationsByType<WeaponView, IWeapon>("Views/Weapons/{0}View")
                .RegisterInterfaceImplementationsByType<WeaponFab, IWeapon>("Fabs/Weapons/{0}Fab")
                .LoadAllAsync();
    }
}