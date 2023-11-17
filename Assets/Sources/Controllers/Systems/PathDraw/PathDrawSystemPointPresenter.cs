using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Sources.Domain.Systems.PathDraw;
using Sources.PresentationInterfaces.Views.Systems.PathDraw;
using UnityEngine;

namespace Sources.Controllers.Systems.PathDraw
{
    public class PathDrawSystemPointPresenter : PresenterBase
    {
        private readonly IPathDrawSystemPointView _view;
        private readonly PathDrawSystemPoint _point;
        
        private float _progress;

        public PathDrawSystemPointPresenter(IPathDrawSystemPointView view, PathDrawSystemPoint point)
        {
            _view = view;
            _point = point;
        }

        public override void Enable()
        {
            _progress = 0;
        }

        private float Progress => Mathf.Clamp01(_progress);

        public async void ShowAsync(CancellationToken cancellationToken)
        {
            try
            {
                _view.Show();
                await FadeIn(cancellationToken);
                await Cooldown(_point.LifeTime, cancellationToken: cancellationToken);
                await FadeOut(cancellationToken);
                _view.Destroy();
            }
            catch (OperationCanceledException)
            {
                await FadeOut(new CancellationTokenSource().Token);
                _view.Destroy();
            }
        }

        private async UniTask FadeIn(CancellationToken cancellationToken)
        {
            while (_progress <= 1)
            {
                _progress += Time.deltaTime / _point.SpawnTime;
                await Evaluate(cancellationToken);
            }

            _progress = 1;
            await Evaluate(cancellationToken);
        }

        private async UniTask FadeOut(CancellationToken cancellationToken)
        {
            while (_progress >= 0)
            {
                _progress -= Time.deltaTime / _point.SpawnTime;
                await Evaluate(cancellationToken);
            }

            _progress = 0;
            await Evaluate(cancellationToken);
        }

        private async UniTask Evaluate(CancellationToken cancellationToken)
        {
            _view.SetLocalScale(_view.NativeScale * _view.ScaleCurve.Evaluate(Progress));
            _view.SetPosition(_point.Position + _view.YPositionCurve.Evaluate(Progress) * Vector3.up);

            await UniTask.Yield(cancellationToken: cancellationToken);
        }

        private async UniTask Cooldown(float cooldown, CancellationToken cancellationToken)
        {
            float time = 0;

            while (time < cooldown)
            {
                time += Time.deltaTime;
                await UniTask.Yield(cancellationToken: cancellationToken);
            }
        }
    }
}