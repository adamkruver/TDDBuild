using UnityEngine;

namespace Sources.InfrastructureInterfaces.Listeners
{
    public interface IPointerHandler
    {
        void OnTouchStart(Vector3 position);
        void OnTouchMove(Vector3 position, bool isPointerOverUI);
        void OnTouchEnd(Vector3 position);
    }
}