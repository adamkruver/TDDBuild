using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Sources.PresentationInterfaces.Views.Systems.PathDraw;
using UnityEngine;

namespace Sources.Presentation.Views.Systems.PathDraw
{
    public class PathPointView : MonoBehaviour, IPathPointView
    {
        [SerializeField] private Vector3 _nativeScale = Vector3.one;
        [SerializeField] private float _scaleTime = 1f;

        private CancellationTokenSource _cancellationTokenSource;

        public void Initialize()
        {
            _cancellationTokenSource = new CancellationTokenSource();
            transform.localScale = Vector3.zero;
        }

        public UniTask Show()
        {
            gameObject.SetActive(true);
            transform.localScale = Vector3.zero;
            transform.DOScale(_nativeScale, _scaleTime);

            return UniTask.Delay(TimeSpan.FromSeconds(_scaleTime), cancellationToken: _cancellationTokenSource.Token);
        }

        public async UniTask Hide()
        {
            transform.localScale = _nativeScale;
            transform.DOScale(Vector3.zero, _scaleTime);

            await UniTask.Delay(TimeSpan.FromSeconds(_scaleTime), cancellationToken: _cancellationTokenSource.Token);
            gameObject.SetActive(false);
        }

        public void Clear() =>
            Destroy(gameObject);

        private void OnDestroy()
        {
            _cancellationTokenSource?.Cancel();
            _cancellationTokenSource?.Dispose();
            transform.DOKill();
        }
    }
}