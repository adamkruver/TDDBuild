using System;
using System.Collections.Generic;
using System.Linq;
using Sources.Frameworks.LiveDatas;
using Sources.InfrastructureInterfaces.Listeners;
using Sources.InfrastructureInterfaces.Services;
using UnityEngine;

namespace Sources.Infrastructure.Services.Pointers
{
    public class MousePointerService : IUpdatable
    {
        private readonly PointerUIService _pointerUIService;
        private readonly IDictionary<int, IPointerHandler> _handlers;
        private readonly IDictionary<int, bool> _startedTouches;
        private readonly MutableLiveData<IUntouchablePointerHandler> _untouchablePointerHandler;

        public MousePointerService(
            PointerUIService pointerUIService,
            IDictionary<int, IPointerHandler> handlers,
            IDictionary<int, bool> startedTouches,
            MutableLiveData<IUntouchablePointerHandler> untouchablePointerHandler
        )
        {
            _pointerUIService = pointerUIService;
            _handlers = handlers;
            _startedTouches = startedTouches;
            _untouchablePointerHandler = untouchablePointerHandler;
        }

        private bool IsTouched => _startedTouches.Values.FirstOrDefault(isStartedTouch => isStartedTouch);
        
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
                _untouchablePointerHandler.Value?.OnMove(pointerPosition, isPointerOverUi);
        }
    }
}