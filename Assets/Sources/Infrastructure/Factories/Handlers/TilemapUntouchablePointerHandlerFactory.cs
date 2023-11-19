using Sources.Infrastructure.Handlers.Pointers.Untouchable;
using Sources.Infrastructure.Services.Raycasts;
using Sources.Infrastructure.Services.Tilemaps;
using Sources.PresentationInterfaces.Views.Constructions;

namespace Sources.Infrastructure.Factories.Handlers
{
    public class TilemapUntouchablePointerHandlerFactory
    {
        private readonly ScreenRaycastService _screenRaycastService;
        private readonly TilemapService _tilemapService;

        public TilemapUntouchablePointerHandlerFactory(
            ScreenRaycastService screenRaycastService,
            TilemapService tilemapService
        )
        {
            _screenRaycastService = screenRaycastService;
            _tilemapService = tilemapService;
        }

        public TilemapUntouchablePointerHandler Create(IConstructionView view) =>
            new TilemapUntouchablePointerHandler(
                _screenRaycastService, _tilemapService, view
            );
    }
}