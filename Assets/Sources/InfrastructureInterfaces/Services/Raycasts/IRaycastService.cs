using UnityEngine;

namespace Sources.InfrastructureInterfaces.Services.Raycasts
{
    public interface IRaycastService
    {
        bool TryRaycast(Vector3 position, Vector3 direction, out Vector3 hitPoint, float maxDistance, int layer);
    }
}