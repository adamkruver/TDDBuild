using Sources.InfrastructureInterfaces.FiniteStateMachines;

namespace Sources.Infrastructure.FiniteStateMachines
{
    public class FiniteStateMachine : IFiniteStateMachine, IFiniteStateChangeService
    {
        private IFiniteState _currentState;

        public void SetFirstState(IFiniteState state) =>
            _currentState = state;

        public void ChangeState(IFiniteState nextState)
        {
            ExitState();
            _currentState = nextState;
            EnterState();
        }

        public void Update(float deltaTime) =>
            _currentState?.Update(deltaTime);

        public void UpdateFixed(float fixedDeltaTime) =>
            _currentState?.UpdateFixed(fixedDeltaTime);

        public void UpdateLate(float deltaTime) =>
            _currentState?.UpdateLate(deltaTime);

        public void Run() =>
            EnterState();

        public void Stop() =>
            ExitState();

        private void EnterState() =>
            _currentState?.Enter(this);

        private void ExitState() =>
            _currentState?.Exit();
    }
}