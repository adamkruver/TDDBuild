using UnityEngine;

namespace Sources.InfrastructureInterfaces.Handlers
{
    public interface IPointerHandler
    {
        void OnTouchStart(Vector3 position);
        void OnTouchMove(Vector3 position);
        void OnTouchEnd(Vector3 position);
    }
}