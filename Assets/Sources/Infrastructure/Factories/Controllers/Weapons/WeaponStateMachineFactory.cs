using System;
using System.Collections.Generic;
using Sources.Controllers;
using Sources.Domain.Weapons;
using Sources.InfrastructureInterfaces.Factories.Controllers;
using Sources.PresentationInterfaces.Views.Systems.TargetTrackers;
using Sources.PresentationInterfaces.Views.Weapons;

namespace Sources.Infrastructure.Factories.Controllers.Weapons
{
    public class WeaponStateMachineFactory : IWeaponStateMachineFactory
    {
        private readonly Dictionary<Type, IWeaponStateMachineFactory> _factories;

        public WeaponStateMachineFactory(Dictionary<Type, IWeaponStateMachineFactory> factories) =>
            _factories = factories ?? throw new ArgumentNullException(nameof(factories));

        public IPresenter Create(IWeaponView view, IWeapon weapon, ITargetTrackerSystem targetTrackerSystem)
        {
            Type weaponType = weapon.GetType();

            if (_factories.ContainsKey(weaponType) == false)
                throw new KeyNotFoundException(weaponType.Name);

            return _factories[weaponType].Create(view, weapon, targetTrackerSystem);
        }
    }
}