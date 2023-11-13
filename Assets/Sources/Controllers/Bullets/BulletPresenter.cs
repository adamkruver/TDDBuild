using System;
using Sources.Domain.Bullets;
using Sources.Domain.HealthPoints;
using Sources.PresentationInterfaces.Views.Bullets;
using UnityEngine;

namespace Sources.Controllers.Bullets
{
    public class BulletPresenter : PresenterBase
    {
        private readonly IBulletView _view;
        private readonly IBullet _bullet;

        public BulletPresenter(IBulletView view, IBullet bullet)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _bullet = bullet ?? throw new ArgumentNullException(nameof(bullet));
        }

        public void Shoot(IDamageable damageable, Vector3 direction) => 
            _bullet.Attack(damageable, direction);
        
        public float Speed => _bullet.Speed;
    }
}