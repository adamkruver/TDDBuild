using Sources.Domain.Bullets;
using Sources.Infrastructure.Factories.Controllers.Bullets;
using Sources.Presentation.Audio;
using Sources.Presentation.Views.Bullets;

namespace Sources.Infrastructure.Factories.Presentation.Views
{
    public class BulletViewFactory
    {
        private readonly BulletPresenterFactory _bulletPresenterFactory;
        private readonly RocketPresenterFactory _rocketPresenterFactory;
        private readonly AudioMixerView _audioMixerView;

        public BulletViewFactory(
            BulletPresenterFactory bulletPresenterFactory,
            RocketPresenterFactory rocketPresenterFactory,
            AudioMixerView audioMixerView
        )
        {
            _bulletPresenterFactory = bulletPresenterFactory;
            _rocketPresenterFactory = rocketPresenterFactory;
            _audioMixerView = audioMixerView;
        }

        public BulletViewBase Create(BulletViewBase bulletViewBase, IBullet bullet)
        {
            if (bullet is Rocket rocket)
                bulletViewBase.Construct(_rocketPresenterFactory.Create(bulletViewBase as RocketView, rocket, _audioMixerView));
            else
                bulletViewBase.Construct(_bulletPresenterFactory.Create(bulletViewBase, bullet, _audioMixerView));

            return bulletViewBase;
        }
    }
}