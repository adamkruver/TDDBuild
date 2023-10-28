using UnityEngine;

namespace Sources.InfrastructureInterfaces.Listeners
{
    public interface IUntouchablePointerHandler
    {
        void OnMove(Vector3 screenPosition, bool isPointerOverUI);
    }
}