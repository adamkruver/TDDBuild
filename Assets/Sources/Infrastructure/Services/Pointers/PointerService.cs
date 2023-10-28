using System;
using System.Collections.Generic;
using System.Linq;
using Sources.InfrastructureInterfaces.Listeners;
using Sources.InfrastructureInterfaces.Services;
using Sources.InfrastructureInterfaces.Services.Pointers;
using UnityEngine;

namespace Sources.Infrastructure.Services.Pointers
{
    public class PointerService : IUpdatable, IPointerService
    {
        private readonly PointerUIService _pointerUIService = new PointerUIService();
        private readonly Dictionary<int, IPointerHandler> _handlers = new Dictionary<int, IPointerHandler>();
        private readonly Dictionary<int, bool> _startedTouches = new Dictionary<int, bool>();
        private IUntouchablePointerHandler _untouchablePointerHandler;

        private bool IsTouched => _startedTouches.Values.FirstOrDefault(isStartedTouch => isStartedTouch);

        public void RegisterHandler(int pointerId, IPointerHandler handler) =>
            _handlers[pointerId] = handler;

        public void UnregisterHandler(int pointerId) =>
            _handlers.Remove(pointerId);

        public void RegisterUntouchableHandler(IUntouchablePointerHandler handler) =>
            _untouchablePointerHandler = handler;

        public void UnregisterUntouchableHandler() =>
            _untouchablePointerHandler = null;

        public void UnregisterAll() =>
            _handlers.Clear();

        public void Update(float deltaTime)
        {
            Vector3 pointerPosition = Input.mousePosition;
            bool isPointerOverUi = _pointerUIService.IsPointerOverUI;
            List<Action<Vector3, bool>> actions = new List<Action<Vector3, bool>>();
            
            foreach (int pointerId in _handlers.Keys)
            {
                if (_startedTouches.ContainsKey(pointerId) == false)
                    _startedTouches[pointerId] = false;

                if (_startedTouches[pointerId] == false)
                {
                    if (isPointerOverUi == false)
                    {
                        if (Input.GetMouseButtonDown(pointerId))
                        {
                            actions.Add(_handlers[pointerId].OnTouchStart);
                            _startedTouches[pointerId] = true;
                        }
                    }
                }
                else
                {
                    if (Input.GetMouseButtonUp(pointerId))
                    {
                        actions.Add(_handlers[pointerId].OnTouchEnd);
                        _startedTouches[pointerId] = false;
                    }
                    else
                    {
                        actions.Add(_handlers[pointerId].OnTouchMove);
                    }
                }
            }

            foreach (Action<Vector3, bool> action in actions) 
                action.Invoke(pointerPosition, isPointerOverUi);

            if (IsTouched == false)
                _untouchablePointerHandler?.OnMove(pointerPosition, isPointerOverUi);
        }
    }
}