using UnityEngine;

namespace Sources.Domain.Systems.PathDraw
{
    public class PathDrawSystemPoint
    {
        public PathDrawSystemPoint(Vector3 position, Vector3 direction, float spawnTime, float lifeTime)
        {
            Position = position;
            Direction = direction;
            SpawnTime = spawnTime;
            LifeTime = lifeTime;
        }

        public Vector3 Position { get; }
        public Vector3 Direction { get; }
        public float SpawnTime { get; }
        public float LifeTime { get; }
    }
}