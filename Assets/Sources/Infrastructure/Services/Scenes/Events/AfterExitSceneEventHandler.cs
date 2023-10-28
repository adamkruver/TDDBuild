using System;
using Cysharp.Threading.Tasks;
using Sources.InfrastructureInterfaces.Services.Scenes.Events.Generic;

namespace Sources.Infrastructure.Services.Scenes.Events
{
    public class AfterExitSceneEventHandler : ISceneServiceEventHandler<string>
    {
        private readonly Func<string, UniTask> _eventHandler;

        public AfterExitSceneEventHandler(Func<string, UniTask> eventHandler) =>
            _eventHandler = eventHandler;

        public async UniTask Handle(string payload) =>
            await _eventHandler.Invoke(payload);
    }
}