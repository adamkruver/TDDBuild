using Sources.Domain.Constructs;
using Sources.Domain.Systems.Upgrades;
using Sources.InfrastructureInterfaces.Services.Times;

namespace Sources.Domain.Weapons.Bullets
{
    public class SingleGun : BulletWeaponBase, IConstructable
    {
        public SingleGun(
            ITimeService timeService,
            WeaponFab weaponFab,
            UpgradeSystem upgradeSystem
        ) : base(
            shootPoints: 1,
            timeService,
            weaponFab, 
            upgradeSystem
        )
        {
        }
    }
}