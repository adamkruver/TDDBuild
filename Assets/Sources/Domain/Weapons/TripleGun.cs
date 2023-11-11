using System.Linq;
using Sources.Domain.Bullets;
using Sources.Domain.Constructs;
using Sources.Domain.Systems.Upgrades;
using Sources.InfrastructureInterfaces.Services.Times;

namespace Sources.Domain.Weapons
{
    public class TripleGun : WeaponBase, IConstructable
    {
        public TripleGun(
            IBullet bullet,
            ITimeService timeService,
            WeaponFab weaponFab,
            UpgradeSystem upgradeSystem
        ) : base(
            Enumerable.Repeat(bullet, 3).ToArray(), 
            timeService, 
            weaponFab, 
            upgradeSystem
        )
        {
        }
    }
}