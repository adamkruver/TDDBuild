using Sources.Controllers.HealthPoints;
using Sources.PresentationInterfaces.Views.HealthPoints;
using UnityEngine;
using UnityEngine.UI;

namespace Sources.Presentation.Views.HealthPoints
{
    public class HealthView : PresentationViewBase<HealthPresenter>, IHealthView
    {
        [SerializeField] private Canvas _canvas;
        [SerializeField] private Image _slider;

        private Transform _cameraTransform;
        private Transform _canvasTransform;

        private void Awake() =>
            _canvasTransform = _canvas.transform;

        public void Update() =>
            _canvasTransform.forward = -_cameraTransform.forward;

        public void SetCamera(Camera camera) =>
            _cameraTransform = camera.transform;

        public void SetNormalizedValue(float normalizedValue) =>
            _slider.fillAmount = normalizedValue;
    }
}