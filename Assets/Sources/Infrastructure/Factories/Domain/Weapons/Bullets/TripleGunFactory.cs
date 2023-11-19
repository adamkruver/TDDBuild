using System;
using Sources.Domain.Systems.Upgrades;
using Sources.Domain.Weapons;
using Sources.Domain.Weapons.Bullets;
using Sources.InfrastructureInterfaces.Providers;
using Sources.InfrastructureInterfaces.Services.Times;

namespace Sources.Infrastructure.Factories.Domain.Weapons.Bullets
{
    public class TripleGunFactory : WeaponFactoryBase<TripleGun>
    {
        private readonly UpgradeSystem _upgradeSystem;

        public TripleGunFactory(
            IResourceProvider resourceProvider,
            ITimeService timeService,
            UpgradeSystem upgradeSystem
        ) : base(resourceProvider, timeService) =>
            _upgradeSystem = upgradeSystem ?? throw new ArgumentNullException(nameof(upgradeSystem));

        public override IWeapon Create() =>
            new TripleGun(TimeService, WeaponFab, _upgradeSystem);
    }
}