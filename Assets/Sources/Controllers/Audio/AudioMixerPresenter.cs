using Sources.Domain.Audio;
using Sources.PresentationInterfaces.Audio;

namespace Sources.Controllers.Audio
{
    public class AudioMixerPresenter : PresenterBase
    {
        private readonly IAudioMixerView _view;
        private readonly AudioMixer _mixer;

        public AudioMixerPresenter(IAudioMixerView view)
        {
            _view = view;
            _mixer = new AudioMixer();
        }

        public void Play(AudioPoint audioPoint)
        {
            _mixer.Play(audioPoint);
        }
    }
}