﻿using System;
using System.Collections.Generic;
using Sources.Constants;
using Sources.Controllers.Scenes;
using Sources.Controllers.Scenes.Gameplay;
using Sources.Domain.Constructs;
using Sources.Domain.Credits;
using Sources.Domain.Systems.Aggressive;
using Sources.Domain.Weapons;
using Sources.Domain.Zombies;
using Sources.Infrastructure.Assessors;
using Sources.Infrastructure.Factories.Controllers.Constructs;
using Sources.Infrastructure.Factories.Controllers.Systems;
using Sources.Infrastructure.Factories.Controllers.Tilemaps;
using Sources.Infrastructure.Factories.Controllers.Zombies;
using Sources.Infrastructure.Factories.Domain.Systems;
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
using Sources.InfrastructureInterfaces.Factories.Scenes;
using Sources.Presentation.Ui;
using Sources.Presentation.Views.Cameras;
using Sources.Presentation.Views.Tilemaps;
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
                [typeof(RocketGun)] = "Views/Weapons/RocketGunView",
            };

            Tilemap tilemap = Object.FindObjectOfType<Tilemap>();
            Hud hud = Object.FindObjectOfType<Hud>();
            TileMapCellUi tileMapCellUi = Object.FindObjectOfType<TileMapCellUi>(true);
            GameplayCamera gameplayCamera = Object.FindObjectOfType<GameplayCamera>();
            BaseView baseView = Object.FindObjectOfType<BaseView>();

            TileRepository tileRepository = new TileRepository(tilemap);

            PaymentService paymentService = new PaymentService(money);
            RaycastService raycastService = new RaycastService(gameplayCamera, Layers.GameplayGrid);
            TilemapService tilemapService = new TilemapService(tilemap, tileMapCellUi);

            ActiveTilePresenterFactory activeTilePresenterFactory =
                new ActiveTilePresenterFactory(tileRepository, tilemapService);
            ActiveTileViewFactory activeTileViewFactory = new ActiveTileViewFactory(activeTilePresenterFactory);

            ActiveTileView activeTile = activeTileViewFactory.Create();

            tilemapService.SetActiveTileView(activeTile);
            tilemapService.HideTileInfo();

            TilemapUntouchablePointerHandlerFactory tilemapUntouchablePointerHandlerFactory =
                new TilemapUntouchablePointerHandlerFactory(
                    raycastService,
                    tilemapService
                );

            GameplayCameraService gameplayCameraService = new GameplayCameraService(gameplayCamera);

            PointerService pointerService = new PointerService();

            ConstructButtonPresenterFactory constructButtonPresenterFactory =
                new ConstructButtonPresenterFactory(
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
            MovementSystemPresenterFactory movementSystemPresenterFactory = new MovementSystemPresenterFactory();

            MovementSystemViewFactory movementSystemViewFactory =
                new MovementSystemViewFactory(movementSystemPresenterFactory);

            DamageableSystemPresenterFactory damageableSystemPresenterFactory = new DamageableSystemPresenterFactory();
            DamageableSystemViewFactory damageableSystemViewFactory =
                new DamageableSystemViewFactory(damageableSystemPresenterFactory);

            ZombieViewFactory zombieViewFactory = new ZombieViewFactory(
                zombiePresenterFactory, movementSystemViewFactory, damageableSystemViewFactory, baseView
            );

            ZombieFactory zombieFactory = new ZombieFactory(enemyRepository, aggressiveSystem);

            return new GameplayScene(
                pointerService, gameplayCameraService, zombieViewFactory, aggressiveSystem, zombieFactory
            );
        }
    }
}