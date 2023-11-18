using UnityEngine;

namespace Sources.Domain.Audio
{
    public class AudioPoint
    {
        public AudioPoint(AudioClip clip, Vector3 position)
        {
            Clip = clip;
            Position = position;
        }

        public AudioClip Clip { get; }
        public Vector3 Position { get; }
    }
}