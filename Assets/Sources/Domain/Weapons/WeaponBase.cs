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

        private float _lastShootTime;
        private readonly LiveData<float> _maxFireDistance;
        private readonly LiveData<float> _cooldown;

        private WeaponBase(
            IBullet bullet,
            ITimeService timeSource,
            UpgradeSystem upgradeSystem,
            float cooldown,
            float minFireDistance,
            float maxFireDistance,
            float horizontalRotationSpeed,
            float verticalRotationSpeed
        )
        {
            _timeSource = timeSource;
            Bullet = bullet;
            _baseCooldown = cooldown;
            MinFireDistance = minFireDistance;
            _baseMaxFireDistance = maxFireDistance;
            HorizontalRotationSpeed = horizontalRotationSpeed;
            VerticalRotationSpeed = verticalRotationSpeed;
            _maxFireDistance = upgradeSystem.MaxFireDistance.Value;
            _cooldown = upgradeSystem.Cooldown.Value;
            
            Debug.Log(_baseCooldown + _cooldown.Value);
        }

        protected WeaponBase(
            IBullet bullet,
            ITimeService timeService,
            WeaponFab weaponFab,
            UpgradeSystem upgradeSystem
        )
            : this(
                bullet,
                timeService,
                upgradeSystem,
                weaponFab.Cooldown,
                weaponFab.MinFireDistance,
                weaponFab.MaxFireDistance,
                weaponFab.HorizontalRotationSpeed,
                weaponFab.VerticalRotationSpeed
            )
        {
        }

        public IBullet Bullet { get; }
        public float MinFireDistance { get; }
        public float MaxFireDistance => _baseMaxFireDistance + _maxFireDistance.Value;
        public float HorizontalRotationSpeed { get; }
        public float VerticalRotationSpeed { get; }
        public float Cooldown => Mathf.Max(_baseCooldown + _cooldown.Value, MinCooldown);

        public bool CanShoot => _lastShootTime < _timeSource.Time - Cooldown;

        public void Shoot() =>
            _lastShootTime = _timeSource.Time;
    }
}