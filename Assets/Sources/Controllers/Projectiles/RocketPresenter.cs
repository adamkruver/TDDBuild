using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Sources.Domain.Audio;
using Sources.Domain.HealthPoints;
using Sources.Domain.Projectiles;
using Sources.PresentationInterfaces.Audio;
using Sources.PresentationInterfaces.Views.Bullets;
using UnityEngine;

namespace Sources.Controllers.Projectiles
{
    public class RocketPresenter : PresenterBase
    {
        private readonly Rocket _rocket;
        private readonly IRocketView _view;
        private readonly IAudioMixerView _audioMixer;
        
        private CancellationTokenSource _cancellationTokenSource;

        public RocketPresenter(IRocketView view, Rocket rocket, IAudioMixerView audioMixer)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _rocket = rocket ?? throw new ArgumentNullException(nameof(rocket));
            _audioMixer = audioMixer ?? throw new ArgumentNullException(nameof(audioMixer));
        }

        public void Shoot()
        {
            _cancellationTokenSource?.Cancel();
            _cancellationTokenSource = new CancellationTokenSource();
            
            LaunchRocketAsync(_cancellationTokenSource.Token);
        }

        private async void LaunchRocketAsync(CancellationToken cancellationToken)
        {
            _view.Detach();
            
            Vector3 startPosition = _view.Position;
            Vector3 endPosition = _rocket.Destination;
            endPosition.y = 0;
            
            Vector3 flyDirection = endPosition - startPosition;
            Vector3 lastPosition = startPosition;

            
            _view.StartFireEngine();
            // AudioPoint engineAudio = new AudioPoint(_view.EngineAudioClip, _view.Position);
            // _audioMixer.Play(engineAudio);
            
            float progress = 0;

            while (progress < 1)
            {
                progress += Time.deltaTime / _view.FlyDuration * (1 + _view.SpeedCurve.Evaluate(progress));

                progress = Mathf.Clamp01(progress);
                
                Vector3 position = startPosition + flyDirection * progress + Vector3.up * _view.FlyHeight * _view.HeightCurve.Evaluate(progress);
                _view.SetPosition(position);
                _view.SetDirection((position - lastPosition));
                lastPosition = position;
                
                await UniTask.Yield(cancellationToken);
            }
            
            _view.FinishFireEngine();
            _view.Explode();
            AttackTargets();
            // _audioMixer.Stop(engineAudio);
            // _audioMixer.Play(new AudioPoint(_view.ExplosionAudioClip, _view.Position));
        }

        private void AttackTargets()
        {
            foreach (IDamageable target in _view.GetTargets(10)) 
                _rocket.Attack(target, Vector3.zero);
        }
    }
}