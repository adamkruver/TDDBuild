using Sources.Infrastructure.Services.Raycasts;
using Sources.Infrastructure.Services.Tilemaps;
using Sources.InfrastructureInterfaces.Listeners;
using Sources.Presentation.Ui;
using Sources.Presentation.Views.Tilemaps;
using UnityEngine;

namespace Sources.Infrastructure.Handlers.Pointers.Untouchable
{
    public class TilemapUntouchablePointerHandler : IUntouchablePointerHandler
    {
        private readonly RaycastService _raycastService;
        private readonly TilemapService _tilemapService;

        public TilemapUntouchablePointerHandler(
            RaycastService raycastService,
            TilemapService tilemapService
        )
        {
            _raycastService = raycastService;
            _tilemapService = tilemapService;
        }

        public void OnMove(Vector3 screenPosition, bool isPointerOverUI)
        {
            if (isPointerOverUI || TryGetTileWorldPosition(screenPosition, out Vector3 tileWorldPosition) == false)
            {
                _tilemapService.HideTileInfo();

                return;
            }

            _tilemapService.ShowTileInfo(tileWorldPosition);
        }

        private bool TryGetTileWorldPosition(Vector3 screenPosition, out Vector3 tileWorldPosition)
        {
            if (_raycastService.TryRaycastFromScreen(screenPosition, out RaycastHit hit) == false
                || _tilemapService.TryGetTilePosition(hit.point, out Vector2Int tilePosition) == false)
            {
                tileWorldPosition = default;

                return false;
            }
            
            tileWorldPosition = _tilemapService.ConvertToWorldPosition(tilePosition);

            return true;
        }
    }
}