using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Sources.Controllers.Scenes;
using Sources.Infrastructure.Services.Scenes.Events;
using Sources.Infrastructure.StateMachines;
using Sources.InfrastructureInterfaces.Services;
using Sources.InfrastructureInterfaces.Services.Scenes;
using Sources.InfrastructureInterfaces.Services.Scenes.Events;

namespace Sources.Infrastructure.Services.Scenes
{
    public class SceneService : ISceneChangeService, IUpdatable, ILateUpdatable, IFixedUpdatable
    {
        private readonly List<IEventHandler> _eventHandlers = new List<IEventHandler>();
        private readonly StateMachine<SceneState> _stateMachine;
        private readonly Dictionary<string, SceneState> _states;

        public SceneService(StateMachine<SceneState> stateMachine, Dictionary<string, SceneState> states)
        {
            _stateMachine = stateMachine;
            _states = states;
        }

        public void AddEventListener(IEventHandler eventHandler) =>
            _eventHandlers.Add(eventHandler);

        public void RemoveEventListener(IEventHandler eventHandler) =>
            _eventHandlers.Remove(eventHandler);

        public async UniTask ChangeStateAsync(string sceneName, object payload = null)
        {
            if (ContainState(sceneName) == false)
                throw new InvalidOperationException(nameof(sceneName));

            await NotifyBeforeExitListeners(sceneName);
            _stateMachine.ExitState();
            await NotifyAfterExitListeners(sceneName);

            await NotifyBeforeEnterListeners(sceneName);
            _stateMachine.EnterState(_states[sceneName], payload);
            await NotifyAfterEnterListeners(sceneName);
        }

        public void Update(float deltaTime) => 
            _stateMachine.Update(deltaTime);

        public void UpdateFixed(float fixedDeltaTime) => 
            _stateMachine.UpdateFixed(fixedDeltaTime);

        public void UpdateLate(float deltaTime) => 
            _stateMachine.UpdateLate(deltaTime);
        
        private async UniTask NotifyBeforeEnterListeners(string sceneName)
        {
            foreach (IEventHandler listener in _eventHandlers)
                if (listener is BeforeEnterSceneEventHandler beforeEnterSceneEvent)
                    await beforeEnterSceneEvent.Handle(sceneName);
        }

        private async UniTask NotifyAfterEnterListeners(string sceneName)
        {
            foreach (IEventHandler listener in _eventHandlers)
                if (listener is AfterEnterSceneEventHandler afterEnterSceneEvent)
                    await afterEnterSceneEvent.Handle(sceneName);
        }

        private async UniTask NotifyBeforeExitListeners(string sceneName)
        {
            foreach (IEventHandler listener in _eventHandlers)
                if (listener is BeforeExitSceneEventHandler beforeExitSceneEvent)
                    await beforeExitSceneEvent.Handle(sceneName);
        }

        private async UniTask NotifyAfterExitListeners(string sceneName)
        {
            foreach (IEventHandler listener in _eventHandlers)
                if (listener is AfterExitSceneEventHandler afterExitSceneEvent)
                    await afterExitSceneEvent.Handle(sceneName);
        }

        private bool ContainState(string stateName) =>
            _states.ContainsKey(stateName);
    }
}