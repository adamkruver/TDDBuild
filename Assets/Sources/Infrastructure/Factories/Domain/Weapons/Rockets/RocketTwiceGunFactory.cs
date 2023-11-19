using System;
using Sources.Domain.Systems.Upgrades;
using Sources.Domain.Weapons;
using Sources.Domain.Weapons.Rockets;
using Sources.InfrastructureInterfaces.Providers;
using Sources.InfrastructureInterfaces.Services.Times;

namespace Sources.Infrastructure.Factories.Domain.Weapons.Rockets
{
    public class RocketTwiceGunFactory : WeaponFactoryBase<RocketTwiceGun>
    {
        private readonly UpgradeSystem _upgradeSystem;

        public RocketTwiceGunFactory(
            IResourceProvider resourceProvider,
            ITimeService timeService,
            UpgradeSystem upgradeSystem
        ) : base(resourceProvider, timeService) =>
            _upgradeSystem = upgradeSystem ?? throw new ArgumentNullException(nameof(upgradeSystem));

        public override IWeapon Create() =>
            new RocketTwiceGun(TimeService, WeaponFab, _upgradeSystem);
    }
}