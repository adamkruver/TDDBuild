using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Sources.Presentation.Ui.Elements
{
    public class CircleSlider : MonoBehaviour
    {
        [SerializeField] private Slider _slider;
        [SerializeField] private RectTransform _handler;
        [SerializeField] private float _speed = 3f;

        private float _currentValue = 0;

        private CancellationTokenSource _cancellationTokenSource;

        public void SetNormalizedValue(float normalizedValue)
        {
            _cancellationTokenSource?.Cancel();

            _cancellationTokenSource = new CancellationTokenSource();
            
            SetNormalizedValueAsync(normalizedValue, _cancellationTokenSource.Token);
        }

        private async UniTask SetNormalizedValueAsync(
            float normalizedValue,
            CancellationToken cancellationToken
        )
        {
            while (Math.Abs(_currentValue - normalizedValue) > 0.01f)
            {
                _currentValue = Mathf.Lerp(_currentValue, normalizedValue, Time.deltaTime * _speed);
                Apply();
                
                await UniTask.Yield(cancellationToken);
            }

            Apply();
        }

        private void Apply()
        {
            _slider.value = _currentValue;
            _handler.localEulerAngles = new Vector3(0, 0, -360 * _currentValue);
        }
    }
}