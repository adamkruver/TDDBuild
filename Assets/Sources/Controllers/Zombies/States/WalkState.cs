using Sources.Infrastructure.FiniteStateMachines.States;
using Sources.InfrastructureInterfaces.FiniteStateMachines;

namespace Sources.Controllers.Zombies.States
{
    public class WalkState : FiniteStateBase, IFiniteState
    {
        protected override void OnExit()
        {
        }

        protected override void OnEnter()
        {
        }
    }
}