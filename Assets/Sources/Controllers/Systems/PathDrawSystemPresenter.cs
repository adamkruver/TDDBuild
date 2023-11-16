using Sources.Infrastructure.Services.NavMeshes;
using Sources.InfrastructureInterfaces.Factories.Presentation.View;
using Sources.PresentationInterfaces.Views.Systems.PathDraw;
using UnityEngine;

namespace Sources.Controllers.Systems
{
    public class PathDrawSystemPresenter : PresenterBase
    {
        private readonly IPathDrawView _pathDrawView;
        private readonly NavMeshService _navMeshService;
        private readonly IPathPointViewFactory _pathPointViewFactory;

        public PathDrawSystemPresenter(
            IPathDrawView pathDrawView,
            NavMeshService navMeshService,
            IPathPointViewFactory pathPointViewFactory
        )
        {
            _pathDrawView = pathDrawView;
            _navMeshService = navMeshService;
            _pathPointViewFactory = pathPointViewFactory;
        }

        public void Draw()
        {
            _pathDrawView.Clear();

            Vector3 startPoint = _pathDrawView.StartPoint;
            Vector3 endPoint = _pathDrawView.EndPoint;
            float interval = _pathDrawView.Interval;

            foreach (Vector3 point in _navMeshService.CalculatePathPoints(startPoint, endPoint, interval))
                _pathDrawView.Append(_pathPointViewFactory.Create(point));

            _pathDrawView.ShowPoints();
        }
        
        public void Hide()
        {
            _pathDrawView.HidePoints();
            _pathDrawView.Clear();
        }
    }
}