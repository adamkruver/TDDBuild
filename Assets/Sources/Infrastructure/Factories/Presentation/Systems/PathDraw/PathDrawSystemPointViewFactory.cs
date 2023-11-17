using Sources.Controllers.Systems.PathDraw;
using Sources.Domain.Systems.PathDraw;
using Sources.Infrastructure.Factories.Controllers.Systems.PathDraw;
using Sources.Infrastructure.ObjectPools;
using Sources.InfrastructureInterfaces.Providers;
using Sources.Presentation.Views.Systems.PathDraw;
using UnityEngine;

namespace Sources.Infrastructure.Factories.Presentation.Systems.PathDraw
{
    public class PathDrawSystemPointViewFactory
    {
        private readonly IResourceProvider _resourceProvider;
        private readonly PathDrawSystemPointPresenterFactory _pathDrawSystemPointPresenterFactory;
        private readonly ObjectPool _objectPool;

        public PathDrawSystemPointViewFactory(
            IResourceProvider resourceProvider,
            PathDrawSystemPointPresenterFactory pathDrawSystemPointPresenterFactory
        )
        {
            _resourceProvider = resourceProvider;
            _pathDrawSystemPointPresenterFactory = pathDrawSystemPointPresenterFactory;
            _objectPool = new ObjectPool(" ==== Path Points Pool ==== ");
        }

        private PathDrawSystemPointView Prefab =>
            _resourceProvider.Load<PathDrawSystemPointView>("Systems/PathDraw/PathDrawSystemPointView");

        public PathDrawSystemPointView Create(Vector3 position, Vector3 direction)
        {
            if (_objectPool.Contain<PathDrawSystemPointView>() == false)
                Object.Instantiate(Prefab).SetPool(_objectPool).Destroy();
            
            PathDrawSystemPointView pathDrawSystemPointView = _objectPool.Get<PathDrawSystemPointView>();

            PathDrawSystemPoint point = new PathDrawSystemPoint(position, direction, 1.4f, 3f);

            PathDrawSystemPointPresenter presenter =
                _pathDrawSystemPointPresenterFactory.Create(pathDrawSystemPointView, point);
            pathDrawSystemPointView.Construct(presenter);

            return pathDrawSystemPointView;
        }
    }
}