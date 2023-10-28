using Sources.Domain.Grids;
using Sources.Infrastructure.Repositories;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Sources.Infrastructure.DataSources
{
    public class GridDataSource
    {
        private readonly TileRepository _tileRepository;

        public GridDataSource(TileRepository tileRepository)
        {
            _tileRepository = tileRepository;
        }

        public void Load(Tilemap tilemap)
        {
            BoundsInt tilemapBounds = tilemap.cellBounds;
            TileBase[] tiles = tilemap.GetTilesBlock(tilemapBounds);

            for (int x = 0; x < tilemapBounds.size.x; x++)
            {
                for (int y = 0; y < tilemapBounds.size.y; y++)
                {
                    TileBase tile = tiles[x + y * tilemapBounds.size.x];

                    if (tile is not null)
                        _tileRepository.Set( new TileModel(tilemapBounds.position.x + x, tilemapBounds.position.y + y, null));
                }
            }
        }
    }
}