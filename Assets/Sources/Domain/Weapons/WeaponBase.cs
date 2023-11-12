using System;
using Sources.Domain.Bullets;
using Sources.Domain.Systems.Upgrades;
using Sources.Frameworks.LiveDatas;
using Sources.InfrastructureInterfaces.Services.Times;
using UnityEngine;

namespace Sources.Domain.Weapons
{
    public abstract class WeaponBase : IWeapon
    {
        private const float MinCooldown = .2f;

        private readonly ITimeService _timeSource;
        private readonly float _baseCooldown;
        private readonly float _baseMaxFireDistance;
        private readonly LiveData<float> _maxFireDistance;
        private readonly LiveData<float> _cooldown;
        private readonly int _shootAtOnce;

        private int _currentBulletId;
        private float _lastShootTime;

        private WeaponBase(
            IBullet[] bullets,
            ITimeService timeSource,
            UpgradeSystem upgradeSystem,
            float cooldown,
            float minFireDistance,
            float maxFireDistance,
            float horizontalRotationSpeed,
            float verticalRotationSpeed,
            int shootAtOnce
        )
        {
            _timeSource = timeSource;
            Bullets = bullets;
            _baseCooldown = cooldown;
            MinFireDistance = minFireDistance;
            _baseMaxFireDistance = maxFireDistance;
            _shootAtOnce = shootAtOnce;
            HorizontalRotationSpeed = horizontalRotationSpeed;
            VerticalRotationSpeed = verticalRotationSpeed;
            _maxFireDistance = upgradeSystem.MaxFireDistance.Value;
            _cooldown = upgradeSystem.Cooldown.Value;

            Debug.Log(_baseCooldown + _cooldown.Value);
        }

        protected WeaponBase(
            IBullet[] bullets,
            ITimeService timeService,
            WeaponFab weaponFab,
            UpgradeSystem upgradeSystem
        )
            : this(
                bullets,
                timeService,
                upgradeSystem,
                weaponFab.Cooldown,
                weaponFab.MinFireDistance,
                weaponFab.MaxFireDistance,
                weaponFab.HorizontalRotationSpeed,
                weaponFab.VerticalRotationSpeed,
                weaponFab.ShootAtOnce
            )
        {
        }

        public event Action Shooting;
        public IBullet[] Bullets { get; }
        public IBullet Bullet => Bullets[_currentBulletId];
        public float MinFireDistance { get; }
        public float MaxFireDistance => _baseMaxFireDistance + _maxFireDistance.Value;
        public float HorizontalRotationSpeed { get; }
        public float VerticalRotationSpeed { get; }
        public int BulletId => _currentBulletId;
        public float Cooldown => Mathf.Max(_baseCooldown + _cooldown.Value, MinCooldown);

        public bool CanShoot => _lastShootTime < _timeSource.Time - Cooldown;

        public virtual void Shoot()
        {
            _lastShootTime = _timeSource.Time;

            for (int i = 0; i < _shootAtOnce; i++)
            {
                Shooting?.Invoke();
                
                if (++_currentBulletId == Bullets.Length)
                    _currentBulletId = 0;
            }
        }
    }
}