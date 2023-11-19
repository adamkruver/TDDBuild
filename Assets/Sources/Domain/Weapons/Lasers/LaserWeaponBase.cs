using System;
using Sources.Domain.Projectiles;
using Sources.Domain.Systems.Upgrades;
using Sources.InfrastructureInterfaces.Services.Times;

namespace Sources.Domain.Weapons.Lasers
{
    public abstract class LaserWeaponBase : WeaponBase, ILaserWeapon
    {
        protected LaserWeaponBase(
            int shootPoints,
            ITimeService timeService,
            WeaponFab weaponFab,
            UpgradeSystem upgradeSystem
        ) : base(
            shootPoints,
            timeService,
            weaponFab,
            upgradeSystem
        )
        {
        }

        public Laser[] Lasers { get; private set; }

        public void SetLasers(Laser[] lasers) =>
            Lasers = lasers ?? throw new ArgumentNullException(nameof(lasers));

        protected override void OnShoot()
        {
            for (int i = 0; i < ShootAtOnce; i++)
            {
                InvokeShootingEvent();
                SetNextShootPoint();
            }
        }

        protected override float GetShootSpeed() =>
            Lasers[ShootPointIndex].Speed;
    }
}