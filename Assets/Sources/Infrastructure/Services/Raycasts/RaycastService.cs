using Sources.InfrastructureInterfaces.Services.Raycasts;
using UnityEngine;

namespace Sources.Infrastructure.Services.Raycasts
{
    public class RaycastService : IRaycastService
    {
        public bool TryRaycast(
            Vector3 position,
            Vector3 direction,
            out Vector3 hitPoint,
            float maxDistance = Mathf.Infinity,
            int layer = -1
        )
        {
            direction = direction.normalized;

            Ray ray = new Ray(position, direction);

            if (Physics.Raycast(ray, out RaycastHit hit, maxDistance, layer) == false)
            {
                hitPoint = position + direction * maxDistance;

                return false;
            }

            hitPoint = hit.point;

            return true;
        }
    }
}