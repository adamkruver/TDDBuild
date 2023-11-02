using Sources.Domain.Zombies;
using Sources.Infrastructure.FiniteStateMachines.Transitions;
using Sources.InfrastructureInterfaces.FiniteStateMachines;

namespace Sources.Controllers.Zombies.StateMachines.Transitions
{
    public class ToDeathTransition : TransitionBase
    {
        private readonly Zombie _zombie;

        public ToDeathTransition(IFiniteState nextState, Zombie zombie) : base(nextState)
        {
            _zombie = zombie;
        }

        protected override bool CanMoveNextState() => 
            _zombie.Health.Points.Value <= 0;
    }
}