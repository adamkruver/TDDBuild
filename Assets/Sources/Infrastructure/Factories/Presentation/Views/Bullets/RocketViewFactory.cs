using System;
using Sources.Controllers.Projectiles;
using Sources.Domain.Projectiles;
using Sources.Infrastructure.Factories.Controllers.Projectiles;
using Sources.Infrastructure.ObjectPools;
using Sources.InfrastructureInterfaces.Factories.Presentation.Projectiles;
using Sources.InfrastructureInterfaces.Providers;
using Sources.Presentation.Views.Bullets;
using Sources.PresentationInterfaces.Views.Bullets;
using Object = UnityEngine.Object;

namespace Sources.Infrastructure.Factories.Presentation.Views.Bullets
{
    public class RocketViewFactory : IRocketViewFactory
    {
        private readonly RocketPresenterFactory _rocketPresenterFactory;
        private readonly ObjectPool _objectPool;
        private readonly RocketView _prefab;

        public RocketViewFactory(RocketPresenterFactory rocketPresenterFactory, IResourceProvider resourceProvider)
        {
            _rocketPresenterFactory =
                rocketPresenterFactory ?? throw new ArgumentNullException(nameof(rocketPresenterFactory));

            if (resourceProvider == null)
                throw new ArgumentNullException(nameof(resourceProvider));

            _objectPool = new ObjectPool(" ==== Rocket Pool ==== ");

            _prefab = resourceProvider.Load<RocketView>("Views/Bullets/RocketView") ??
                      throw new InvalidOperationException(nameof(resourceProvider.Load));
        }

        public IProjectileView Create(Rocket rocket)
        {
            if (_objectPool.Contain<RocketView>() == false)
            {
                Object.Instantiate(_prefab)
                    .SetPool(_objectPool)
                    .Destroy();
            }

            RocketView view = _objectPool.Get<RocketView>();
            RocketPresenter presenter = _rocketPresenterFactory.Create(view, rocket);
            view.Construct(presenter);

            return view;
        }
    }
}