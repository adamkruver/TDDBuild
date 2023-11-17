using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using Sources.Infrastructure.Factories.Presentation.Systems.PathDraw;
using Sources.Infrastructure.Services.NavMeshes;
using Sources.Presentation.Views.Systems.PathDraw;
using Sources.PresentationInterfaces.Views.Systems.PathDraw;
using UnityEngine;

namespace Sources.Controllers.Systems.PathDraw
{
    public class PathDrawSystemPresenter : PresenterBase
    {
        private readonly IPathDrawSystemView _pathDrawSystemView;
        private readonly NavMeshService _navMeshService;
        private readonly PathDrawSystemPointViewFactory _pathDrawSystemPointViewFactory;
        private CancellationTokenSource _cancellationTokenSource;

        public PathDrawSystemPresenter(
            IPathDrawSystemView pathDrawSystemView,
            NavMeshService navMeshService,
            PathDrawSystemPointViewFactory pathDrawSystemPointViewFactory
        )
        {
            _pathDrawSystemView = pathDrawSystemView;
            _navMeshService = navMeshService;
            _pathDrawSystemPointViewFactory = pathDrawSystemPointViewFactory;
        }

        public override void Enable() =>
            _pathDrawSystemView.AddButtonListener(Draw);

        public override void Disable() =>
            _pathDrawSystemView.RemoveButtonListener(Draw);

        private async void Draw()
        {
            _cancellationTokenSource?.Cancel();
            _cancellationTokenSource = new CancellationTokenSource();

            CancellationToken cancellationToken = _cancellationTokenSource.Token;
            Vector3 startPoint = _pathDrawSystemView.StartPoint;
            Vector3 endPoint = _pathDrawSystemView.EndPoint;
            float interval = _pathDrawSystemView.Distance;

            try
            {
                foreach (Vector3 point in _navMeshService.CalculatePathPoints(startPoint, endPoint, interval))
                {
                    PathDrawSystemPointView pointView = _pathDrawSystemPointViewFactory.Create(point, Vector3.forward);
                    pointView.Show(cancellationToken);

                    await UniTask.Delay(
                        TimeSpan.FromSeconds(_pathDrawSystemView.SpawnInterval),
                        cancellationToken: cancellationToken
                    );
                }
            }
            catch (OperationCanceledException)
            {
            }
        }
    }
}