﻿using Sources.Infrastructure.Handlers.Pointers.Untouchable;
using Sources.Infrastructure.Services.Raycasts;
using Sources.Infrastructure.Services.Tilemaps;

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

        public TilemapUntouchablePointerHandler Create() =>
            new TilemapUntouchablePointerHandler(
                _raycastService, _tilemapService
            );
    }
}