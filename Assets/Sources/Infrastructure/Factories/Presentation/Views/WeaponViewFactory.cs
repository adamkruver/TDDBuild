using System.Linq;
using Sources.Controllers;
using Sources.Controllers.Weapons;
using Sources.Domain.Weapons;
using Sources.InfrastructureInterfaces.Factories.Controllers;
using Sources.Presentation.Views.Systems.TargetTrackers;
using Sources.Presentation.Views.Weapons;
using Sources.PresentationInterfaces.Views.Weapons;
using UnityEngine;

namespace Sources.Infrastructure.Factories.Presentation.Views
{
    public class WeaponViewFactory
    {
        private readonly IWeaponStateMachineFactory _weaponStateMachineFactory;
        private readonly BulletViewFactory _bulletViewFactory;
        private readonly TargetTrackerSystem _targetTrackerSystem;

        public WeaponViewFactory(
            IWeaponStateMachineFactory weaponStateMachineFactory,
            BulletViewFactory bulletViewFactory,
            TargetTrackerSystem targetTrackerSystem
        )
        {
            _weaponStateMachineFactory = weaponStateMachineFactory;
            _bulletViewFactory = bulletViewFactory;
            _targetTrackerSystem = targetTrackerSystem;
        }

        public CompositeWeaponView Create(IWeapon weapon)
        {
            CompositeWeaponView compositeWeaponView =
                Object.Instantiate(Resources.Load<CompositeWeaponView>(GetPrefabPath(weapon)));

            WeaponView[] weaponViews = compositeWeaponView.WeaponViews;
            

            IPresenter stateMachine = _weaponStateMachineFactory.Create(
                compositeWeaponView, 
                weaponViews.Cast<IWeaponView>().ToArray(), 
                weapon,
                _targetTrackerSystem
            );

            foreach (WeaponView view in weaponViews) 
                _bulletViewFactory.Create(view.Bullet, weapon.Bullet);
            
            compositeWeaponView.Construct(stateMachine);

            return compositeWeaponView;
        }

        private string GetPrefabPath(IWeapon weapon) =>
            $"Views/Weapons/{weapon.GetType().Name}View";
    }
}