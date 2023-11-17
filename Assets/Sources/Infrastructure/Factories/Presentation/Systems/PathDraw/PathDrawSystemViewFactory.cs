using Sources.Controllers.Systems.PathDraw;
using Sources.Infrastructure.Factories.Controllers.Systems.PathDraw;
using Sources.Infrastructure.Services.NavMeshes;
using Sources.InfrastructureInterfaces.Providers;
using Sources.Presentation.Views.Systems.PathDraw;
using UnityEngine;

namespace Sources.Infrastructure.Factories.Presentation.Systems.PathDraw
{
    public class PathDrawSystemViewFactory
    {
        private readonly IResourceProvider _resourceProvider;
        private readonly PathDrawSystemPresenterFactory _pathDrawSystemPresenterFactory;
        private readonly PathDrawSystemPointViewFactory _pathDrawSystemPointViewFactory;

        public PathDrawSystemViewFactory(
            IResourceProvider resourceProvider,
            PathDrawSystemPresenterFactory pathDrawSystemPresenterFactory,
            PathDrawSystemPointViewFactory pathDrawSystemPointViewFactory
        )
        {
            _resourceProvider = resourceProvider;
            _pathDrawSystemPresenterFactory = pathDrawSystemPresenterFactory;
            _pathDrawSystemPointViewFactory = pathDrawSystemPointViewFactory;
        }

        private PathDrawSystemView Prefab =>
            _resourceProvider.Load<PathDrawSystemView>("Systems/PathDraw/PathDrawSystemView");

        public PathDrawSystemView Create(NavMeshService navMeshService)
        {
            PathDrawSystemView systemView = Object.Instantiate(Prefab);

            PathDrawSystemPresenter presenter =
                _pathDrawSystemPresenterFactory.Create(systemView, navMeshService, _pathDrawSystemPointViewFactory);
            systemView.Construct(presenter);

            return systemView;
        }
    }
}