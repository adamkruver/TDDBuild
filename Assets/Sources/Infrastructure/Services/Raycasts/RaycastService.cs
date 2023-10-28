using Sources.Presentation.Views.Cameras;
using UnityEngine;

namespace Sources.Infrastructure.Services.Raycasts
{
    public class RaycastService
    {
        private readonly GameplayCamera _gameplayCamera;
        private readonly int _layer;

        public RaycastService(
            GameplayCamera gameplayCamera,
            int layer
        )
        {
            _gameplayCamera = gameplayCamera;
            _layer = layer;
        }

        private Camera Camera => _gameplayCamera.Camera;

        public bool TryRaycastFromScreen(Vector3 screenPosition, out RaycastHit hit) =>
            Physics.Raycast(Camera.ScreenPointToRay(screenPosition), out hit, Mathf.Infinity, _layer);
    }
}