using System.Collections.Generic;
using Sources.InfrastructureInterfaces.FiniteStateMachines;

namespace Sources.Infrastructure.FiniteStateMachines.States
{
    public abstract class FiniteStateBase : IFiniteState
    {
        private readonly List<ITransition> _transitions = new List<ITransition>();
        private IFiniteStateChangeService _service;

        public void AddTransition(ITransition transition) =>
            _transitions.Add(transition);

        public void RemoveTransition(ITransition transition) =>
            _transitions.Remove(transition);

        public void Enter(IFiniteStateChangeService service)
        {
            _service = service;
            OnEnter();
            EnableTransitions();
        }

        public void Exit()
        {
            DisableTransitions();
            OnExit();
        }

        public void Update(float deltaTime)
        {
            OnUpdate(deltaTime);

            if (HasNextState(out IFiniteState nextState))
                _service.ChangeState(nextState);
        }

        public void UpdateFixed(float fixedDeltaTime) =>
            OnFixedUpdate(fixedDeltaTime);

        public void UpdateLate(float deltaTime) =>
            OnLateUpdate(deltaTime);

        protected virtual void OnEnter()
        {
        }

        protected virtual void OnExit()
        {
        }

        protected virtual void OnUpdate(float deltaTime)
        {
        }

        protected virtual void OnFixedUpdate(float fixedDeltaTime)
        {
        }

        protected virtual void OnLateUpdate(float deltaTime)
        {
        }

        private bool HasNextState(out IFiniteState nextState)
        {
            foreach (var transition in _transitions)
                if (transition.HasNextState(out nextState))
                    return true;

            nextState = null;

            return false;
        }

        private void EnableTransitions()
        {
            foreach (ITransition transition in _transitions)
                transition.Enable();
        }

        private void DisableTransitions()
        {
            foreach (ITransition transition in _transitions)
                transition.Disable();
        }
    }
}