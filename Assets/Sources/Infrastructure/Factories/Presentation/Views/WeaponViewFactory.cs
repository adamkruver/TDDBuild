using System;
using System.Collections.Generic;
using Sources.Controllers;
using Sources.Controllers.Weapons;
using Sources.Domain.Weapons;
using Sources.Infrastructure.Factories.Controllers.Weapons;
using Sources.InfrastructureInterfaces.Factories.Controllers;
using Sources.Presentation.Views.Weapons;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Sources.Infrastructure.Factories.Presentation.Views
{
    public class WeaponViewFactory
    {
        private readonly IWeaponStateMachineFactory _weaponStateMachineFactory;
        private readonly BulletViewFactory _bulletViewFactory;
        private readonly Dictionary<Type, string> _prefabPaths;

        public WeaponViewFactory(
            IWeaponStateMachineFactory weaponStateMachineFactory,
            BulletViewFactory bulletViewFactory,
            Dictionary<Type, string> prefabPaths)
        {
            _weaponStateMachineFactory = weaponStateMachineFactory;
            _bulletViewFactory = bulletViewFactory;
            _prefabPaths = prefabPaths;
        }

        public WeaponView Create(IWeapon weapon)
        {
            string prefabPath = _prefabPaths[weapon.GetType()];
            WeaponView weaponView = Object.Instantiate(Resources.Load<WeaponView>(prefabPath));
            
            IPresenter stateMachine = _weaponStateMachineFactory.Create(
                weaponView, weapon, weaponView.TargetTrackerSystem
            );
            
            weaponView.Construct(stateMachine);

            _bulletViewFactory.Create(weaponView.Bullet, weapon.Bullet);

            return weaponView;
        }
    }
}