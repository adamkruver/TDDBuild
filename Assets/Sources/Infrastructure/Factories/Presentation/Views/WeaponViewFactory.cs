using Sources.Controllers;
using Sources.Domain.Weapons;
using Sources.InfrastructureInterfaces.Factories.Controllers;
using Sources.Presentation.Views.Weapons;
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

        public WeaponView Create(IWeapon weapon)
        {
            WeaponView weaponView = Object.Instantiate(Resources.Load<WeaponView>(GetPrefabPath(weapon)));

            IPresenter stateMachine = _weaponStateMachineFactory.Create(
                weaponView, weapon, weaponView.TargetTrackerSystem
            );

            weaponView.Construct(stateMachine);

            _bulletViewFactory.Create(weaponView.Bullet, weapon.Bullet);

            return weaponView;
        }

        private string GetPrefabPath(IWeapon weapon) =>
            $"Views/Weapons/{weapon.GetType().Name}View";
    }
}