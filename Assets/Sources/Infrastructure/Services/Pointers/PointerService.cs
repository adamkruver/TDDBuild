using System.Collections.Generic;
using Sources.InfrastructureInterfaces.Handlers;
using Sources.InfrastructureInterfaces.Services;
using UnityEngine;

namespace Sources.Infrastructure.Services.Pointers
{
    public class PointerService : IUpdatable
    {
        private readonly Dictionary<int, IPointerHandler> _handlers = new Dictionary<int, IPointerHandler>();
        private readonly Dictionary<int, bool> _startedTouches = new Dictionary<int, bool>();

        public void RegisterHandler(int pointerId, IPointerHandler handler) =>
            _handlers[pointerId] = handler;

        public void UnregisterHandler(int pointerId) =>
            _handlers.Remove(pointerId);

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
        }
    }
}