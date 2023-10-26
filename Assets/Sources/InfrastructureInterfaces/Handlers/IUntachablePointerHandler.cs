using UnityEngine;

namespace Sources.InfrastructureInterfaces.Handlers
{
    public interface IUntouchablePointerHandler
    {
        void OnMove(Vector3 position);
    }
}