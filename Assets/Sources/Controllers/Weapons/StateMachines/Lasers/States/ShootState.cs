using System;
using System.Linq;
using Sources.Domain.Weapons;
using Sources.Infrastructure.FiniteStateMachines.States;
using Sources.PresentationInterfaces.Animations.Weapons;
using Sources.PresentationInterfaces.Views.Weapons;

namespace Sources.Controllers.Weapons.StateMachines.Lasers.States
{
    public class ShootState : FiniteStateBase
    {
        private readonly IWeaponView[] _weaponViews;
        private readonly IWeaponAnimation[] _weaponAnimations;
        private readonly IWeapon _weapon;

        private int _currentBarrelId;

        public ShootState(IWeaponView[] weaponViews, IWeapon weapon)
        {
            _weaponViews = weaponViews ?? throw new ArgumentNullException(nameof(weaponViews));
            _weapon = weapon ?? throw new ArgumentNullException(nameof(weapon));
            _weaponAnimations = _weaponViews.Select(view => view.Animation).ToArray();
        }

        protected override void OnEnter()
        {
            _weaponAnimations[_currentBarrelId].Shoot();
            _weaponViews[_currentBarrelId].Fire();
            _weapon.Fire();

            if (++_currentBarrelId == _weaponViews.Length)
                _currentBarrelId = 0;
        }
    }
}