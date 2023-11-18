using Sources.Controllers.Audio;
using Sources.Domain.Audio;
using Sources.Infrastructure.Factories.Presentation.Audio;
using Sources.InfrastructureInterfaces.Factories.Presentation.Audio;
using Sources.PresentationInterfaces.Audio;

namespace Sources.Infrastructure.Factories.Controllers.Audio
{
    public class AudioMixerPresenterFactory
    {
        private readonly IAudioSourceViewFactory _audioSourceViewFactory;

        public AudioMixerPresenterFactory() => 
            _audioSourceViewFactory = new AudioSourceViewFactory();

        public AudioMixerPresenter Create(IAudioMixerView view) =>
            new AudioMixerPresenter(view, new AudioMixer(_audioSourceViewFactory));
    }
}