using Sources.Domain.Constructs;
using Sources.Domain.Systems.Upgrades;
using Sources.InfrastructureInterfaces.Services.Times;

namespace Sources.Domain.Weapons.Lasers
{
    public class LaserGun : LaserWeaponBase, IConstructable
    {
        public LaserGun(
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