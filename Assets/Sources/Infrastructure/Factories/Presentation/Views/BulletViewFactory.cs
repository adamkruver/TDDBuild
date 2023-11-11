using Sources.Domain.Bullets;
using Sources.Infrastructure.Factories.Controllers.Bullets;
using Sources.Presentation.Views.Bullets;

namespace Sources.Infrastructure.Factories.Presentation.Views
{
    public class BulletViewFactory
    {
        private readonly BulletPresenterFactory _bulletPresenterFactory;

        public BulletViewFactory(BulletPresenterFactory bulletPresenterFactory) =>
            _bulletPresenterFactory = bulletPresenterFactory;

        public BulletViewBase Create(BulletViewBase bulletViewBase, IBullet bullet)
        {
            var presenter = _bulletPresenterFactory.Create(bulletViewBase, bullet);
            bulletViewBase.Construct(presenter);

            return bulletViewBase;
        }
    }
}