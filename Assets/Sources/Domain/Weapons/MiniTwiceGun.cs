using System.Linq;
using Sources.Domain.Bullets;
using Sources.Domain.Constructs;
using Sources.Domain.Systems.Upgrades;
using Sources.InfrastructureInterfaces.Services.Times;

namespace Sources.Domain.Weapons
{
    public class MiniTwiceGun : WeaponBase, IConstructable
    {
        public MiniTwiceGun(
            IBullet bullet,
            ITimeService timeService,
            WeaponFab weaponFab,
            UpgradeSystem upgradeSystem
        ) : base(
            Enumerable.Repeat(bullet, 2).ToArray(),
            timeService,
            weaponFab,
            upgradeSystem
        )
        {
        }
    }
}