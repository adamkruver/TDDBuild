using System.Collections.Generic;
using UnityEngine;

namespace Sources.Domain.Audio
{
    public class AudioMixer
    {
        private readonly List<AudioSourcePoint> _sources = new List<AudioSourcePoint>();
        private float _thresholdDistance;

        public void Play(AudioPoint audioPoint)
        {
            _thresholdDistance = .5f;

            var audioSources = GetAudioFromPoint(audioPoint.Position, _thresholdDistance);

            foreach (AudioSourcePoint audio in audioSources)
            {
                audio.Source.Stop();
                _sources.Remove(audio);
            }

            var audioSource = new GameObject("OneShootAudioClip").AddComponent<AudioSource>();
            audioSource.clip = audioPoint.Clip;
            _sources.Add(new AudioSourcePoint(audioSource, audioPoint.Position));
            audioSource.Play();
        }

        private IEnumerable<AudioSourcePoint> GetAudioFromPoint(Vector3 position, float radius)
        {
            List<AudioSourcePoint> audioPoints = new List<AudioSourcePoint>();

            foreach (AudioSourcePoint source in _sources)
                if (Vector3.Distance(source.Position, position) <= radius)
                    audioPoints.Add(source);

            return audioPoints;
        }
    }
}