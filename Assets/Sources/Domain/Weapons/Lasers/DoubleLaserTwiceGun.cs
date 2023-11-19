using Sources.Domain.Constructs;
using Sources.Domain.Systems.Upgrades;
using Sources.InfrastructureInterfaces.Services.Times;

namespace Sources.Domain.Weapons.Lasers
{
    public class DoubleLaserTwiceGun : LaserWeaponBase, IConstructable
    {
        public DoubleLaserTwiceGun(
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