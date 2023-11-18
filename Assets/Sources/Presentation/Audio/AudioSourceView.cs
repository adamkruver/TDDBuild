using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Sources.Presentation.Components;
using Sources.PresentationInterfaces.Audio;
using UnityEngine;

namespace Sources.Presentation.Audio
{
    public class AudioSourceView : PoolableBehaviour, IAudioSourceView
    {
        private CancellationTokenSource _cancellationTokenSource;
        public AudioSource Source { get; private set; }
        public Vector3 Position { get; private set; }

        public async void Play()
        {
            _cancellationTokenSource?.Cancel();
            _cancellationTokenSource = new CancellationTokenSource();
            await Play(_cancellationTokenSource.Token);
        }
        
        private async UniTask Play(CancellationToken cancellationToken)
        {
            Source.Play();
            await UniTask.Delay(TimeSpan.FromSeconds(Source.clip.length), cancellationToken: cancellationToken);
            
            if(cancellationToken.IsCancellationRequested == false)
                Stop();
        }

        public void Show() => 
            gameObject.SetActive(true);

        public void Stop()
        {
            Source.Stop();
            Destroy();
        }

        private void Awake() =>
            Source = gameObject.AddComponent<AudioSource>();

        public void SetPosition(Vector3 position) =>
            Position = position;
    }
}