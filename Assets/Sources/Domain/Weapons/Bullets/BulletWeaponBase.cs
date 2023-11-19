using System;
using Sources.Domain.Projectiles;
using Sources.Domain.Systems.Upgrades;
using Sources.InfrastructureInterfaces.Services.Times;

namespace Sources.Domain.Weapons.Bullets
{
    public abstract class BulletWeaponBase : WeaponBase, IBulletWeapon
    {
        protected BulletWeaponBase(
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

        public Bullet[] Bullets { get; private set; }

        public void SetBullets(Bullet[] bullets) =>
            Bullets = bullets ?? throw new ArgumentNullException(nameof(bullets));

        protected override void OnShoot()
        {
            for (int i = 0; i < ShootAtOnce; i++)
            {
                InvokeShootingEvent();
                SetNextShootPoint();
            }
        }

        protected override float GetShootSpeed() =>
            Bullets[ShootPointIndex].Speed;
    }
}