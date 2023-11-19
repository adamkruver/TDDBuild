using Sources.Presentation.Views.Cameras;
using UnityEngine;

namespace Sources.Infrastructure.Services.Raycasts
{
    public class ScreenRaycastService
    {
        private readonly GameplayCamera _gameplayCamera;
        private readonly int _layer;

        public ScreenRaycastService(
            GameplayCamera gameplayCamera,
            int layer
        )
        {
            _gameplayCamera = gameplayCamera;
            _layer = layer;
        }

        private Camera Camera => _gameplayCamera.Camera;

        public bool TryRaycastFromScreen(Vector3 screenPosition, out RaycastHit hit, float maxDistance = Mathf.Infinity) =>
            Physics.Raycast(Camera.ScreenPointToRay(screenPosition), out hit, maxDistance, _layer);
    }
}