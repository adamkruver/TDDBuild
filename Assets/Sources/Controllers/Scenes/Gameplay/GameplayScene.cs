using Sources.Domain.Systems.Aggressive;
using Sources.Infrastructure.Resource;
using Sources.Infrastructure.Services.Cameras;
using Sources.Infrastructure.Services.Pointers;
using UnityEngine;

namespace Sources.Controllers.Scenes.Gameplay
{
    public class GameplayScene : IScene
    {
        private readonly ResourceService _resourceService;
        private readonly PointerService _pointerService;
        private readonly GameplayCameraService _gameplayCameraService;
        private readonly AggressiveSystem _aggressiveSystem;

        public GameplayScene(
            ResourceService resourceService,
            PointerService pointerService,
            GameplayCameraService gameplayCameraService,
            AggressiveSystem aggressiveSystem
        )
        {
            _resourceService = resourceService;
            _pointerService = pointerService;
            _gameplayCameraService = gameplayCameraService;
            _aggressiveSystem = aggressiveSystem;
        }

        public void Update(float deltaTime)
        {
            _pointerService.Update(deltaTime);

            if (Input.GetKeyDown(KeyCode.A))
            {
                _aggressiveSystem.AddProgress(40);
            }
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
            _resourceService.Dispose();
        }
    }
}