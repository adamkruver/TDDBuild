using System.Linq;
using Sources.Domain.Constructs;
using Sources.Domain.Projectiles;
using Sources.Domain.Systems.Upgrades;
using Sources.InfrastructureInterfaces.Services.Times;

namespace Sources.Domain.Weapons.Lasers
{
    public class DoubleLaserGun : LaserWeaponBase, IConstructable
    {
        public DoubleLaserGun(
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