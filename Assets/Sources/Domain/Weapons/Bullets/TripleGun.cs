using Sources.Domain.Constructs;
using Sources.Domain.Systems.Upgrades;
using Sources.InfrastructureInterfaces.Services.Times;

namespace Sources.Domain.Weapons.Bullets
{
    public class TripleGun : BulletWeaponBase, IConstructable
    {
        public TripleGun(
            ITimeService timeService,
            WeaponFab weaponFab,
            UpgradeSystem upgradeSystem
        ) : base(
            shootPoints: 3,
            timeService, 
            weaponFab, 
            upgradeSystem
        )
        {
        }
    }
}