using Sources.Controllers.Tilemaps;
using Sources.Infrastructure.Repositories;
using Sources.Infrastructure.Services.Tilemaps;
using Sources.PresentationInterfaces.Views;

namespace Sources.Infrastructure.Factories.Controllers.Tilemaps
{
    public class ActiveTilePresenterFactory
    {
        private readonly TileRepository _tileRepository;
        private readonly TilemapService _tilemapService;

        public ActiveTilePresenterFactory(TileRepository tileRepository, TilemapService tilemapService)
        {
            _tileRepository = tileRepository;
            _tilemapService = tilemapService;
        }

        public ActiveTilePresenter Create(IActiveView view) =>
            new ActiveTilePresenter(view, _tileRepository, _tilemapService);
    }
}