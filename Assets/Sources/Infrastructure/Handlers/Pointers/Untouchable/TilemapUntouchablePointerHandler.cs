using Sources.InfrastructureInterfaces.Handlers;
using Sources.Presentation.Views.Cameras;
using Sources.Presentation.Views.Tilemaps;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Sources.Infrastructure.Handlers.Pointers.Untouchable
{
    public class TilemapUntouchablePointerHandler : IUntouchablePointerHandler
    {
        private readonly GameplayCamera _gameplayCamera;
        private readonly Tilemap _tilemap;
        private readonly ActiveTilemapCellView _activeTilemapCell;

        public TilemapUntouchablePointerHandler(GameplayCamera gameplayCamera)
        {
            _gameplayCamera = gameplayCamera;
            _tilemap = Object.FindObjectOfType<Tilemap>();
            _activeTilemapCell = Object.FindObjectOfType<ActiveTilemapCellView>();
        }

        public void OnMove(Vector3 position)
        {
            Ray ray = _gameplayCamera.Camera.ScreenPointToRay(position);
            int layer = 1 << LayerMask.NameToLayer("GameplayGrid");

            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, layer) == false)
            {
                _activeTilemapCell.Hide();
                
                return;
            }

            Vector3Int gridPosition = _tilemap.WorldToCell(hit.point);

            if (_tilemap.HasTile(gridPosition) == false)
            {
                _activeTilemapCell.Hide();
                
                return;
            }

            _activeTilemapCell.Show(gridPosition);
        }
    }
}