using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Sources.Infrastructure.Services.NavMeshes
{
    public class NavMeshService
    {
        private const float RemainingDistanceThreshold = 0.3f;

        public List<Vector3> CalculatePathPoints(Vector3 startPosition, Vector3 endPosition, float pointsInterval)
        {
            List<Vector3> result = new List<Vector3>();
            NavMeshPath path = new NavMeshPath();

            bool isExist = NavMesh.CalculatePath(startPosition, endPosition, NavMesh.AllAreas, path);

            if (isExist == false)
                return result;

            Vector3 previousCorner = path.corners[0];
            float accumulatedDistance = 0.0f;
            float remainingDistance = 0;

            for (var i = 1; i < path.corners.Length; i++)
            {
                Vector3 corner = path.corners[i];
                Vector3 directionToNextCorner = corner - previousCorner;
                float distanceToNextCorner = directionToNextCorner.magnitude;

                directionToNextCorner.Normalize();

                while (accumulatedDistance  + remainingDistance < distanceToNextCorner)
                {
                    Vector3 spawnPosition =
                        previousCorner + directionToNextCorner * (accumulatedDistance + remainingDistance);

                    result.Add(spawnPosition);

                    accumulatedDistance += pointsInterval;
                }

                remainingDistance = distanceToNextCorner - accumulatedDistance;

                if (remainingDistance / pointsInterval < RemainingDistanceThreshold) 
                    remainingDistance = 0;

                accumulatedDistance -= distanceToNextCorner;
                previousCorner = corner;
            }

            return result;
        }
    }
}