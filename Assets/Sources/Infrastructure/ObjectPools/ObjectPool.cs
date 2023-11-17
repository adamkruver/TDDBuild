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
        private readonly GameObject _group;

        public ObjectPool(string groupName)
        {
            _group = new GameObject(groupName);
        }

        public T Get<T>() where T : PoolableBehaviour
        {
            PoolableBehaviour @object = _objects.FirstOrDefault(gameObject => gameObject.GetComponent<T>() != null);

            if (@object == null)
                return null;

            _objects.Remove(@object);

            return @object.GetComponent<T>();
        }

        public bool Contain<T>() where T : PoolableBehaviour =>
            _objects.FirstOrDefault(gameObject => gameObject.GetComponent<T>() != null) != null;

        public void Add(PoolableBehaviour @object)
        {
            _objects.Add(@object);
            @object.transform.SetParent(_group.transform);
        }
    }
}