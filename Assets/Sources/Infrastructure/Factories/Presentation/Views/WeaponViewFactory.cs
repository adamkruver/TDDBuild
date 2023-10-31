using System;
using System.Collections.Generic;
using Sources.Controllers.Weapons;
using Sources.Domain.Weapons;
using Sources.Infrastructure.Factories.Controllers.Weapons;
using Sources.Presentation.Views.Weapons;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Sources.Infrastructure.Factories.Presentation.Views
{
    public class WeaponViewFactory
    {
        private readonly WeaponPresenterFactory _weaponPresenterFactory;
        private readonly Dictionary<Type, string> _prefabPaths;

        public WeaponViewFactory(WeaponPresenterFactory weaponPresenterFactory, Dictionary<Type, string> prefabPaths)
        {
            _weaponPresenterFactory = weaponPresenterFactory;
            _prefabPaths = prefabPaths;
        }

        public WeaponView Create(IWeapon weapon)
        {
            string prefabPath = _prefabPaths[weapon.GetType()];
            WeaponView weaponView = Object.Instantiate(Resources.Load<WeaponView>(prefabPath));
            WeaponPresenter presenter = _weaponPresenterFactory.Create(
                weaponView, weapon, weaponView.TargetTrackerSystem
            );
            weaponView.Construct(presenter);

            return weaponView;
        }
    }
}