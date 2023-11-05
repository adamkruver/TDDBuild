using Sources.PresentationInterfaces.Views.Enemies;
using Sources.PresentationInterfaces.Views.Systems.TargetTrackers;
using UnityEngine;

namespace Sources.Presentation.Views.Systems.TargetTrackers
{
    public class TargetTrackerSystem : ITargetTrackerSystem
    {
        private readonly LayerMask _enemyLayerMask;
        private readonly LayerMask _obstacleLayerMask;
        private readonly Collider[] _colliders;

        public TargetTrackerSystem(int maxColliders, LayerMask enemyLayerMask, LayerMask obstacleLayerMask)
        {
            _enemyLayerMask = enemyLayerMask;
            _obstacleLayerMask = obstacleLayerMask;
            _colliders = new Collider[maxColliders];
        }

        public IEnemyView Track(Vector3 position, float minFireDistance, float maxFireDistance)
        {
            int colliders = Physics.OverlapSphereNonAlloc(position, maxFireDistance, _colliders, _enemyLayerMask);
        
            if (colliders == 0)
                return null;

            return GetClosestEnemy(position, colliders, minFireDistance, maxFireDistance);
        }

        public bool CanSeeEnemy(Vector3 position, IEnemyView enemy, float minFireDistance, float maxFireDistance)
        {
            if (enemy == null)
                return false;
            
            Vector3 direction = enemy.Position - position;
            float distance = direction.magnitude;

            if (distance > maxFireDistance)
                return false;

            if (distance < minFireDistance)
                return false;
            
            if (Physics.Raycast(position, direction.normalized, out RaycastHit hit, distance, _obstacleLayerMask))
                return false;

            return true;
        }

        private IEnemyView GetClosestEnemy(Vector3 position, int colliders, float minFireDistance, float maxFireDistance)
        {
            float maxDistance = float.MaxValue;
            IEnemyView view = null;

            for (int i = 0; i < colliders; i++)
            {
                if (_colliders[i].TryGetComponent(out IEnemyView enemyView))
                {
                    float distance = GetDistance(position, enemyView);

                    if (distance < maxDistance)
                    {
                        if (CanSeeEnemy(position, enemyView, minFireDistance, maxFireDistance))
                        {
                            maxDistance = distance;
                            view = enemyView;
                        }
                    }
                }
            }
            
            return view;
        }

        private float GetDistance(Vector3 position, IEnemyView enemy) =>
            Vector3.Distance(position, enemy.Position);
    }
}