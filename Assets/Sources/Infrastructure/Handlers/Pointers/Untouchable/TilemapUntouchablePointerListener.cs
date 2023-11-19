using Sources.Infrastructure.Services.Raycasts;
using Sources.Infrastructure.Services.Tilemaps;
using Sources.InfrastructureInterfaces.Listeners;
using Sources.Presentation.Ui;
using Sources.Presentation.Views.Tilemaps;
using Sources.PresentationInterfaces.Views.Constructions;
using UnityEngine;

namespace Sources.Infrastructure.Handlers.Pointers.Untouchable
{
    public class TilemapUntouchablePointerHandler : IUntouchablePointerHandler
    {
        private readonly ScreenRaycastService _screenRaycastService;
        private readonly TilemapService _tilemapService;
        private readonly IConstructionView _constructionView;

        public TilemapUntouchablePointerHandler(
            ScreenRaycastService screenRaycastService,
            TilemapService tilemapService,
            IConstructionView constructionView
        )
        {
            _screenRaycastService = screenRaycastService;
            _tilemapService = tilemapService;
            _constructionView = constructionView;
        }

        public void OnMove(Vector3 screenPosition, bool isPointerOverUI)
        {
            if (isPointerOverUI || TryGetTileWorldPosition(screenPosition, out Vector3 tileWorldPosition) == false)
            {
                _constructionView.Hide();

                return;
            }

            _constructionView.Show();
            _constructionView.SetPosition(tileWorldPosition);
        }

        private bool TryGetTileWorldPosition(Vector3 screenPosition, out Vector3 tileWorldPosition)
        {
            if (_screenRaycastService.TryRaycastFromScreen(screenPosition, out RaycastHit hit) == false
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