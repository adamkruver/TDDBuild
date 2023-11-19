using Sources.Domain.Constructs;
using Sources.Domain.Systems.Upgrades;
using Sources.InfrastructureInterfaces.Services.Times;

namespace Sources.Domain.Weapons.Bullets
{
    public class QuadGun : BulletWeaponBase, IConstructable
    {
        public QuadGun(
            ITimeService timeService,
            WeaponFab weaponFab,
            UpgradeSystem upgradeSystem
        ) : base(
            shootPoints: 4,
            timeService, 
            weaponFab,
            upgradeSystem
        )
        {
        }
    }
}