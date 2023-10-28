using Sources.Infrastructure.Services.Cameras;
using Sources.InfrastructureInterfaces.Listeners;
using UnityEngine;

namespace Sources.Infrastructure.Listeners.Pointers
{
    public class CameraRotationPointerListener : IPointerListener
    {
        private readonly GameplayCameraService _gameplayCameraService;

        private Vector3 _startPosition;
        private Vector3 _startAngles;

        private float _rotationStrength = .2f;

        public CameraRotationPointerListener(GameplayCameraService gameplayCameraService)
        {
            _gameplayCameraService = gameplayCameraService;
        }

        public void OnTouchStart(Vector3 position)
        {
            _startPosition = position;
            _startAngles = _gameplayCameraService.Angles;
        }

        public void OnTouchMove(Vector3 position)
        {
            Vector3 delta = (position - _startPosition) * _rotationStrength;
            Vector3 angles = new Vector3(delta.y, delta.x) + _startAngles;

            _gameplayCameraService.Rotate(angles);
        }

        public void OnTouchEnd(Vector3 position)
        {
        }
    }
}