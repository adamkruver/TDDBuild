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
    public class LaserPresenter : PresenterBase
    {
        private readonly ILaserView _view;
        private readonly Laser _laser;
        private readonly IAudioMixerView _audioMixer;

        private CancellationTokenSource _cancellationTokenSource;

        public LaserPresenter(
            ILaserView view,
            Laser laser,
            IAudioMixerView audioMixer
        )
        {
            _view = view;
            _laser = laser;
            _audioMixer = audioMixer;
        }

        public void Shoot()
        {
            _cancellationTokenSource?.Cancel();
            _cancellationTokenSource = new CancellationTokenSource();

            Vector3 from = _view.Position;
            Ray ray = new Ray(_view.Position, _view.Forward);
            (Vector3 to, IDamageable target) = _view.Raycast(ray, Mathf.Infinity);

            ShootAsync(target, from, to, _cancellationTokenSource.Token);
        }

        private async void ShootAsync(IDamageable target, Vector3 from, Vector3 to, CancellationToken cancellationToken)
        {
            if (_view.Duration == 0)
                throw new DivideByZeroException(nameof(_view.Duration));

            Vector3 direction = (to - from).normalized;

            _view.SetLaserPositions(from, to);
            _view.StartDistortion(from, to);
            _view.StartBurnTarget(to, -direction);

            PlaySound();
            
            if(target != null)
                _laser.Attack(target, -direction);

            await StartShoot(cancellationToken);
        }

        private async UniTask StartShoot(CancellationToken cancellationToken)
        {
            for (float time = 0; time < 1f; time += Time.deltaTime / _view.Duration)
            {
                EvaluateShoot(time);

                await UniTask.Yield(cancellationToken);
            }
        }

        private void EvaluateShoot(float time)
        {
            time = Mathf.Clamp01(time);
            _view.SetLaserWidth(_view.WidthCurve.Evaluate(time) * _view.Duration * _view.NativeWidth);
        }

        private void PlaySound() =>
            _audioMixer.Play(new AudioPoint(_view.AudioClip, _view.Position));
    }
}