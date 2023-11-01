using System;
using Sources.Domain.Weapons;
using Sources.Infrastructure.FiniteStateMachines.States;
using Sources.PresentationInterfaces.Animations.Weapons;
using Sources.PresentationInterfaces.Views.Weapons;
using UnityEngine;

namespace Sources.Controllers.Weapons.StateMachines.Lasers.States
{
    public class ShootState : FiniteStateBase
    {
        private readonly IWeaponView _weaponView;
        private readonly IWeaponAnimation _weaponAnimation;
        private readonly IWeapon _weapon;

        public ShootState(
            IWeaponView weaponView,
            IWeaponAnimation weaponAnimation,
            IWeapon weapon
        )
        {
            _weaponView = weaponView ?? throw new ArgumentNullException(nameof(weaponView));
            _weaponAnimation = weaponAnimation ?? throw new ArgumentNullException(nameof(weaponAnimation));
            _weapon = weapon ?? throw new ArgumentNullException(nameof(weapon));
        }

        protected override void OnEnter()
        {
            _weaponAnimation.Shoot();
            _weaponView.Fire();
            _weapon.Fire();
        }
    }
}