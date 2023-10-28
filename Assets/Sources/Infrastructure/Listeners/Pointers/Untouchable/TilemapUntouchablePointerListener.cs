using Sources.InfrastructureInterfaces.Listeners;
using Sources.Presentation.Uis;
using Sources.Presentation.Views.Cameras;
using Sources.Presentation.Views.Tilemaps;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Sources.Infrastructure.Listeners.Pointers.Untouchable
{
    public class TilemapUntouchablePointerListener : IUntouchablePointerListener
    {
        private readonly GameplayCamera _gameplayCamera;
        private readonly Tilemap _tilemap;
        private readonly ActiveTilemapCellView _activeTilemapCell;
        private readonly TileMapCellUi _tileMapCellUi;

        public TilemapUntouchablePointerListener(GameplayCamera gameplayCamera)
        {
            _gameplayCamera = gameplayCamera;
            _tilemap = Object.FindObjectOfType<Tilemap>();
            _activeTilemapCell = Object.FindObjectOfType<ActiveTilemapCellView>();
            _tileMapCellUi = Object.FindObjectOfType<TileMapCellUi>(true);
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
            _tileMapCellUi.Show(gridPosition);
        }
    }
}