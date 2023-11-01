using Sources.Domain.Weapons;
using Sources.Infrastructure.FiniteStateMachines.Transitions;
using Sources.InfrastructureInterfaces.FiniteStateMachines;

namespace Sources.Controllers.Weapons.StateMachines.Lasers.Transitions
{
    public class ToCooldownTransition : TransitionBase
    {
        private readonly IWeapon _weapon;

        public ToCooldownTransition(IFiniteState nextState, IWeapon weapon) : base(nextState) => 
            _weapon = weapon;

        protected override bool CanMoveNextState() => 
            _weapon.CanShoot == false;
    }
}