using System;
using Sources.Domain.Systems.Upgrades;
using Sources.Domain.Weapons;
using Sources.Domain.Weapons.Lasers;
using Sources.InfrastructureInterfaces.Providers;
using Sources.InfrastructureInterfaces.Services.Times;

namespace Sources.Infrastructure.Factories.Domain.Weapons.Lasers
{
    public class DoubleLaserTwiceGunFactory : WeaponFactoryBase<DoubleLaserTwiceGun>
    {
        private readonly UpgradeSystem _upgradeSystem;

        public DoubleLaserTwiceGunFactory(
            IResourceProvider resourceProvider,
            ITimeService timeService,
            UpgradeSystem upgradeSystem
        ) : base(resourceProvider, timeService) =>
            _upgradeSystem = upgradeSystem ?? throw new ArgumentNullException(nameof(upgradeSystem));

        public override IWeapon Create() =>
            new DoubleLaserTwiceGun(TimeService, WeaponFab, _upgradeSystem);
    }
}