using Sources.Domain.Systems.Upgrades;
using Sources.Domain.Weapons;
using Sources.Infrastructure.Factories.Domain.Bullets;
using Sources.InfrastructureInterfaces.Services.Times;

namespace Sources.Infrastructure.Factories.Domain.Weapons
{
    public class DoubleLaserGunFactory : WeaponFactoryBase<DoubleLaserGun>
    {
        private readonly LaserFactory _laserFactory;
        private readonly UpgradeSystem _laserUpgradeSystem;

        public DoubleLaserGunFactory(
            LaserFactory laserFactory,
            ITimeService timeService,
            UpgradeSystem laserUpgradeSystem
        ) : base(timeService)
        {
            _laserFactory = laserFactory;
            _laserUpgradeSystem = laserUpgradeSystem;
        }

        public override IWeapon Create() =>
            new DoubleLaserGun(_laserFactory.Create(), TimeService, WeaponFab, _laserUpgradeSystem);
    }
}