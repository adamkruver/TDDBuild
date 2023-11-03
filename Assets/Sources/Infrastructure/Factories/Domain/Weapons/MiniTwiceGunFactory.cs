using Sources.Domain.Systems.Upgrades;
using Sources.Domain.Weapons;
using Sources.Infrastructure.Factories.Domain.Bullets;
using Sources.InfrastructureInterfaces.Services.Times;

namespace Sources.Infrastructure.Factories.Domain.Weapons
{
    public class MiniTwiceGunFactory : WeaponFactoryBase<MiniTwiceGun>
    {
        private readonly BulletFactory _bulletFactory;
        private readonly UpgradeSystem _bulletUpgradeSystem;

        public MiniTwiceGunFactory(
            BulletFactory bulletFactory,
            ITimeService timeService,
            UpgradeSystem bulletUpgradeSystem
        ) : base(timeService)
        {
            _bulletFactory = bulletFactory;
            _bulletUpgradeSystem = bulletUpgradeSystem;
        }

        public override IWeapon Create() =>
            new MiniTwiceGun(_bulletFactory.Create(), TimeService, WeaponFab, _bulletUpgradeSystem);
    }
}