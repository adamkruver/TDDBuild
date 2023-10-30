using System.Collections.Generic;
using System.Linq;
using Sources.Domain.Tiles;
using Sources.Extensions.Lists;
using Sources.Infrastructure.DataSources;
using UnityEngine.Tilemaps;

namespace Sources.Infrastructure.Repositories
{
    public class TileRepository
    {
        private readonly List<TileModel> _gridCells = new List<TileModel>();

        public TileRepository(Tilemap tilemap) =>
            new GridDataSource(this).Load(tilemap);

        public TileModel Get(int x, int y) =>
            _gridCells.FirstOrDefault(cell => cell.X == x && cell.Y == y);

        public void Set(TileModel tileModel) =>
            _gridCells.Replace(Get(tileModel.X, tileModel.Y), tileModel);

        public TileModel[] GetAll() =>
            _gridCells.ToArray();
    }
}