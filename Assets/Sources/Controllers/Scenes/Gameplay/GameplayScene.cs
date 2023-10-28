using System.Numerics;
using Sources.Domain.Turrets;
using Sources.Domain.Weapons;
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
        private readonly TurretViewFactory _turretViewFactory;

        public GameplayScene(
            PointerService pointerService, 
            GameplayCameraService gameplayCameraService,
            TurretViewFactory turretViewFactory
        )
        {
            _pointerService = pointerService;
            _gameplayCameraService = gameplayCameraService;
            _turretViewFactory = turretViewFactory;
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
            Turret turret = new Turret(new RocketGun());

            _turretViewFactory.Create(turret, new Vector2Int(0,0));
        }

        public void Exit()
        {
        }
    }
}