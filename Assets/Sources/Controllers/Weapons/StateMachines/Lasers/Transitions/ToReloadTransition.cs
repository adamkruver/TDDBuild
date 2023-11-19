using Sources.Domain.Weapons.Rockets;
using Sources.Infrastructure.FiniteStateMachines.Transitions;
using Sources.InfrastructureInterfaces.FiniteStateMachines;

namespace Sources.Controllers.Weapons.StateMachines.Lasers.Transitions
{
    public class ToReloadTransition : TransitionBase
    {
        private readonly IRocketWeapon _rocketWeapon;

        public ToReloadTransition(IFiniteState nextState, IRocketWeapon rocketWeapon) : base(nextState) =>
            _rocketWeapon = rocketWeapon;

        protected override bool CanMoveNextState() =>
            _rocketWeapon.HasNoRockets;
    }
}