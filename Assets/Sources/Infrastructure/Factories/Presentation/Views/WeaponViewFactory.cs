using Sources.Controllers;
using Sources.Domain.Weapons;
using Sources.InfrastructureInterfaces.Factories.Controllers;
using Sources.Presentation.Views.Weapons;
using Sources.PresentationInterfaces.Views.Weapons;
using UnityEngine;

namespace Sources.Infrastructure.Factories.Presentation.Views
{
    public class WeaponViewFactory
    {
        private readonly IWeaponStateMachineFactory _weaponStateMachineFactory;
        private readonly BulletViewFactory _bulletViewFactory;

        public WeaponViewFactory(
            IWeaponStateMachineFactory weaponStateMachineFactory,
            BulletViewFactory bulletViewFactory
        )
        {
            _weaponStateMachineFactory = weaponStateMachineFactory;
            _bulletViewFactory = bulletViewFactory;
        }

        public CompositeWeaponView Create(IWeapon weapon)
        {
            WeaponView weaponView = Object.Instantiate(Resources.Load<WeaponView>(GetPrefabPath(weapon)));

            IPresenter stateMachine = _weaponStateMachineFactory.Create(
                weaponView, weapon, weaponView.TargetTrackerSystem
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

        private string GetPrefabPath(IWeapon weapon) =>
            $"Views/Weapons/{weapon.GetType().Name}View";
    }
}