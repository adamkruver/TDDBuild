using Sources.Controllers.Systems;
using Sources.Infrastructure.Factories.Controllers.Systems;
using Sources.Infrastructure.Services.NavMeshes;
using Sources.InfrastructureInterfaces.Factories.Presentation.View;
using Sources.Presentation.Views.Systems.PathDraw;
using UnityEngine;

namespace Sources.Infrastructure.Factories.Presentation.Systems
{
    public class PathDrawViewFactory
    {
        private readonly PathDrawSystemPresenterFactory _pathDrawSystemPresenterFactory;
        private readonly IPathPointViewFactory _pathPointViewFactory;

        public PathDrawViewFactory(PathDrawSystemPresenterFactory pathDrawSystemPresenterFactory,
            IPathPointViewFactory pathPointViewFactory)
        {
            _pathDrawSystemPresenterFactory = pathDrawSystemPresenterFactory;
            _pathPointViewFactory = pathPointViewFactory;
        }

        public PathDrawView Create(NavMeshService navMeshService)
        {
            PathDrawView view = Object.Instantiate(Resources.Load<PathDrawView>("Views/PathDraw/PathDrawView"));
            PathDrawSystemPresenter presenter =
                _pathDrawSystemPresenterFactory.Create(view, navMeshService, _pathPointViewFactory);
            view.Construct(presenter);

            return view;
        }
    }
}