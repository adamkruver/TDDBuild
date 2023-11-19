using Sources.Domain.Constructs;
using Sources.Domain.Systems.Upgrades;
using Sources.InfrastructureInterfaces.Services.Times;

namespace Sources.Domain.Weapons.Bullets
{
    public class DoubleGun : BulletWeaponBase, IConstructable
    {
        public DoubleGun(
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