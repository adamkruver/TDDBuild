using UnityEngine;

namespace Sources.PresentationInterfaces.Audio
{
    public interface IAudioSourceView
    {
        AudioSource Source { get; }
        Vector3 Position { get; }
        void Play();
        void Stop();
    }
}