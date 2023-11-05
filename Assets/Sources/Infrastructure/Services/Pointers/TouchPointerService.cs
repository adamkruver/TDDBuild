using System;
using System.Collections.Generic;
using Sources.InfrastructureInterfaces.Listeners;
using Sources.InfrastructureInterfaces.Services;
using UnityEngine;

namespace Sources.Infrastructure.Services.Pointers
{
    public class TouchPointerService : IUpdatable
    {
        private readonly PointerUIService _pointerUIService;
        private readonly IDictionary<int, IPointerHandler> _handlers;
        private readonly IDictionary<int, bool> _startedTouches;

        private const int PointerId = 1;

        public TouchPointerService(
            PointerUIService pointerUIService,
            IDictionary<int, IPointerHandler> handlers,
            IDictionary<int, bool> startedTouches
        )
        {
            _pointerUIService = pointerUIService;
            _handlers = handlers;
            _startedTouches = startedTouches;
        }

        public void Update(float deltaTime)
        {
            var touches = Input.touches;

            if (touches.Length != 1)
                return;

            Touch touch = touches[0];

            Vector3 pointerPosition = touch.position;
            bool isPointerOverUi = _pointerUIService.IsPointerOverUI;
            List<Action<Vector3, bool>> actions = new List<Action<Vector3, bool>>();

            if (_startedTouches.ContainsKey(PointerId) == false)
                _startedTouches[PointerId] = false;

            if (_startedTouches[PointerId] == false)
            {
                if (isPointerOverUi == false)
                {
                    if (touch.phase == TouchPhase.Began)
                    {
                        actions.Add(_handlers[PointerId].OnTouchStart);
                        _startedTouches[PointerId] = true;
                    }
                }
            }
            else
            {
                if (touch.phase == TouchPhase.Ended)
                {
                    actions.Add(_handlers[PointerId].OnTouchEnd);
                    _startedTouches[PointerId] = false;
                }
                else
                {
                    actions.Add(_handlers[PointerId].OnTouchMove);
                }
            }

            foreach (Action<Vector3, bool> action in actions)
                action.Invoke(pointerPosition, isPointerOverUi);
        }
    }
}