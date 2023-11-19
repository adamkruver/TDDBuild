using System;
using Sources.Controllers.Projectiles;
using Sources.Domain.Projectiles;
using Sources.PresentationInterfaces.Audio;
using Sources.PresentationInterfaces.Views.Bullets;

namespace Sources.Infrastructure.Factories.Controllers.Projectiles
{
    public class BulletPresenterFactory
    {
        private readonly IAudioMixerView _audioMixer;

        public BulletPresenterFactory(IAudioMixerView audioMixer) =>
            _audioMixer = audioMixer ?? throw new ArgumentNullException(nameof(audioMixer));

        public BulletPresenter Create(IBulletView view, Bullet bullet) =>
            new BulletPresenter(view, bullet, _audioMixer);
    }
}