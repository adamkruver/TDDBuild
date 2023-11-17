using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Sources.Controllers.Scenes;
using Sources.Infrastructure.Services.Scenes.Events;
using Sources.Infrastructure.StateMachines;
using Sources.InfrastructureInterfaces.Factories.Scenes;
using Sources.InfrastructureInterfaces.Services;
using Sources.InfrastructureInterfaces.Services.Scenes;
using Sources.InfrastructureInterfaces.Services.Scenes.Events;

namespace Sources.Infrastructure.Services.Scenes
{
    public class SceneService : ISceneChangeService, IUpdatable, ILateUpdatable, IFixedUpdatable
    {
        private readonly StateMachine<IScene> _stateMachine = new StateMachine<IScene>();
        private readonly List<IEventHandler> _eventHandlers = new List<IEventHandler>();

        private readonly IReadOnlyDictionary<string, ISceneFactory> _sceneFactories;

        public SceneService(IReadOnlyDictionary<string, ISceneFactory> sceneFactories) =>
            _sceneFactories = sceneFactories;

        public void AddEventListener(IEventHandler eventHandler) =>
            _eventHandlers.Add(eventHandler);

        public void RemoveEventListener(IEventHandler eventHandler) =>
            _eventHandlers.Remove(eventHandler);

        public async UniTask ChangeSceneAsync(string sceneName, object payload = null)
        {
            if (ContainState(sceneName) == false)
                throw new InvalidOperationException(nameof(sceneName));

            await NotifyBeforeExitListeners(sceneName);
            _stateMachine.ExitState();
            await NotifyAfterExitListeners(sceneName);

            await NotifyBeforeEnterListeners(sceneName);
            IScene scene = await _sceneFactories[sceneName].Create(payload);
            _stateMachine.EnterState(scene, payload);
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
            _sceneFactories.ContainsKey(stateName);
    }
}