using Sources.InfrastructureInterfaces.Listeners;
using Sources.Presentation.Uis;
using Sources.Presentation.Views.Cameras;
using Sources.Presentation.Views.Tilemaps;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Sources.Infrastructure.Listeners.Pointers.Untouchable
{
    public class TilemapUntouchablePointerHandler : IUntouchablePointerHandler
    {
        private readonly GameplayCamera _gameplayCamera;
        private readonly Tilemap _tilemap;
        private readonly ActiveTilemapCellView _activeTilemapCell;
        private readonly TileMapCellUi _tileMapCellUi;

        public TilemapUntouchablePointerHandler(GameplayCamera gameplayCamera)
        {
            _gameplayCamera = gameplayCamera;
            _tilemap = Object.FindObjectOfType<Tilemap>();
            _activeTilemapCell = Object.FindObjectOfType<ActiveTilemapCellView>();
            _tileMapCellUi = Object.FindObjectOfType<TileMapCellUi>(true);
        }

        public void OnMove(Vector3 position, bool isPointerOverUI)
        {
            if (isPointerOverUI)
            {
                Hide();
                
                return;
            }
            
            Ray ray = _gameplayCamera.Camera.ScreenPointToRay(position);
            int layer = 1 << LayerMask.NameToLayer("GameplayGrid");

            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, layer) == false)
            {
                Hide();
                
                return;
            }

            Vector3Int gridPosition = _tilemap.WorldToCell(hit.point);

            if (_tilemap.HasTile(gridPosition) == false)
            {
                Hide();
                
                return;
            }

            Show(gridPosition);
        }

        private void Show(Vector3Int gridPosition)
        {
            _activeTilemapCell.Show(gridPosition);
            _tileMapCellUi.Show(gridPosition);
        }

        private void Hide()
        {
            _activeTilemapCell.Hide();
            _tileMapCellUi.Hide();
        }
    }
}