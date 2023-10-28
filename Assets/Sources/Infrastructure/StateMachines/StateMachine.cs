using Sources.InfrastructureInterfaces.Services;
using Sources.InfrastructureInterfaces.StateMachines;

namespace Sources.Infrastructure.StateMachines
{
    public class StateMachine<T> : IUpdatable, ILateUpdatable, IFixedUpdatable where T: IState
    {
        private T _currentState;

        public void ChangeState(T state, object payload = null)
        {
            ExitState();
            EnterState(state, payload);
        }

        public void ExitState()
        {
            _currentState?.Exit();
            _currentState = default;
        }

        public void EnterState(T state, object payload = null)
        {
            _currentState = state;
            _currentState?.Enter(payload);
        }

        public void Update(float deltaTime) =>
            _currentState?.Update(deltaTime);

        public void UpdateLate(float deltaTime) =>
            _currentState?.UpdateLate(deltaTime);

        public void UpdateFixed(float fixedDeltaTime) =>
            _currentState?.UpdateFixed(fixedDeltaTime);
    }
}