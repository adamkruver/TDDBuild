using Sources.Infrastructure.FiniteStateMachines.Transitions;
using Sources.InfrastructureInterfaces.FiniteStateMachines;

namespace Sources.Controllers.Weapons.StateMachines.Lasers.Transitions
{
    public class ToShootStateTransition : TransitionBase
    {
        public ToShootStateTransition(IFiniteState nextState) : base(nextState)
        {
        }

        public override void Enable()
        {
        }

        public override void Disable()
        {
        }

        protected override bool CanMoveNextState()
        {
            return false;
        }
    }
}