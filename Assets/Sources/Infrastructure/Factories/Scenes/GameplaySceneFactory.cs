using Sources.Controllers.Scenes;
using Sources.Controllers.Scenes.Gameplay;
using Sources.Infrastructure.Handlers.Pointers;
using Sources.Infrastructure.Handlers.Pointers.Untouchable;
using Sources.Infrastructure.Services.Cameras;
using Sources.Infrastructure.Services.Pointers;
using Sources.InfrastructureInterfaces.Factories.Scenes;
using Sources.Presentation.Views.Cameras;
using UnityEngine;

namespace Sources.Infrastructure.Factories.Scenes
{
    public class GameplaySceneFactory : ISceneFactory
    {
        public IScene Create(object payload)
        {
            PointerService pointerService = new PointerService();
            GameplayCamera gameplayCamera = Object.FindObjectOfType<GameplayCamera>();

            GameplayCameraService gameplayCameraService = new GameplayCameraService(gameplayCamera);
            
            pointerService.RegisterHandler(1, new CameraRotationPointerHandler(gameplayCameraService));
            pointerService.RegisterUntouchableHandler(new TilemapUntouchablePointerHandler(gameplayCamera));

            return new GameplayScene(pointerService, gameplayCameraService);
        }
    }
}