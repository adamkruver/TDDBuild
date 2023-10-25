using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Sources.InfrastructureInterfaces.Services;
using Sources.InfrastructureInterfaces.StateMachines;

namespace Sources.Infrastructure.StateMachines
{
    public class StateMachine : IUpdatable, ILateUpdatable, IFixedUpdatable
    {
        private readonly List<Func<string, UniTask>> _enteringHandlers = new List<Func<string, UniTask>>();
        private readonly List<Func<UniTask>> _exitingHandlers = new List<Func<UniTask>>();

        private readonly Dictionary<string, IState> _states;

        private IState _currentState;

        public StateMachine(Dictionary<string, IState> states) =>
            _states = states ?? throw new ArgumentNullException(nameof(states));

        public void AddEnterHandler(Func<string, UniTask> handler) =>
            _enteringHandlers.Add(handler);

        public void AddExitHandler(Func<UniTask> handler) =>
            _exitingHandlers.Add(handler);

        public void RemoveEnterHandler(Func<string, UniTask> handler) =>
            _enteringHandlers.Remove(handler);

        public void RemoveExitHandler(Func<UniTask> handler) =>
            _exitingHandlers.Remove(handler);

        public async UniTask ChangeStateAsync(string stateName, object payload = null)
        {
            if (HasState(stateName) == false)
                throw new InvalidOperationException(nameof(stateName));

            foreach (Func<string, UniTask> enteringHandler in _enteringHandlers)
                await enteringHandler.Invoke(stateName);

            _currentState?.Exit();
            _currentState = _states[stateName];
            _currentState?.Enter(payload);

            foreach (Func<UniTask> exitingHandler in _exitingHandlers)
                await exitingHandler.Invoke();
        }

        public void Update(float deltaTime) =>
            _currentState?.Update(deltaTime);

        public void UpdateLate(float deltaTime) =>
            _currentState?.UpdateLate(deltaTime);

        public void UpdateFixed(float fixedDeltaTime) =>
            _currentState?.UpdateFixed(fixedDeltaTime);

        private bool HasState(string stateName) =>
            _states.ContainsKey(stateName);
    }
}