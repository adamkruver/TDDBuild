using Sources.Controllers.Audio;
using Sources.Infrastructure.Factories.Controllers.Audio;
using Sources.Infrastructure.ObjectPools;
using Sources.Presentation.Audio;
using UnityEngine;

namespace Sources.Infrastructure.Factories.Presentation.Audio
{
    public class AudioMixerViewFactory
    {
        private readonly AudioMixerPresenterFactory _audioMixerPresenterFactory;
        private readonly ObjectPool _objectPool;

        public AudioMixerViewFactory(AudioMixerPresenterFactory audioMixerPresenterFactory)
        {
            _audioMixerPresenterFactory = audioMixerPresenterFactory;
        }

        public AudioMixerView Create()
        {
            AudioMixerView audioMixerView = new GameObject("AudioMixerView").AddComponent<AudioMixerView>();
            AudioMixerPresenter presenter = _audioMixerPresenterFactory.Create(audioMixerView);
            audioMixerView.Construct(presenter);

            return audioMixerView;
        }
    }
}