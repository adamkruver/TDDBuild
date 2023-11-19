using System;
using Sources.Domain.Weapons;
using Sources.Infrastructure.FiniteStateMachines.States;

namespace Sources.Controllers.Weapons.StateMachines.Common.States
{
    public class ShootState : FiniteStateBase
    {
        private readonly IWeapon _weapon;

        public ShootState(IWeapon weapon) =>
            _weapon = weapon ?? throw new ArgumentNullException(nameof(weapon));

        protected override void OnEnter() =>
            _weapon.Shoot();
    }
}