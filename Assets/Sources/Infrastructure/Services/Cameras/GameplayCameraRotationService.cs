using Sources.InfrastructureInterfaces.Services;
using Sources.Presentation.Views.Cameras;
using UnityEngine;
using UnityEngine.UIElements;

namespace Sources.Infrastructure.Services.Cameras
{
    public class GameplayCameraService : ILateUpdatable
    {
        private readonly GameplayCamera _camera;
        
        private Vector2 _angles;
        
        public GameplayCameraService(GameplayCamera camera) => 
            _camera = camera;

        public Vector3 Angles => _camera.Angles;

        public void Rotate(Vector2 angles) =>
            _angles = angles;

        public void UpdateLate(float deltaTime) => 
            _camera.Rotate(_angles);
    }
}