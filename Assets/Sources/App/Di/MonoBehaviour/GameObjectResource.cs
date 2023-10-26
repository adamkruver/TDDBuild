using System;
using UnityEngine;

namespace Sources.App.Di.MonoBehaviour
{
    public class GameObjectResource<TObject, TConfig>
        where TObject : UnityEngine.MonoBehaviour where TConfig : IPrefabProvider, new()
    {
        private readonly IPrefabProvider _prefabProvider = new TConfig();

        public TObject Create()
        {
            TObject gameObject = Resources.Load<TObject>(
                _prefabProvider.GetPath<TObject>()
            );

            if (gameObject == null)
                throw new NullReferenceException(
                    $"{GetType()}: {typeof(TObject)} not found in '{_prefabProvider.GetPath<TObject>()}'");

            return gameObject;
        }
    }
}