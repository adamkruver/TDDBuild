using System;
using System.Collections.Generic;

namespace Sources.App.Di
{
    public class ObjectsContainer: IDisposable
    {
        private readonly Dictionary<Type, object> _objects = new Dictionary<Type, object>();

        public void Register(Type type, object obj)
        {
            if (_objects.ContainsKey(type))
                throw new InvalidOperationException($"{GetType()}: already contains object for {type}");

            _objects.Add(type, obj);
        }

        public object Get(Type type)
        {
            if (_objects.ContainsKey(type) == false)
                return null;

            return _objects[type];
        }

        public void Dispose()
        {
            foreach (var (type, obj) in _objects)
                if(obj is IDisposable disposable and not ObjectsContainer)
                    disposable.Dispose();
        }
    }
}