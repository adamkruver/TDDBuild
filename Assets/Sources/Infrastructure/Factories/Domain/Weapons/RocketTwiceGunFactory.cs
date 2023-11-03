using Sources.Domain.Systems.Upgrades;
using Sources.Domain.Weapons;
using Sources.Infrastructure.Factories.Domain.Bullets;
using Sources.InfrastructureInterfaces.Services.Times;

namespace Sources.Infrastructure.Factories.Domain.Weapons
{
    public class RocketTwiceGunFactory : WeaponFactoryBase<RocketTwiceGun>
    {
        private readonly RocketFactory _rocketFactory;
        private readonly UpgradeSystem _rocketUpgradeSystem;

        public RocketTwiceGunFactory(
            RocketFactory rocketFactory,
            ITimeService timeService,
            UpgradeSystem rocketUpgradeSystem
        ) : base(timeService)
        {
            _rocketFactory = rocketFactory;
            _rocketUpgradeSystem = rocketUpgradeSystem;
        }

        public override IWeapon Create() =>
            new RocketTwiceGun(_rocketFactory.Create(), TimeService, WeaponFab, _rocketUpgradeSystem);
    }
}