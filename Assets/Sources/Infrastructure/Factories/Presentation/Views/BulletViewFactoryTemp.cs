using Sources.Domain.Projectiles;
using Sources.Infrastructure.Factories.Controllers.Projectiles;
using Sources.Presentation.Audio;
using Sources.Presentation.Views.Bullets;
using Sources.PresentationInterfaces.Views.Bullets;

namespace Sources.Infrastructure.Factories.Presentation.Views
{
    public class BulletViewFactoryTemp
    {
        private readonly BulletPresenterFactory _bulletPresenterFactory;
        private readonly RocketPresenterFactory _rocketPresenterFactory;
        private readonly AudioMixerView _audioMixerView;

        public BulletViewFactoryTemp(
            BulletPresenterFactory bulletPresenterFactory,
            RocketPresenterFactory rocketPresenterFactory,
            AudioMixerView audioMixerView
        )
        {
            _bulletPresenterFactory = bulletPresenterFactory;
            _rocketPresenterFactory = rocketPresenterFactory;
            _audioMixerView = audioMixerView;
        }

        public IProjectileView Create(IProjectileView bulletViewBase, IBullet bullet)
        {
             // if (bullet is Rocket rocket)
             //     bulletViewBase.Construct(_rocketPresenterFactory.Create(bulletViewBase as RocketView, rocket, _audioMixerView));
             // else
             //     bulletViewBase.Construct(_bulletPresenterFactory.Create(bulletViewBase, bullet, _audioMixerView));

            return bulletViewBase;
        }
    }
}