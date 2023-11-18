using Sources.Infrastructure.ObjectPools;
using Sources.InfrastructureInterfaces.Factories.Presentation.Audio;
using Sources.Presentation.Audio;
using Sources.PresentationInterfaces.Audio;
using UnityEngine;

namespace Sources.Infrastructure.Factories.Presentation.Audio
{
    public class AudioSourceViewFactory : IAudioSourceViewFactory
    {
        private readonly ObjectPool _objectPool;

        public AudioSourceViewFactory()
        {
            _objectPool = new ObjectPool(" ==== Audio Source Pool ==== ");
        }

        public IAudioSourceView Create(Vector3 position)
        {
            if (_objectPool.Contain<AudioSourceView>() == false)
            {
                var audioSource = new GameObject("OneShootAudioClip")
                    .AddComponent<AudioSourceView>();

                audioSource.Source.playOnAwake = false;

                audioSource
                    .SetPool(_objectPool)
                    .Destroy();
            }

            var view = _objectPool.Get<AudioSourceView>();
            view.SetPosition(position);
            view.Show();

            return view;
        }
    }
}