using System;
using JetBrains.Annotations;
using Sources.Controllers.Projectiles;
using Sources.Domain.Projectiles;
using Sources.PresentationInterfaces.Audio;
using Sources.PresentationInterfaces.Views.Bullets;

namespace Sources.Infrastructure.Factories.Controllers.Projectiles
{
    public class LaserPresenterFactory
    {
        private readonly IAudioMixerView _audioMixer;

        public LaserPresenterFactory([NotNull] IAudioMixerView audioMixer) =>
            _audioMixer = audioMixer ?? throw new ArgumentNullException(nameof(audioMixer));

        public LaserPresenter Create(ILaserView view, Laser laser) =>
            new LaserPresenter(view, laser, _audioMixer);
    }
}