﻿using Sources.Domain.Systems.Upgrades;
using Sources.Domain.Weapons;
using Sources.Infrastructure.Factories.Domain.Bullets;
using Sources.InfrastructureInterfaces.Services.Times;

namespace Sources.Infrastructure.Factories.Domain.Weapons
{
    public class DoubleGunFactory : WeaponFactoryBase<DoubleGun>
    {
        private readonly BulletFactory _bulletFactory;
        private readonly UpgradeSystem _upgradeSystem;

        public DoubleGunFactory(
            BulletFactory bulletFactory,
            ITimeService timeService,
            UpgradeSystem upgradeSystem
        ) : base(timeService)
        {
            _bulletFactory = bulletFactory;
            _upgradeSystem = upgradeSystem;
        }

        public override IWeapon Create() =>
            new DoubleGun(_bulletFactory.Create(), TimeService, WeaponFab, _upgradeSystem);
    }
}