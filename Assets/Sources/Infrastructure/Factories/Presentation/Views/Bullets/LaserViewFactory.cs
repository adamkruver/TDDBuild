using System;
using Sources.Domain.Projectiles;
using Sources.Domain.Weapons.Lasers;
using Sources.Infrastructure.Factories.Controllers.Projectiles;
using Sources.Infrastructure.ObjectPools;
using Sources.InfrastructureInterfaces.Factories.Presentation.Projectiles;
using Sources.InfrastructureInterfaces.Providers;
using Sources.Presentation.Views.Bullets;
using Sources.PresentationInterfaces.Views.Bullets;
using Object = UnityEngine.Object;

namespace Sources.Infrastructure.Factories.Presentation.Views.Bullets
{
    public class LaserViewFactory : ILaserViewFactory
    {
        private readonly LaserPresenterFactory _laserPresenterFactory;
        private readonly ObjectPool _objectPool;
        private readonly LaserView _prefab;

        public LaserViewFactory(
            LaserPresenterFactory laserPresenterFactory,
            IResourceProvider resourceProvider
        )
        {
            _laserPresenterFactory =
                laserPresenterFactory ?? throw new ArgumentNullException(nameof(laserPresenterFactory));

            if (resourceProvider == null)
                throw new ArgumentNullException(nameof(resourceProvider));

            _objectPool = new ObjectPool(" ==== Laser Pool ==== ");

            _prefab = resourceProvider.Load<LaserView>("Views/Bullets/LaserView") ??
                      throw new InvalidOperationException(nameof(resourceProvider.Load));
        }


        public IProjectileView Create(Laser laser)
        {
            if (_objectPool.Contain<LaserView>() == false)
            {
                Object.Instantiate(_prefab)
                    .SetPool(_objectPool)
                    .Destroy();
            }

            LaserView laserView = _objectPool.Get<LaserView>();
            var presenter = _laserPresenterFactory.Create(laserView, laser);

            laserView.Construct(presenter);

            return laserView;
        }
    }
}