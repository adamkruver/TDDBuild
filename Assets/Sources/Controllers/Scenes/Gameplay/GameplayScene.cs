using Sources.Domain.Zombies;
using Sources.Infrastructure.Factories.Controllers.Zombies;
using Sources.Infrastructure.Factories.Presentation.Views;
using Sources.Infrastructure.Services.Cameras;
using Sources.Infrastructure.Services.Pointers;

namespace Sources.Controllers.Scenes.Gameplay
{
    public class GameplayScene : IScene
    {
        private readonly PointerService _pointerService;
        private readonly GameplayCameraService _gameplayCameraService;
        private readonly ZombieViewFactory _zombieViewFactory;

        public GameplayScene(
            PointerService pointerService,
            GameplayCameraService gameplayCameraService,
            ZombieViewFactory zombieViewFactory
        )
        {
            _pointerService = pointerService;
            _gameplayCameraService = gameplayCameraService;
            _zombieViewFactory = zombieViewFactory;
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
            _zombieViewFactory.Create(new Zombie());
        }

        public void Exit()
        {
        }
    }
}