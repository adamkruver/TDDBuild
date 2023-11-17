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

        public PathDrawSystemPointPresenter(IPathDrawSystemPointView view, PathDrawSystemPoint point)
        {
            _view = view;
            _point = point;
        }

        public async UniTask Show(CancellationToken cancellationToken)
        {
            _view.Show();
            await FadeIn(cancellationToken);
            await UniTask.Delay(TimeSpan.FromSeconds(_point.LifeTime), cancellationToken: cancellationToken);
            await FadeOut(cancellationToken);
            _view.Destroy();
        }

        private async UniTask FadeIn(CancellationToken cancellationToken)
        {
            for (float time = 0; time <= 1; time += Time.deltaTime / _point.SpawnTime)
                await Evaluate(time, cancellationToken);

            await Evaluate(1, cancellationToken);
        }

        private async UniTask FadeOut(CancellationToken cancellationToken)
        {
            for (float time = 1; time >= 0; time -= Time.deltaTime / _point.SpawnTime)
                await Evaluate(time, cancellationToken);

            await Evaluate(0, cancellationToken);
        }

        private async UniTask Evaluate(float time, CancellationToken cancellationToken)
        {
            _view.SetLocalScale(_view.NativeScale * _view.ScaleCurve.Evaluate(time));
            _view.SetPosition(_point.Position + _view.YPositionCurve.Evaluate(time) * Vector3.up);

            await UniTask.Yield(cancellationToken: cancellationToken);
        }
    }
}