using Sources.Controllers.Audio;
using Sources.PresentationInterfaces.Audio;

namespace Sources.Infrastructure.Factories.Controllers.Audio
{
    public class AudioMixerPresenterFactory
    {
        public AudioMixerPresenter Create(IAudioMixerView view) =>
            new AudioMixerPresenter(view);
    }
}