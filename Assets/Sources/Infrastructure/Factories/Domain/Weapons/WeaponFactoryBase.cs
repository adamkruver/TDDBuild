﻿using Sources.Domain.Bullets;
using Sources.Domain.Weapons;
using Sources.InfrastructureInterfaces.Services.Times;
using UnityEngine;

namespace Sources.Infrastructure.Factories.Domain.Weapons
{
    public abstract class WeaponFactoryBase<T> : IWeaponFactory where T : IWeapon
    {
        protected readonly ITimeService TimeService;
        protected readonly WeaponFab WeaponFab;

        protected WeaponFactoryBase(ITimeService timeService)
        {
            TimeService = timeService;
            WeaponFab = Resources.Load<WeaponFab>($"Fabs/Weapons/{typeof(T).Name}Fab");
        }

        public abstract IWeapon Create();
    }
}