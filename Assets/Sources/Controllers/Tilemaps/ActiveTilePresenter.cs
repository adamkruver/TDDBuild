using Sources.Domain.Grids;
using Sources.Infrastructure.Repositories;
using Sources.Infrastructure.Services.Tilemaps;
using Sources.PresentationInterfaces.Views;
using UnityEngine;

namespace Sources.Controllers.Tilemaps
{
    public class ActiveTilePresenter
    {
        private readonly IActiveView _view;
        private readonly TileRepository _tileRepository;
        private readonly TilemapService _tilemapService;

        public ActiveTilePresenter(IActiveView view, TileRepository tileRepository, TilemapService tilemapService)
        {
            _view = view;
            _tileRepository = tileRepository;
            _tilemapService = tilemapService;
        }

        public void OnChangedPosition(Vector3 position)
        {
            if (_tilemapService.TryGetTilePosition(position, out Vector2Int tilePosition) == false)
            {
                _view.Deactivate();
                
                return;
            }

            TileModel tile = _tileRepository.Get(tilePosition.x, tilePosition.y);

            if (tile?.Object is not null)
            {
                _view.Deactivate();
                
                return;
            }
            
            _view.Activate();
        }
    }
}