using Sources.Domain.Audio;

namespace Sources.PresentationInterfaces.Audio
{
    public interface IAudioMixerView
    {
        void Play(AudioPoint audioPoint);
        void Stop(AudioPoint audioPoint);
    }
}