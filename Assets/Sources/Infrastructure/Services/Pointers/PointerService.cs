using System.Collections.Generic;
using Sources.Frameworks.LiveDatas;
using Sources.InfrastructureInterfaces.Listeners;
using Sources.InfrastructureInterfaces.Services;
using Sources.InfrastructureInterfaces.Services.Pointers;
using UnityEngine;

namespace Sources.Infrastructure.Services.Pointers
{
    public class PointerService : IUpdatable, IPointerService
    {
        private readonly PointerUIService _pointerUIService = new PointerUIService();
        private readonly IDictionary<int, IPointerHandler> _handlers = new Dictionary<int, IPointerHandler>();
        private readonly IDictionary<int, bool> _startedTouches = new Dictionary<int, bool>();

        private readonly MutableLiveData<IUntouchablePointerHandler> _untouchablePointerHandler =
            new MutableLiveData<IUntouchablePointerHandler>();

        private readonly MousePointerService _mousePointerService;
        private readonly TouchPointerService _touchPointerService;

        public PointerService()
        {
            _mousePointerService = new MousePointerService(
                _pointerUIService, _handlers, _startedTouches, _untouchablePointerHandler
            );
            _touchPointerService = new TouchPointerService(_pointerUIService, _handlers, _startedTouches);
        }

        public void RegisterHandler(int pointerId, IPointerHandler handler) =>
            _handlers[pointerId] = handler;

        public void UnregisterHandler(int pointerId) =>
            _handlers.Remove(pointerId);

        public void RegisterUntouchableHandler(IUntouchablePointerHandler handler) =>
            _untouchablePointerHandler.Value = handler;

        public void UnregisterUntouchableHandler() =>
            _untouchablePointerHandler.Value = null;

        public void UnregisterAll() =>
            _handlers.Clear();

        public void Update(float deltaTime)
        {
            if (Input.touches.Length > 0)
            {
                _touchPointerService.Update(deltaTime);

                return;
            }

            _mousePointerService.Update(deltaTime);
        }
    }
}