using System;
using System.Collections.Generic;
using Sources.Controllers.Weapons;
using Sources.Domain.Weapons;
using Sources.Infrastructure.Factories.Controllers.Weapons;
using Sources.Presentation.Views.Weapons;
using Sources.PresentationInterfaces.Views.Weapons;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Sources.Infrastructure.Factories.Presentation.Views
{
    public class WeaponViewFactory
    {
        private readonly WeaponStateMachineFactory _weaponStateMachineFactory;
        private readonly BulletViewFactory _bulletViewFactory;
        private readonly Dictionary<Type, string> _prefabPaths;

        public WeaponViewFactory(
            WeaponStateMachineFactory weaponStateMachineFactory,
            BulletViewFactory bulletViewFactory,
            Dictionary<Type, string> prefabPaths)
        {
            _weaponStateMachineFactory = weaponStateMachineFactory;
            _bulletViewFactory = bulletViewFactory;
            _prefabPaths = prefabPaths;
        }

        public CompositeWeaponView Create(IWeapon weapon)
        {
            string prefabPath = _prefabPaths[weapon.GetType()];
            CompositeWeaponView compositeWeaponView = Object.Instantiate(Resources.Load<CompositeWeaponView>(prefabPath));
            
            IWeaponView[] weaponViews = compositeWeaponView.WeaponViews;

            WeaponStateMachine stateMachine = _weaponStateMachineFactory.Create(
                weaponViews, weapon, compositeWeaponView.TargetTrackerSystem
            );

            foreach (IWeaponView weaponView in weaponViews)
            {
                WeaponView view = (WeaponView) weaponView;
                view.Construct(stateMachine);
                _bulletViewFactory.Create(view.Bullet, weapon.Bullet);
            }

            return compositeWeaponView;
        }
    }
}