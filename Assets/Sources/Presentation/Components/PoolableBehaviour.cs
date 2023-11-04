using Sources.Infrastructure.ObjectPools;
using UnityEngine;

namespace Sources.Presentation.Components
{
    public class PoolableBehaviour : MonoBehaviour
    {
        private ObjectPool _objectPool;
        
        public void Create(ObjectPool objectPool)
        {
            _objectPool = objectPool;
            OnAfterCreate();
        }

        public void Destroy()
        {
            if (_objectPool == null)
            {
                Destroy(gameObject);

                return;
            }
            
            OnBeforeDestroy();
            
            gameObject.SetActive(false);
            _objectPool.Add(this);
        }

        protected virtual void OnAfterCreate()
        {
        }

        protected virtual void OnBeforeDestroy()
        {
        }
    }
}