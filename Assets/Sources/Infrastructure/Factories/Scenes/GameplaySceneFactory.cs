using System;
using System.Collections.Generic;
using Sources.Controllers.Constructs;
using Sources.Controllers.Scenes;
using Sources.Controllers.Scenes.Gameplay;
using Sources.Domain.Constructs;
using Sources.Domain.Weapons;
using Sources.Infrastructure.Factories.Controllers.Constructs;
using Sources.Infrastructure.Factories.Controllers.Turrets;
using Sources.Infrastructure.Factories.Controllers.Weapons;
using Sources.Infrastructure.Factories.Presentation.Ui;
using Sources.Infrastructure.Factories.Presentation.Views;
using Sources.Infrastructure.Listeners.Pointers;
using Sources.Infrastructure.Listeners.Pointers.Untouchable;
using Sources.Infrastructure.Repositories;
using Sources.Infrastructure.Services.Cameras;
using Sources.Infrastructure.Services.Pointers;
using Sources.InfrastructureInterfaces.Factories.Scenes;
using Sources.Presentation.Uis;
using Sources.Presentation.Views.Cameras;
using UnityEngine;
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
            FooterUi footerUi = Object.FindObjectOfType<FooterUi>();

            GridRepository gridRepository = new GridRepository(tilemap);

            GameplayCamera gameplayCamera = Object.FindObjectOfType<GameplayCamera>();

            GameplayCameraService gameplayCameraService = new GameplayCameraService(gameplayCamera);

            PointerService pointerService = new PointerService();

            ConstructButtonPresenterFactory constructButtonPresenterFactory =
                new ConstructButtonPresenterFactory(pointerService, gameplayCamera, weapons);

            pointerService.RegisterHandler(1, new CameraRotationPointerHandler(gameplayCameraService));

            ConstructButtonUiFactory constructButtonUiFactory =
                new ConstructButtonUiFactory(constructButtonPresenterFactory);

            ConstructButtonCollection constructButtonCollection =
                Resources.Load<ConstructButtonCollection>("Fabs/Buttons/Constructs/CollectionFab");

            foreach (ConstructButton constructButton in constructButtonCollection.ConstructButtons)
                footerUi.AddChild(constructButtonUiFactory.Create(constructButton).gameObject);

            return new GameplayScene(pointerService, gameplayCameraService);
        }
    }
}