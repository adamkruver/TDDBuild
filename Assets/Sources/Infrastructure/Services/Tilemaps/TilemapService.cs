using Sources.Presentation.Ui;
using Sources.Presentation.Views.Tilemaps;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Sources.Infrastructure.Services.Tilemaps
{
    public class TilemapService
    {
        private readonly Vector3 _offset = new Vector3(1, 0, 1);
        private readonly Tilemap _tilemap;
        private readonly TileMapCellUi _tileMapCellUi;

        private ActiveTileView _activeTileView;
        
        public TilemapService(
            Tilemap tilemap,
            TileMapCellUi tileMapCellUi
        )
        {
            _tilemap = tilemap;
            _tileMapCellUi = tileMapCellUi;
        }
        
        public void SetActiveTileView(ActiveTileView activeTileView) =>
            _activeTileView = activeTileView;
        
        public bool TryGetTilePosition(Vector3 worldPosition, out Vector2Int tilePosition)
        {
            Vector3Int position = _tilemap.WorldToCell(worldPosition);

            if (_tilemap.HasTile(position) == false)
            {
                tilePosition = default;

                return false;
            }

            tilePosition = new Vector2Int(position.x, position.y);

            return true;
        }

        public Vector3 ConvertToWorldPosition(Vector2Int tilePosition) =>
            new Vector3(tilePosition.x, 0, tilePosition.y) * 2 + _offset;

        public void ShowTileInfo(Vector3 position)
        {
            _activeTileView.Show(position);
            _tileMapCellUi.Show(position);
        }

        public void HideTileInfo()
        {
            _activeTileView.Hide();
            _tileMapCellUi.Hide();
        }
    }
}