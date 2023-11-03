﻿using Sources.Domain.Bullets;
using Sources.Domain.Weapons;
using Sources.InfrastructureInterfaces.Services.Times;

namespace Sources.Infrastructure.Factories.Domain.Weapons
{
    public class DoubleLaserTwiceGunFactory : WeaponFactoryBase<DoubleLaserTwiceGun>
    {
        public DoubleLaserTwiceGunFactory(ITimeService timeService) : base(timeService)
        {
        }

        public override IWeapon Create() =>
            new DoubleLaserTwiceGun(new Laser(), TimeService, WeaponFab);
    }
}