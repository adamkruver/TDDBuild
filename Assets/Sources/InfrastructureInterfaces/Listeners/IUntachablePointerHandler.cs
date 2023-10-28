using UnityEngine;

namespace Sources.InfrastructureInterfaces.Listeners
{
    public interface IUntouchablePointerHandler
    {
        void OnMove(Vector3 position, bool isPointerOverUI);
    }
}