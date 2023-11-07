using Sources.Domain.Systems.Upgrades;
using Sources.Domain.Weapons;
using Sources.Infrastructure.Factories.Domain.Bullets;
using Sources.InfrastructureInterfaces.Providers;
using Sources.InfrastructureInterfaces.Services.Times;

namespace Sources.Infrastructure.Factories.Domain.Weapons
{
    public class QuadGunFactory : WeaponFactoryBase<QuadGun>
    {
        private readonly BulletFactory _bulletFactory;
        private readonly UpgradeSystem _upgradeSystem;

        public QuadGunFactory(
            IResourceProvider resourceProvider,
            BulletFactory bulletFactory,
            ITimeService timeService,
            UpgradeSystem upgradeSystem
        ) : base(resourceProvider, timeService)
        {
            _bulletFactory = bulletFactory;
            _upgradeSystem = upgradeSystem;
        }

        public override IWeapon Create() =>
            new QuadGun(_bulletFactory.Create(), TimeService, WeaponFab, _upgradeSystem);
    }
}