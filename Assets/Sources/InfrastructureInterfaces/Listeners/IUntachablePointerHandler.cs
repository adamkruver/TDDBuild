using UnityEngine;

namespace Sources.InfrastructureInterfaces.Listeners
{
    public interface IUntouchablePointerListener
    {
        void OnMove(Vector3 position);
    }
}