using System;
using System.Collections.Generic;
using System.Linq;
using Sources.Constants;
using Sources.Controllers.Scenes;
using Sources.Controllers.Scenes.Gameplay;
using Sources.Domain.Constructs;
using Sources.Domain.Credits;
using Sources.Domain.Systems.Aggressive;
using Sources.Domain.Systems.EnemySpawn;
using Sources.Domain.Weapons;
using Sources.Domain.Zombies;
using Sources.Infrastructure.Assessors;
using Sources.Infrastructure.Factories.Controllers.Bullets;
using Sources.Infrastructure.Factories.Controllers.Constructions;
using Sources.Infrastructure.Factories.Controllers.Constructs;
using Sources.Infrastructure.Factories.Controllers.Systems;
using Sources.Infrastructure.Factories.Controllers.Tilemaps;
using Sources.Infrastructure.Factories.Controllers.Turrets;
using Sources.Infrastructure.Factories.Controllers.Weapons;
using Sources.Infrastructure.Factories.Controllers.Zombies;
using Sources.Infrastructure.Factories.Domain.Systems;
using Sources.Infrastructure.Factories.Domain.Turrets;
using Sources.Infrastructure.Factories.Domain.Zombies;
using Sources.Infrastructure.Factories.Handlers;
using Sources.Infrastructure.Factories.Presentation.Systems;
using Sources.Infrastructure.Factories.Presentation.Ui;
using Sources.Infrastructure.Factories.Presentation.Views;
using Sources.Infrastructure.Handlers.Pointers;
using Sources.Infrastructure.Repositories;
using Sources.Infrastructure.Services.Cameras;
using Sources.Infrastructure.Services.Payments;
using Sources.Infrastructure.Services.Pointers;
using Sources.Infrastructure.Services.Raycasts;
using Sources.Infrastructure.Services.Tilemaps;
using Sources.Infrastructure.Services.Times;
using Sources.InfrastructureInterfaces.Factories.Scenes;
using Sources.Presentation.Previews.Constructions;
using Sources.Presentation.Ui;
using Sources.Presentation.Views.Cameras;
using Sources.Presentation.Views.Systems.Spawn;
using Sources.PresentationInterfaces.Views.Enemies;
using UnityEngine;
using UnityEngine.Tilemaps;
using Object = UnityEngine.Object;

