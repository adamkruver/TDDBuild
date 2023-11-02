using UnityEngine;
using UnityEngine.Tilemaps;

namespace Sources.Infrastructure.Services.Tilemaps
{
    public class TilemapService
    {
        private readonly Vector3 _offset = new Vector3(1, 0, 1);
        private readonly Tilemap _tilemap;

        public TilemapService(Tilemap tilemap) =>
            _tilemap = tilemap;

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
    }
}