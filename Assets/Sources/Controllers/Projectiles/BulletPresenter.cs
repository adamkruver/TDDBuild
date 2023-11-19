using System;
using Sources.Domain.Audio;
using Sources.Domain.HealthPoints;
using Sources.Domain.Projectiles;
using Sources.PresentationInterfaces.Audio;
using Sources.PresentationInterfaces.Views.Bullets;
using UnityEngine;

namespace Sources.Controllers.Projectiles
{
    public class BulletPresenter : PresenterBase
    {
        private readonly IBulletView _view;
        private readonly Bullet _bullet;
        private readonly IAudioMixerView _audioMixer;

        public BulletPresenter(IBulletView view, Bullet bullet, IAudioMixerView audioMixer)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _bullet = bullet ?? throw new ArgumentNullException(nameof(bullet));
            _audioMixer = audioMixer ?? throw new ArgumentNullException(nameof(audioMixer));

            SetSpeed();
        }

        public void Shoot()
        {
            _view.StartProjectile();
            _audioMixer.Play(new AudioPoint(_view.ShootAudioClip, _view.Position));
        }

        public void Collide(Func<IDamageable> targetProvider)
        {
            if (targetProvider is null)
                throw new ArgumentNullException(nameof(targetProvider));

            IDamageable target = targetProvider.Invoke();

            _view.FinishProjectile();

            if (target == null)
                return;

            OnReachTarget(target, _view.Position);
        }

        private void OnReachTarget(IDamageable target, Vector3 direction) =>
            _bullet.Attack(target, direction);

        private void SetSpeed() =>
            _view.SetSpeed(_bullet.Speed);
    }
}