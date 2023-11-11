using System.Linq;
using Sources.Domain.Bullets;
using Sources.Domain.Constructs;
using Sources.Domain.Systems.Upgrades;
using Sources.InfrastructureInterfaces.Services.Times;

namespace Sources.Domain.Weapons
{
    public class LaserGun : WeaponBase, IConstructable
    {
        public LaserGun(
            IBullet bullet,
            ITimeService timeService,
            WeaponFab weaponFab,
            UpgradeSystem upgradeSystem
        ) : base(
            Enumerable.Repeat(bullet, 1).ToArray(), 
            timeService, 
            weaponFab, 
            upgradeSystem
        )
        {
        }
    }
}