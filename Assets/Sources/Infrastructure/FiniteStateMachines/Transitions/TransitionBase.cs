using Sources.InfrastructureInterfaces.FiniteStateMachines;

namespace Sources.Infrastructure.FiniteStateMachines.Transitions
{
    public abstract class TransitionBase : ITransition
    {
        private readonly IFiniteState _nextState;

        protected TransitionBase(IFiniteState nextState) =>
            _nextState = nextState;

        public virtual void Enable()
        {
        }

        public virtual void Disable()
        {
        }

        public void Update(float deltaTime) =>
            OnUpdate(deltaTime);

        public void UpdateFixed(float fixedDeltaTime) =>
            OnFixedUpdate(fixedDeltaTime);

        public void UpdateLate(float deltaTime) =>
            OnLateUpdate(deltaTime);

        public bool HasNextState(out IFiniteState nextState)
        {
            nextState = _nextState;

            return CanMoveNextState();
        }

        protected abstract bool CanMoveNextState();

        protected virtual void OnUpdate(float deltaTime)
        {
        }

        protected virtual void OnFixedUpdate(float fixedDeltaTime)
        {
        }

        protected virtual void OnLateUpdate(float deltaTime)
        {
        }
    }
}