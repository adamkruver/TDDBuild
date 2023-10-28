using UnityEngine;

namespace Sources.InfrastructureInterfaces.Listeners
{
    public interface IPointerListener
    {
        void OnTouchStart(Vector3 position);
        void OnTouchMove(Vector3 position);
        void OnTouchEnd(Vector3 position);
    }
}