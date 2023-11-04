using System.Collections.Generic;
using System.Linq;
using Sources.InfrastructureInterfaces.ObjectPools;
using Sources.Presentation.Components;
using UnityEngine;

namespace Sources.Infrastructure.ObjectPools
{
    public class ObjectPool : IObjectPool
    {
        private readonly List<PoolableBehaviour> _objects = new List<PoolableBehaviour>();

        public T Get<T>() where T : PoolableBehaviour
        {
            PoolableBehaviour @object = _objects.FirstOrDefault(gameObject => gameObject.GetComponent<T>() != null);

            if (@object == null)
                return null;

            _objects.Remove(@object);

            return @object.GetComponent<T>();
        }

        public void Add(PoolableBehaviour @object) =>
            _objects.Add(@object);
    }
}