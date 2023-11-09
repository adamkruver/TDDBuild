using Sources.Domain.Systems.Aggressive;
using Sources.Domain.Systems.Spawn;
using Sources.Infrastructure.Resource;
using Sources.Infrastructure.Services.Cameras;
using Sources.Infrastructure.Services.Pointers;
using Sources.Presentation.Ui.Systems.Spawn;
using UnityEngine;

namespace Sources.Controllers.Scenes.Gameplay
{
    public class GameplayScene : IScene
    {
        private readonly ResourceService _resourceService;
        private readonly PointerService _pointerService;
        private readonly GameplayCameraService _gameplayCameraService;
        private readonly SpawnSystem _spawnSystem;
        private readonly AggressiveSystem _aggressiveSystem;
        private readonly SpawnNotifierUi _spawnNotifierUi;

        public GameplayScene(
            ResourceService resourceService,
            PointerService pointerService,
            GameplayCameraService gameplayCameraService,
            SpawnSystem spawnSystem,
            SpawnNotifierUi spawnNotifierUi
        )
        {
            _resourceService = resourceService;
            _pointerService = pointerService;
            _gameplayCameraService = gameplayCameraService;
            _spawnSystem = spawnSystem;
            _spawnNotifierUi = spawnNotifierUi;
        }

        public void Update(float deltaTime)
        {
            _pointerService.Update(deltaTime);
            
            if (Input.GetKeyDown(KeyCode.W))
                _spawnNotifierUi.Show();
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
            _spawnSystem.Start();
        }

        public void Exit()
        {
            _spawnSystem.Finish();
            _resourceService.Dispose();
        }
    }
}