using System;
using Sources.Domain.Systems.Upgrades;
using Sources.Frameworks.LiveDatas;
using Sources.InfrastructureInterfaces.Services.Times;
using UnityEngine;

namespace Sources.Domain.Weapons
{
    public abstract class WeaponBase : IWeapon
    {
        private const float MinCooldown = .2f;

        private readonly int _shootPoints;
        private readonly ITimeService _timeSource;
        private readonly float _baseCooldown;
        private readonly float _baseMaxFireDistance;
        private readonly LiveData<float> _maxFireDistance;
        private readonly LiveData<float> _cooldown;

        private float _lastShootTime;

        private WeaponBase(
            int shootPoints,
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
            _shootPoints = shootPoints;
            _timeSource = timeSource;
            _baseCooldown = cooldown;
            MinFireDistance = minFireDistance;
            _baseMaxFireDistance = maxFireDistance;
            ShootAtOnce = shootAtOnce;
            HorizontalRotationSpeed = horizontalRotationSpeed;
            VerticalRotationSpeed = verticalRotationSpeed;
            _maxFireDistance = upgradeSystem.MaxFireDistance.Value;
            _cooldown = upgradeSystem.Cooldown.Value;
            _lastShootTime = _timeSource.Time - Cooldown;
        }

        protected WeaponBase(
            int shootPoints,
            ITimeService timeService,
            WeaponFab weaponFab,
            UpgradeSystem upgradeSystem
        )
            : this(
                shootPoints,
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
        public event Action ShootPointChanged;

        public int ShootAtOnce { get; }
        public int ShootPointIndex { get; private set; }
        public float MinFireDistance { get; }
        public float MaxFireDistance => _baseMaxFireDistance + _maxFireDistance.Value;
        public float HorizontalRotationSpeed { get; }
        public float VerticalRotationSpeed { get; }
        public float ShootSpeed => GetShootSpeed();
        public float Cooldown => Mathf.Max(_baseCooldown + _cooldown.Value, MinCooldown);

        public bool CanShoot => _lastShootTime < _timeSource.Time - Cooldown;

        public virtual void Shoot()
        {
            _lastShootTime = _timeSource.Time;
            OnShoot();
        }

        protected abstract void OnShoot();

        protected abstract float GetShootSpeed();

        protected void InvokeShootingEvent() =>
            Shooting?.Invoke();

        protected void SetNextShootPoint()
        {
            ShootPointIndex++;
            ShootPointIndex %= _shootPoints;

            ShootPointChanged?.Invoke();
        }
    }
}