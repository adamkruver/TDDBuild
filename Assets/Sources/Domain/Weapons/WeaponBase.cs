using Sources.Domain.Bullets;
using Sources.InfrastructureInterfaces.Services.Times;

namespace Sources.Domain.Weapons
{
    public abstract class WeaponBase : IWeapon
    {
        private readonly ITimeService _timeSource;

        private float _lastShootTime;

        protected WeaponBase(
            IBullet bullet,
            ITimeService timeSource,
            float cooldown,
            float minFireDistance,
            float maxFireDistance,
            float horizontalRotationSpeed,
            float verticalRotationSpeed
        )
        {
            _timeSource = timeSource;
            Bullet = bullet;
            Cooldown = cooldown;
            MinFireDistance = minFireDistance;
            MaxFireDistance = maxFireDistance;
            HorizontalRotationSpeed = horizontalRotationSpeed;
            VerticalRotationSpeed = verticalRotationSpeed;
        }

        protected WeaponBase(
            IBullet bullet,
            ITimeService timeService,
            WeaponFab weaponFab
        )
            : this(
                bullet,
                timeService,
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
        public float MaxFireDistance { get; }
        public float HorizontalRotationSpeed { get; }
        public float VerticalRotationSpeed { get; }
        public float Cooldown { get; }

        public bool CanShoot => _lastShootTime < _timeSource.Time - Cooldown;

        public void Fire() =>
            _lastShootTime = _timeSource.Time;
    }
}