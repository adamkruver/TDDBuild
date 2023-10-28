using System.Collections.Generic;
using System.Linq;
using Sources.Domain.Grids;
using Sources.Extensions.Lists;
using Sources.Infrastructure.DataSources;
using UnityEngine.Tilemaps;

namespace Sources.Infrastructure.Repositories
{
    public class GridRepository
    {
        private readonly List<GridCell> _gridCells = new List<GridCell>();

        public GridRepository(Tilemap tilemap) =>
            new GridDataSource(this).Load(tilemap);

        public GridCell Get(int x, int y) =>
            _gridCells.FirstOrDefault(cell => cell.X == x && cell.Y == y);

        public void Set(GridCell gridCell) =>
            _gridCells.Replace(Get(gridCell.X, gridCell.Y), gridCell);

        public GridCell[] GetAll() =>
            _gridCells.ToArray();
    }
}