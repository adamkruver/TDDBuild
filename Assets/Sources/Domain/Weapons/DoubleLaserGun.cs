﻿using Sources.Domain.Bullets;
using Sources.Domain.Constructs;
using Sources.Domain.Systems.Upgrades;
using Sources.InfrastructureInterfaces.Services.Times;

namespace Sources.Domain.Weapons
{
    public class DoubleLaserGun : WeaponBase, IConstructable
    {
        public DoubleLaserGun(
            IBullet bullet,
            ITimeService timeService,
            WeaponFab weaponFab,
            UpgradeSystem laserUpgradeSystem
        ) : base(bullet, timeService, weaponFab, laserUpgradeSystem)
        {
        }
    }
}