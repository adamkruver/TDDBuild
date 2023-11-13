using System.Linq;
using Sources.Domain.Bullets;
using Sources.Domain.Constructs;
using Sources.Domain.Systems.Upgrades;
using Sources.InfrastructureInterfaces.Services.Times;

namespace Sources.Domain.Weapons
{
    public class RocketTwiceGun : WeaponBase, IConstructable
    {
        public RocketTwiceGun(
            IBullet bullet,
            ITimeService timeService,
            WeaponFab weaponFab,
            UpgradeSystem upgradeSystem
        ) : base(
            Enumerable.Repeat(bullet, 4).ToArray(), 
            timeService, 
            weaponFab, 
            upgradeSystem
        )
        {
        }
        
        public Rocket Rocket => Bullet as Rocket;
    }
}