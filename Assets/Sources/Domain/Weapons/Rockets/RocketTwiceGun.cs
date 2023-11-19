using Sources.Domain.Constructs;
using Sources.Domain.Systems.Upgrades;
using Sources.InfrastructureInterfaces.Services.Times;

namespace Sources.Domain.Weapons.Rockets
{
    public class RocketTwiceGun : RocketWeaponBase, IConstructable
    {
        public RocketTwiceGun(
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

        protected override float GetShootSpeed()
        {
            return 1000000;
        }
    }
}