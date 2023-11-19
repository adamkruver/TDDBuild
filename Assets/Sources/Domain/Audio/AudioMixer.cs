using System.Collections.Generic;
using Sources.InfrastructureInterfaces.Factories.Presentation.Audio;
using Sources.Presentation.Audio;
using Sources.PresentationInterfaces.Audio;
using UnityEngine;

namespace Sources.Domain.Audio
{
    public class AudioMixer
    {
        private readonly IAudioSourceViewFactory _audioSourceViewFactory;
        private readonly List<IAudioSourceView> _sources = new List<IAudioSourceView>();
        private float _thresholdDistance;

        public AudioMixer(IAudioSourceViewFactory audioSourceViewFactory)
        {
            _audioSourceViewFactory = audioSourceViewFactory;
        }

        public void Play(AudioPoint audioPoint)
        {
            _thresholdDistance = .5f;

            var audioSources = GetAudioFromPoint(audioPoint.Position, _thresholdDistance);

            foreach (AudioSourceView audio in audioSources)
            {
                audio.Source.Stop();
                _sources.Remove(audio);
            }

            var audioSource = _audioSourceViewFactory.Create(audioPoint.Position);

            audioSource.Source.clip = audioPoint.Clip;
            _sources.Add(audioSource);
            audioSource.Play();
        }

        private IEnumerable<AudioSourceView> GetAudioFromPoint(Vector3 position, float radius)
        {
            List<AudioSourceView> audioPoints = new List<AudioSourceView>();

            foreach (AudioSourceView source in _sources)
                if (Vector3.Distance(source.Position, position) <= radius)
                    audioPoints.Add(source);

            return audioPoints;
        }

        public void Stop(AudioPoint audioPoint)
        {
            // TODO: stop audio
        }
    }
}