using Sources.Infrastructure.Services.Cameras;
using Sources.Infrastructure.Services.Pointers;

namespace Sources.Controllers.Scenes.Gameplay
{
    public class GameplayScene : IScene
    {
        private readonly PointerService _pointerService;
        private readonly GameplayCameraService _gameplayCameraService;

        public GameplayScene(
            PointerService pointerService,
            GameplayCameraService gameplayCameraService
        )
        {
            _pointerService = pointerService;
            _gameplayCameraService = gameplayCameraService;
        }

        public void Update(float deltaTime)
        {
            _pointerService.Update(deltaTime);
        }

        public void UpdateFixed(float fixedDeltaTime)
        {
        }

        public void UpdateLate(float deltaTime)
        {
            _gameplayCameraService.UpdateLate(deltaTime);
        }

        public void Enter(object payload)
        {
        }

        public void Exit()
        {
        }
    }
}