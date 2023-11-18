using System;
using Sources.Domain.Audio;
using Sources.Domain.Bullets;
using Sources.Domain.HealthPoints;
using Sources.Presentation.Audio;
using Sources.PresentationInterfaces.Views.Bullets;
using UnityEngine;

namespace Sources.Controllers.Bullets
{
    public class BulletPresenter : PresenterBase
    {
        private readonly IBulletView _view;
        private readonly IBullet _bullet;
        private readonly AudioMixerView _audioMixerView;

        public BulletPresenter(IBulletView view, IBullet bullet, AudioMixerView audioMixerView)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _bullet = bullet ?? throw new ArgumentNullException(nameof(bullet));
            _audioMixerView = audioMixerView;
        }

        public void Shoot(IDamageable damageable, Vector3 direction) => 
            _bullet.Attack(damageable, direction);

        public void PlaySound() => 
            _audioMixerView.Play(new AudioPoint(_view.ShootAudioClip, _view.Position));

        public float Speed => _bullet.Speed;
    }
}