using System;
using System.Collections.Generic;
using Sources.Controllers;
using Sources.Domain.Weapons;
using Sources.InfrastructureInterfaces.Factories.Controllers;
using Sources.Presentation.Views.Weapons;
using Sources.PresentationInterfaces.Views.Systems.TargetTrackers;
using Sources.PresentationInterfaces.Views.Weapons;

namespace Sources.Infrastructure.Factories.Controllers.Weapons
{
    public class WeaponStateMachineFactory : IWeaponStateMachineFactory
    {
        private readonly Dictionary<Type, IWeaponStateMachineFactory> _factories;

        public WeaponStateMachineFactory(Dictionary<Type, IWeaponStateMachineFactory> factories) =>
            _factories = factories ?? throw new ArgumentNullException(nameof(factories));

        public IPresenter Create(
            ICompositeWeaponView compositeView,
            IWeaponView[] views,
            IWeapon weapon,
            ITargetTrackerSystem targetTrackerSystem
        )
        {
            Type weaponType = weapon.GetType();

            if (_factories.ContainsKey(weaponType) == false)
                throw new KeyNotFoundException(weaponType.Name);

            return _factories[weaponType].Create(compositeView, views, weapon, targetTrackerSystem);
        }
    }
}