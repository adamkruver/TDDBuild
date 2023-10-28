using System;
using System.Collections.Generic;
using Sources.Controllers.Scenes;
using Sources.Controllers.Scenes.Gameplay;
using Sources.Domain.Weapons;
using Sources.Infrastructure.Factories.Controllers.Turrets;
using Sources.Infrastructure.Factories.Controllers.Weapons;
using Sources.Infrastructure.Factories.Presentation.Views;
using Sources.Infrastructure.Listeners.Pointers;
using Sources.Infrastructure.Listeners.Pointers.Untouchable;
using Sources.Infrastructure.Repositories;
using Sources.Infrastructure.Services.Cameras;
using Sources.Infrastructure.Services.Pointers;
using Sources.InfrastructureInterfaces.Factories.Scenes;
using Sources.Presentation.Views.Cameras;
using UnityEngine.Tilemaps;
using Object = UnityEngine.Object;

namespace Sources.Infrastructure.Factories.Scenes
{
    public class GameplaySceneFactory : ISceneFactory
    {
        public IScene Create(object payload)
        {
            Dictionary<Type, string> weapons = new Dictionary<Type, string>()
            {
                [typeof(LaserGun)] = "Views/Weapons/LaserGunView",
                [typeof(RocketGun)] = "Views/Weapons/RocketGunView",
            };

            Tilemap tilemap = Object.FindObjectOfType<Tilemap>();
            GridRepository gridRepository = new GridRepository(tilemap);
            
            PointerService pointerService = new PointerService();
            GameplayCamera gameplayCamera = Object.FindObjectOfType<GameplayCamera>();

            GameplayCameraService gameplayCameraService = new GameplayCameraService(gameplayCamera);
            
            TurretPresenterFactory turretPresenterFactory = new TurretPresenterFactory();
            WeaponPresenterFactory weaponPresenterFactory = new WeaponPresenterFactory();
            
            WeaponViewFactory weaponViewFactory = new WeaponViewFactory(weaponPresenterFactory, weapons);

            TurretViewFactory turretViewFactory = new TurretViewFactory(turretPresenterFactory, weaponViewFactory);
            
            pointerService.RegisterHandler(1, new CameraRotationPointerListener(gameplayCameraService));
            pointerService.RegisterUntouchableHandler(new TilemapUntouchablePointerListener(gameplayCamera));
            pointerService.RegisterHandler(0, new GameplayInteractPointerListener(turretViewFactory,gameplayCamera));


            return new GameplayScene(pointerService, gameplayCameraService, turretViewFactory);
        }
    }
}