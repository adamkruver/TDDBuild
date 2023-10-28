﻿using System;
using Cysharp.Threading.Tasks;
using Sources.InfrastructureInterfaces.Services.Scenes.Events.Generic;

namespace Sources.Infrastructure.Services.Scenes.Events
{
    public class AfterEnterSceneEventHandler : ISceneServiceEventHandler<string>
    {
        private readonly Func<string, UniTask> _eventHandler;

        public AfterEnterSceneEventHandler(Func<string, UniTask> eventHandler) =>
            _eventHandler = eventHandler;

        public UniTask Handle(string payload) =>
            _eventHandler.Invoke(payload);
    }
}