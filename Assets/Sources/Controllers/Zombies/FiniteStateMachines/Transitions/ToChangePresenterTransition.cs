using Sources.Domain.Zombies;
using Sources.Infrastructure.FiniteStateMachines.Transitions;
using Sources.InfrastructureInterfaces.FiniteStateMachines;

namespace Sources.Controllers.Zombies.FiniteStateMachines.Transitions
{
    public class ToChangePresenterTransition : TransitionBase
    {
        private readonly Zombie _zombie;

        public ToChangePresenterTransition(IFiniteState nextState, Zombie zombie) : base(nextState)
        {
            _zombie = zombie;
        }

        protected override bool CanMoveNextState() =>
            _zombie.IsDecaying;
    }
}