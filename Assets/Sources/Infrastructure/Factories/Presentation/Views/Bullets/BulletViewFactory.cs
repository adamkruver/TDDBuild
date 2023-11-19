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
    public class BulletViewFactory : IBulletViewFactory
    {
        private readonly BulletPresenterFactory _bulletPresenterFactory;
        private readonly ObjectPool _objectPool;
        private readonly BulletView _prefab;

        public BulletViewFactory(
            BulletPresenterFactory bulletPresenterFactory,
            IResourceProvider resourceProvider
        )
        {
            _bulletPresenterFactory =
                bulletPresenterFactory ?? throw new ArgumentNullException(nameof(bulletPresenterFactory));

            if (resourceProvider == null)
                throw new ArgumentNullException(nameof(resourceProvider));

            _objectPool = new ObjectPool(" ==== Bullet Pool ==== ");

            _prefab = resourceProvider.Load<BulletView>("Views/Bullets/BulletView") ??
                      throw new InvalidOperationException(nameof(resourceProvider.Load));
        }

        public IProjectileView Create(Bullet bullet)
        {
            if (_objectPool.Contain<BulletView>() == false)
            {
                Object.Instantiate(_prefab)
                    .SetPool(_objectPool)
                    .Destroy();
            }

            BulletView view = _objectPool.Get<BulletView>();
            BulletPresenter presenter = _bulletPresenterFactory.Create(view, bullet);

            view.Construct(presenter);

            return view;
        }
    }
}