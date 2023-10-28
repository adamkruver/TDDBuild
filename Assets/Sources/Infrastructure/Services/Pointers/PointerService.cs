using System.Collections.Generic;
using System.Linq;
using Sources.InfrastructureInterfaces.Listeners;
using Sources.InfrastructureInterfaces.Services;
using UnityEngine;

namespace Sources.Infrastructure.Services.Pointers
{
    public class PointerService : IUpdatable
    {
        private readonly Dictionary<int, IPointerListener> _handlers = new Dictionary<int, IPointerListener>();
        private readonly Dictionary<int, bool> _startedTouches = new Dictionary<int, bool>();
        private IUntouchablePointerListener _untouchablePointerListener;

        private bool IsTouched => _startedTouches.Values.FirstOrDefault(isStartedTouch => isStartedTouch);

        public void RegisterHandler(int pointerId, IPointerListener listener) =>
            _handlers[pointerId] = listener;

        public void UnregisterHandler(int pointerId) =>
            _handlers.Remove(pointerId);

        public void RegisterUntouchableHandler(IUntouchablePointerListener listener) =>
            _untouchablePointerListener = listener;

        public void UnregisterUntouchableHandler() =>
            _untouchablePointerListener = null;

        public void UnregisterAll() =>
            _handlers.Clear();

        public void Update(float deltaTime)
        {
            Vector3 pointerPosition = Input.mousePosition;

            foreach (int pointerId in _handlers.Keys)
            {
                if (_startedTouches.ContainsKey(pointerId) == false)
                    _startedTouches[pointerId] = false;

                if (_startedTouches[pointerId] == false)
                {
                    if (Input.GetMouseButtonDown(pointerId))
                    {
                        _handlers[pointerId].OnTouchStart(pointerPosition);
                        _startedTouches[pointerId] = true;
                    }
                }
                else
                {
                    if (Input.GetMouseButtonUp(pointerId))
                    {
                        _handlers[pointerId].OnTouchEnd(pointerPosition);
                        _startedTouches[pointerId] = false;
                    }
                    else
                    {
                        _handlers[pointerId].OnTouchMove(pointerPosition);
                    }
                }
            }

            if (IsTouched == false) 
                _untouchablePointerListener?.OnMove(pointerPosition);
        }
    }
}