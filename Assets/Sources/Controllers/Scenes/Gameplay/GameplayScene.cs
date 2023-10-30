using Sources.Domain.Systems.Aggressive;
using Sources.Domain.Zombies;
using Sources.Infrastructure.Factories.Controllers.Zombies;
using Sources.Infrastructure.Factories.Domain.Zombies;
using Sources.Infrastructure.Factories.Presentation.Views;
using Sources.Infrastructure.Services.Cameras;
using Sources.Infrastructure.Services.Pointers;
using UnityEngine;

namespace Sources.Controllers.Scenes.Gameplay
{
    public class GameplayScene : IScene
    {
        private readonly PointerService _pointerService;
        private readonly GameplayCameraService _gameplayCameraService;
        private readonly ZombieViewFactory _zombieViewFactory;
        private readonly AggressiveSystem _aggressiveSystem;
        private readonly ZombieFactory _zombieFactory;

        public GameplayScene(
            PointerService pointerService,
            GameplayCameraService gameplayCameraService,
            ZombieViewFactory zombieViewFactory,
            AggressiveSystem aggressiveSystem,
            ZombieFactory zombieFactory
        )
        {
            _pointerService = pointerService;
            _gameplayCameraService = gameplayCameraService;
            _zombieViewFactory = zombieViewFactory;
            _aggressiveSystem = aggressiveSystem;
            _zombieFactory = zombieFactory;
        }

        public void Update(float deltaTime)
        {
            _pointerService.Update(deltaTime);

            if (Input.GetKeyDown(KeyCode.A)) 
                _aggressiveSystem.AddProgress(50);
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
            _zombieViewFactory.Create(_zombieFactory.Create());
            _zombieViewFactory.Create(_zombieFactory.Create());
            _zombieViewFactory.Create(_zombieFactory.Create());
            _zombieViewFactory.Create(_zombieFactory.Create());
            _zombieViewFactory.Create(_zombieFactory.Create());
        }

        public void Exit()
        {
        }
    }
}