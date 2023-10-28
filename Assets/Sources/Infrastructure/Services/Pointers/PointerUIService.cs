using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Sources.Infrastructure.Services.Pointers
{
    public class PointerUIService
    {
        private readonly int _layerMask = LayerMask.NameToLayer("UI");

        public bool IsPointerOverUI => PointerIsOverUI();

        private EventSystem EventSystem => EventSystem.current;
        private Vector3 PointerPosition => Input.mousePosition;

        private bool PointerIsOverUI()
        {
            GameObject hitObject = UIRaycast(ScreenPositionToPointerData(PointerPosition));
            return hitObject != null && hitObject.layer == _layerMask;
        }

        private GameObject UIRaycast(PointerEventData pointerEventData)
        {
            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.RaycastAll(pointerEventData, results);

            return results.Count < 1
                ? null
                : results[0].gameObject;
        }

        private PointerEventData ScreenPositionToPointerData(Vector2 screenPosition) =>
            new PointerEventData(EventSystem)
            {
                position = screenPosition
            };
    }
}