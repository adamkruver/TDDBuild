using System;
using Sources.Controllers.Projectiles;
using Sources.Domain.Projectiles;
using Sources.Presentation.Views.Bullets;
using Sources.PresentationInterfaces.Audio;

namespace Sources.Infrastructure.Factories.Controllers.Projectiles
{
    public class RocketPresenterFactory
    {
        private readonly IAudioMixerView _audioMixer;

        public RocketPresenterFactory(IAudioMixerView audioMixer) =>
            _audioMixer = audioMixer ?? throw new ArgumentNullException(nameof(audioMixer));

        public RocketPresenter Create(RocketView view, Rocket rocket) =>
            new RocketPresenter(view, rocket, _audioMixer);
    }
}