using Sources.Domain.Bullets;
using Sources.Infrastructure.Factories.Controllers.Bullets;
using Sources.Presentation.Views.Bullets;

namespace Sources.Infrastructure.Factories.Presentation.Views
{
    public class BulletViewFactory
    {
        private readonly BulletPresenterFactory _bulletPresenterFactory;
        private readonly RocketPresenterFactory _rocketPresenterFactory;

        public BulletViewFactory(
            BulletPresenterFactory bulletPresenterFactory,
            RocketPresenterFactory rocketPresenterFactory
        )
        {
            _bulletPresenterFactory = bulletPresenterFactory;
            _rocketPresenterFactory = rocketPresenterFactory;
        }

        public BulletViewBase Create(BulletViewBase bulletViewBase, IBullet bullet)
        {
            if (bullet is Rocket rocket)
                bulletViewBase.Construct(_rocketPresenterFactory.Create(bulletViewBase, rocket));
            else
                bulletViewBase.Construct(_bulletPresenterFactory.Create(bulletViewBase, bullet));

            return bulletViewBase;
        }
    }
}