namespace Sources.Infrastructure.Factories.Scenes
{
    public class GameplaySceneFactory : ISceneFactory
    {
        public IScene Create(object payload)
        {
            Dictionary<Type, int> enemyDeathAccessors = new Dictionary<Type, int>()
            {
                [typeof(Zombie)] = 50,
            };

            AggressiveLevelCollection aggressiveLevelCollection =
                Resources.Load<AggressiveLevelCollection>("Fabs/Systems/Aggressive/AggressiveLevelCollectionFab");

            EnemyRepository enemyRepository = new EnemyRepository();
            AggressiveSystemFactory aggressiveSystemFactory = new AggressiveSystemFactory();
            EnemyDeathAssessor enemyDeathAssessor = new EnemyDeathAssessor(enemyDeathAccessors);

            AggressiveSystem aggressiveSystem = aggressiveSystemFactory.Create(aggressiveLevelCollection);

            Money money = new Money(220);

            Dictionary<Type, string> weapons = new Dictionary<Type, string>()
            {
                [typeof(LaserGun)] = "Views/Weapons/LaserGunView",
                [typeof(RocketTwiceGun)] = "Views/Weapons/RocketGunView",
            };

            Tilemap tilemap = Object.FindObjectOfType<Tilemap>();
            Hud hud = Object.FindObjectOfType<Hud>();
            GameplayCamera gameplayCamera = Object.FindObjectOfType<GameplayCamera>();
            BaseView baseView = Object.FindObjectOfType<BaseView>();

            TileRepository tileRepository = new TileRepository(tilemap);

            PaymentService paymentService = new PaymentService(money);
            RaycastService raycastService = new RaycastService(gameplayCamera, Layers.GameplayGrid);
            TilemapService tilemapService = new TilemapService(tilemap);

            ActiveTilePresenterFactory activeTilePresenterFactory =
                new ActiveTilePresenterFactory(tileRepository, tilemapService);
//            ActiveTileViewFactory activeTileViewFactory = new ActiveTileViewFactory(activeTilePresenterFactory);

            Dictionary<string, TurretConstructionPreview> turretConstructionViews =
                new Dictionary<string, TurretConstructionPreview>()
                {
                    [nameof(LaserGun)] = Object.Instantiate(
                        Resources.Load<TurretConstructionPreview>("Previews/Weapons/LaserGunPreview")
                    ),
                    [nameof(DoubleLaserGun)] = Object.Instantiate(
                        Resources.Load<TurretConstructionPreview>("Previews/Weapons/DoubleLaserGunPreview")
                    ),
                    [nameof(DoubleLaserTwiceGun)] = Object.Instantiate(
                        Resources.Load<TurretConstructionPreview>("Previews/Weapons/DoubleLaserTwiceGunPreview")
                    ),
                    [nameof(MiniTwiceGun)] = Object.Instantiate(
                        Resources.Load<TurretConstructionPreview>("Previews/Weapons/MiniTwiceGunPreview")
                    ),
                    [nameof(RocketTwiceGun)] = Object.Instantiate(
                        Resources.Load<TurretConstructionPreview>("Previews/Weapons/RocketTwiceGunPreview")
                    ),
                };

            turretConstructionViews.Values.ToList().ForEach(view => view.Hide());

            TurretFactory turretFactory = new TurretFactory(tileRepository);

            WeaponStateMachineFactory weaponStateMachineFactory = new WeaponStateMachineFactory();

            BulletPresenterFactory bulletPresenterFactory = new BulletPresenterFactory();


            BulletViewFactory bulletViewFactory = new BulletViewFactory(bulletPresenterFactory);

            WeaponViewFactory weaponViewFactory = new WeaponViewFactory(
                weaponStateMachineFactory, bulletViewFactory, weapons
            );

            TurretPresenterFactory turretPresenterFactory = new TurretPresenterFactory();
            TurretViewFactory turretViewFactory = new TurretViewFactory(turretPresenterFactory, weaponViewFactory);


            TurretConstructionPresenterFactory turretConstructionPresenterFactory =
                new TurretConstructionPresenterFactory(tilemapService, turretViewFactory, turretFactory);

            TurretConstructionViewFactory turretConstructionViewFactory =
                new TurretConstructionViewFactory(turretConstructionPresenterFactory, turretConstructionViews);


            TilemapUntouchablePointerHandlerFactory tilemapUntouchablePointerHandlerFactory =
                new TilemapUntouchablePointerHandlerFactory(
                    raycastService,
                    tilemapService
                );

            GameplayCameraService gameplayCameraService = new GameplayCameraService(gameplayCamera);


            PointerService pointerService = new PointerService();
            TimeService timeService = new TimeService();

            ConstructButtonPresenterFactory constructButtonPresenterFactory =
                new ConstructButtonPresenterFactory(
                    timeService,
                    turretConstructionViewFactory,
                    tilemapUntouchablePointerHandlerFactory,
                    tileRepository,
                    paymentService,
                    pointerService,
                    tilemapService,
                    gameplayCamera,
                    weapons
                );

            pointerService.RegisterHandler(1, new CameraRotationPointerHandler(gameplayCameraService));

            ConstructButtonUiFactory constructButtonUiFactory =
                new ConstructButtonUiFactory(constructButtonPresenterFactory);

            ConstructButtonCollection constructButtonCollection =
                Resources.Load<ConstructButtonCollection>("Fabs/Buttons/Constructs/CollectionFab");


            AggressiveSystemPresenterFactory aggressiveSystemPresenterFactory =
                new AggressiveSystemPresenterFactory(enemyRepository);

            AggressiveSystemViewFactory aggressiveSystemViewFactory =
                new AggressiveSystemViewFactory(aggressiveSystemPresenterFactory);

            hud.TopCenter.AddChild(new MoneyUiFactory().Create(money));
            hud.TopRight.AddChild(aggressiveSystemViewFactory.Create(aggressiveSystem));

            foreach (ConstructButton constructButton in constructButtonCollection.ConstructButtons)
                hud.Footer.AddChild(constructButtonUiFactory.Create(constructButton));

            ZombiePresenterFactory zombiePresenterFactory = new ZombiePresenterFactory(
                aggressiveSystem, enemyRepository, enemyDeathAssessor
            );
            ZombieStateMachineFactory zombieStateMachineFactory = new ZombieStateMachineFactory(
                aggressiveSystem, enemyRepository, enemyDeathAssessor);
            
            MovementSystemPresenterFactory movementSystemPresenterFactory = new MovementSystemPresenterFactory();

            MovementSystemViewFactory movementSystemViewFactory =
                new MovementSystemViewFactory(movementSystemPresenterFactory);

            DamageableSystemPresenterFactory damageableSystemPresenterFactory = new DamageableSystemPresenterFactory();
            DamageableSystemViewFactory damageableSystemViewFactory =
                new DamageableSystemViewFactory(damageableSystemPresenterFactory);

            ZombieViewFactory zombieViewFactory = new ZombieViewFactory(
                zombiePresenterFactory, zombieStateMachineFactory, movementSystemViewFactory, damageableSystemViewFactory, baseView
            );

            ZombieFactory zombieFactory = new ZombieFactory(enemyRepository, aggressiveSystem);

            Dictionary<string, Func<Vector3, IEnemyView>> enemyViewFactories =
                new Dictionary<string, Func<Vector3, IEnemyView>>()
                {
                    ["Zombie"] = position => zombieViewFactory.Create(zombieFactory.Create(), position),
                };

            EnemyViewFactory enemyViewFactory = new EnemyViewFactory(enemyViewFactories);

            SpawnSystemPresenterFactory spawnSystemPresenterFactory = new SpawnSystemPresenterFactory(enemyViewFactory);
            SpawnSystemViewFactory spawnSystemViewFactory = new SpawnSystemViewFactory(spawnSystemPresenterFactory);

            SpawnSystemView spawnSystemView = Object.FindObjectOfType<SpawnSystemView>();
            EnemySpawnWaveCollectionFab enemySpawnWaveCollectionFab =
                Resources.Load<EnemySpawnWaveCollectionFab>("Fabs/Systems/Spawn/EnemySpawnWaveCollectionFab");

            spawnSystemViewFactory.Create(spawnSystemView, enemySpawnWaveCollectionFab);

            return new GameplayScene(pointerService, gameplayCameraService, aggressiveSystem);
        }
    }
}