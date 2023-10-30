using Sources.Domain.Tiles;
using Sources.Domain.Walls;
using Sources.Infrastructure.Repositories;
using UnityEngine;

namespace Sources.Infrastructure.Factories.Domain.Walls
{
    public class WallFactory
    {
        private readonly TileRepository _tileRepository;

        public WallFactory(TileRepository tileRepository)
        {
            _tileRepository = tileRepository;
        }

        public Wall Create(Vector2Int position)
        {
            Wall wall = new Wall();
            TileModel model = new TileModel(position.x, position.y, wall);
            _tileRepository.Set(model);
            
            return new Wall();
        }
    }
}