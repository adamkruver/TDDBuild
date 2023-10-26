using System;
using System.Collections.Generic;

namespace Sources.App.Di.MonoBehaviour
{
    public abstract class AbstractPrefabProvider : IPrefabProvider
    {
        private readonly Dictionary<Type, string> _provides;

        protected AbstractPrefabProvider(Dictionary<Type, string> provides) =>
            _provides = provides;

        public string GetPath<T>() =>
            _provides.ContainsKey(typeof(T))
                ? _provides[typeof(T)]
                : throw new NullReferenceException($"{GetType()}: not found provide for {typeof(T)}");
    }
}