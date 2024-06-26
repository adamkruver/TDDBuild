﻿using Sources.Domain.Weapons;
using Sources.Infrastructure.FiniteStateMachines.Transitions;
using Sources.InfrastructureInterfaces.FiniteStateMachines;

namespace Sources.Controllers.Weapons.StateMachines.Common.Transitions
{
    public class ToTrackTargetTransition : TransitionBase
    {
        private readonly IWeapon _weapon;

        public ToTrackTargetTransition(
            IFiniteState nextState,
            IWeapon weapon
        ) : base(nextState) =>
            _weapon = weapon;

        protected override bool CanMoveNextState() =>
            _weapon.CanShoot;
    }
}