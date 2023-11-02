using Sources.Infrastructure.Handlers.Pointers.Untouchable;
using Sources.Infrastructure.Services.Raycasts;
using Sources.Infrastructure.Services.Tilemaps;
using Sources.PresentationInterfaces.Views.Constructions;

namespace Sources.Infrastructure.Factories.Handlers
{
    public class TilemapUntouchablePointerHandlerFactory
    {
        private readonly RaycastService _raycastService;
        private readonly TilemapService _tilemapService;

        public TilemapUntouchablePointerHandlerFactory(
            RaycastService raycastService,
            TilemapService tilemapService
        )
        {
            _raycastService = raycastService;
            _tilemapService = tilemapService;
        }

        public TilemapUntouchablePointerHandler Create(IConstructionView view) =>
            new TilemapUntouchablePointerHandler(
                _raycastService, _tilemapService, view
            );
    }
}