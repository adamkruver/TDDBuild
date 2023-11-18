using UnityEngine;

namespace Sources.Domain.Audio
{
    public class AudioSourcePoint
    {
        public AudioSourcePoint(AudioSource source, Vector3 position)
        {
            Source = source;
            Position = position;
        }

        public AudioSource Source { get; }
        public Vector3 Position { get; }
    }
}