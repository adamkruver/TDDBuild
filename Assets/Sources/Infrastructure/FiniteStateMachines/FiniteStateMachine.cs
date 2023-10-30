using Sources.InfrastructureInterfaces.FiniteStateMachines;

namespace Sources.Infrastructure.FiniteStateMachines
{
    public class FiniteStateMachine : IFiniteStateMachine, IFiniteStateChangeService
    {
        private IFiniteState _currentState;

        public void ChangeState(IFiniteState nextState)
        {
            _currentState?.Exit();
            _currentState = nextState;
            _currentState?.Enter(this);
        }

        public void Update(float deltaTime) =>
            _currentState?.Update(deltaTime);

        public void UpdateFixed(float fixedDeltaTime) =>
            _currentState?.UpdateFixed(fixedDeltaTime);

        public void UpdateLate(float deltaTime) =>
            _currentState?.UpdateLate(deltaTime);
    }
}