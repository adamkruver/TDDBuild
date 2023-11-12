using Sources.Domain.HealthPoints;
using Sources.Frameworks.LiveDatas;
using Sources.Infrastructure.FiniteStateMachines.Transitions;
using Sources.InfrastructureInterfaces.FiniteStateMachines;

namespace Sources.Controllers.Zombies.FiniteStateMachines.Transitions
{
    public class ToDeathTransition : TransitionBase
    {
        private readonly LiveData<float> _health;

        public ToDeathTransition(IFiniteState nextState, Health health) : base(nextState)
        {
            _health = health.Points;
        }

        protected override bool CanMoveNextState() =>
            _health.Value <= 0;
    }
}