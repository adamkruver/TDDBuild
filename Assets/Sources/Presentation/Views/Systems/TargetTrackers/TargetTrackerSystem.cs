using Sources.PresentationInterfaces.Views.Enemies;
using Sources.PresentationInterfaces.Views.Systems.TargetTrackers;
using UnityEngine;

namespace Sources.Presentation.Views.Systems.TargetTrackers
{
    public class TargetTrackerSystem : MonoBehaviour, ITargetTrackerSystem
    {
        [SerializeField] private int _maxColliders = 100;
        [SerializeField] private LayerMask _layerMask;

        private Collider[] _colliders;
        private Transform _transform;

        private void Awake()
        {
            _colliders = new Collider[_maxColliders];
            _transform = GetComponent<Transform>();
        }

        public IEnemyView Track(float radius)
        {
            int colliders = Physics.OverlapSphereNonAlloc(_transform.position, radius, _colliders, _layerMask);

            if (colliders == 0)
                return null;
            
            return GetClosestEnemy(colliders);            
        }

        private IEnemyView GetClosestEnemy(int colliders)
        {
            float maxDistance = float.MaxValue;
            IEnemyView view = null;
            
            for (int i = 0; i < colliders; i++)
            {
                if (_colliders[i].TryGetComponent(out IEnemyView enemyView))
                {
                    float distance = GetDistance(enemyView);
                    
                    if(distance < maxDistance)
                    {
                        maxDistance = distance;
                        view = enemyView;
                    }
                }
            }
            
            return view;
        }
        
        private float GetDistance(IEnemyView enemyView) => 
            Vector3.Distance(_transform.position, enemyView.Position);
    }
}