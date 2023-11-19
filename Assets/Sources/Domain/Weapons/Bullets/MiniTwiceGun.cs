using Sources.Domain.Constructs;
using Sources.Domain.Systems.Upgrades;
using Sources.InfrastructureInterfaces.Services.Times;

namespace Sources.Domain.Weapons.Bullets
{
    public class MiniTwiceGun : BulletWeaponBase, IConstructable
    {
        public MiniTwiceGun(
            ITimeService timeService,
            WeaponFab weaponFab,
            UpgradeSystem upgradeSystem
        ) : base(
            shootPoints: 2,
            timeService,
            weaponFab,
            upgradeSystem
        )
        {
        }
    }
}