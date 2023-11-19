using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Sources.Presentation.Ui.Systems.Spawn
{
    [RequireComponent(typeof(CanvasGroup))]
    public class SpawnNotifierUi : MonoBehaviour
    {
        [Range(.2f, 10)] [SerializeField] private float _lifeTime = 1;
        [SerializeField] private AnimationCurve _sizeOverLifeTime;
        [SerializeField] private AnimationCurve _alphaOverLifeTime;

        private CancellationTokenSource _cancellationTokenSource;
        private Transform _transform;
        private CanvasGroup _canvasGroup;

        private void Awake()
        {
            _transform = GetComponent<Transform>();
            _canvasGroup = GetComponent<CanvasGroup>();
        }

        public void Show()
        {
            _cancellationTokenSource?.Cancel();
            _cancellationTokenSource = new CancellationTokenSource();

            Animate(_cancellationTokenSource.Token);
        }

        private async UniTask Animate(CancellationToken cancellationToken)
        {
            float time = 0;

            SetNomalizedTime(0);
            while (time < 1)
            {
                time = Mathf.MoveTowards(time, 1, Time.deltaTime / _lifeTime);
                SetNomalizedTime(time);

                await UniTask.Yield(cancellationToken);
            }

            SetNomalizedTime(1);
        }

        private void SetNomalizedTime(float value)
        {
            value = Mathf.Clamp01(value);

            _canvasGroup.alpha = _alphaOverLifeTime.Evaluate(value);
            _transform.localScale = Vector3.one * _sizeOverLifeTime.Evaluate(value);
        }
    }
}