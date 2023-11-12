using Sources.Infrastructure.FiniteStateMachines.Transitions;
using Sources.InfrastructureInterfaces.FiniteStateMachines;

namespace Sources.Controllers.Zombies.FiniteStateMachines.Transitions
{
    public class ToAnyOneFrameTransition : TransitionBase
    {
        public ToAnyOneFrameTransition(IFiniteState nextState) : base(nextState)
        {
        }

        protected override bool CanMoveNextState() =>
            true;
    }
}