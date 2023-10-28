using Sources.Domain.Turrets;
using Sources.Domain.Weapons;
using Sources.Infrastructure.Factories.Presentation.Views;
using Sources.InfrastructureInterfaces.Listeners;
using Sources.Presentation.Views.Cameras;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Sources.Infrastructure.Listeners.Pointers
{
    public class GameplayInteractPointerListener : IPointerListener
    {
        private readonly TurretViewFactory _turretViewFactory;
        private readonly GameplayCamera _gameplayCamera;
        private readonly Tilemap _tilemap;

        public GameplayInteractPointerListener(
            TurretViewFactory turretViewFactory,
            GameplayCamera gameplayCamera
        )
        {
            _turretViewFactory = turretViewFactory;
            _gameplayCamera = gameplayCamera;
            _tilemap = Object.FindObjectOfType<Tilemap>();
        }

        public void OnTouchStart(Vector3 position)
        {
            Ray ray = _gameplayCamera.Camera.ScreenPointToRay(position);
            int layer = 1 << LayerMask.NameToLayer("GameplayGrid");

            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, layer) == false)
                return;

            Vector3Int gridPosition = _tilemap.WorldToCell(hit.point);

            if (_tilemap.HasTile(gridPosition) == false)
                return;

            _turretViewFactory.Create(new Turret(new RocketGun()), new Vector2Int(gridPosition.x, gridPosition.y));
        }

        public void OnTouchMove(Vector3 position)
        {
        }

        public void OnTouchEnd(Vector3 position)
        {
        }
    }
}