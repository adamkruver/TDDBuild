using Sources.Controllers.Audio;
using Sources.Domain.Audio;
using Sources.Presentation.Views;
using Sources.PresentationInterfaces.Audio;

namespace Sources.Presentation.Audio
{
    public class AudioMixerView : PresentationViewBase<AudioMixerPresenter>, IAudioMixerView
    {
        public void Play(AudioPoint audioPoint)
        {
            Presenter?.Play(audioPoint);
        }
    }
}