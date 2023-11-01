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

        public BulletView Create(BulletView bulletView, IBullet bullet)
        {
            var presenter = _bulletPresenterFactory.Create(bulletView, bullet);
            bulletView.Construct(presenter);

            return bulletView;
        }
    }
}