using System;
using Sources.Domain.Weapons;
using Sources.Infrastructure.FiniteStateMachines.States;
using Sources.Presentation.Views.Weapons;

namespace Sources.Controllers.Weapons.StateMachines.Lasers.States
{
    public class ShootState : FiniteStateBase
    {
        private readonly ICompositeWeaponView _compositeWeaponView;
        private readonly IWeapon _weapon;
        private readonly int _shootsAtOnce;
        private readonly int _maxBarrelId;

        private int _currentBarrelId;

        public ShootState(ICompositeWeaponView compositeWeaponView, IWeapon weapon, int shootsAtOnce)
        {
            _compositeWeaponView = compositeWeaponView ?? throw new ArgumentNullException(nameof(compositeWeaponView));
            _weapon = weapon ?? throw new ArgumentNullException(nameof(weapon));
            _shootsAtOnce = shootsAtOnce;
            _maxBarrelId = compositeWeaponView.BarrelsAmount;
        }

        protected override void OnEnter()
        {
            _weapon.Shoot();

            for (int i = 0; i < _shootsAtOnce; i++)
            {
                _compositeWeaponView.Shoot(_currentBarrelId);

                if (++_currentBarrelId == _maxBarrelId)
                    _currentBarrelId = 0;
            }
        }
    }
}