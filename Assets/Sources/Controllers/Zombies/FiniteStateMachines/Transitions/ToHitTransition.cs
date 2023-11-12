using Sources.Domain.HealthPoints;
using Sources.Frameworks.LiveDatas;
using Sources.Infrastructure.FiniteStateMachines.Transitions;
using Sources.InfrastructureInterfaces.FiniteStateMachines;

namespace Sources.Controllers.Zombies.FiniteStateMachines.Transitions
{
    public class ToHitTransition : TransitionBase
    {
        private readonly LiveData<float> _health;

        private float _previousHealth;
        private float _hitThreshold;

        public ToHitTransition(IFiniteState nextState, Health health) : base(nextState)
        {
            _health = health.Points;
            _previousHealth = _health.Value;
        }

        protected override bool CanMoveNextState()
        {
            bool result = _previousHealth - _health.Value > _hitThreshold;
            _previousHealth = _health.Value;
            return result;
        }
    }
}