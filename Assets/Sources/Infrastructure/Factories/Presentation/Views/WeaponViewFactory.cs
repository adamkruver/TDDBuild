using Sources.Controllers;
using Sources.Domain.Weapons;
using Sources.Infrastructure.Resource;
using Sources.Infrastructure.Services.Weapons;
using Sources.InfrastructureInterfaces.Factories.Controllers;
using Sources.InfrastructureInterfaces.Services.Weapons;
using Sources.Presentation.Views.Systems.TargetTrackers;
using Sources.Presentation.Views.Weapons;
using UnityEngine;

namespace Sources.Infrastructure.Factories.Presentation.Views
{
    public class WeaponViewFactory
    {
        private readonly ResourceService _resourceService;
        private readonly IWeaponStateMachineFactory _weaponStateMachineFactory;
        private readonly TargetTrackerSystem _targetTrackerSystem;

        public WeaponViewFactory(
            ResourceService resourceService,
            IWeaponStateMachineFactory weaponStateMachineFactory,
            TargetTrackerSystem targetTrackerSystem
        )
        {
            _resourceService = resourceService;
            _weaponStateMachineFactory = weaponStateMachineFactory;
            _targetTrackerSystem = targetTrackerSystem;
        }

        public WeaponView Create(IWeapon weapon)
        {
            WeaponView weaponView =
                Object.Instantiate(_resourceService.Load<WeaponView>(GetPrefabPath(weapon)));

            // ShootPointView[] weaponViews = compositeWeaponView.ShootPoints;

            ITargetProvider targetProvider = new TargetProvider(_targetTrackerSystem, weaponView, weapon);

            IPresenter stateMachine = _weaponStateMachineFactory.Create(
                weaponView,
                weapon,
                targetProvider
            );

//            foreach (WeaponView view in weaponViews)
  //              _bulletViewFactory.Create(view.ProjectileView, weapon.Bullet);

            weaponView.Construct(stateMachine);

            return weaponView;
        }

        private string GetPrefabPath(IWeapon weapon) =>
            $"Views/Weapons/{weapon.GetType().Name}View";
    }
}