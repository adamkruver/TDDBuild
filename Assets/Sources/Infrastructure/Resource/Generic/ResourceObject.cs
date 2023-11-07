using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Sources.Infrastructure.Resource.Generic
{
    class ResourceObject<T> : ResourceObject where T : Object
    {
        public ResourceObject(string path) =>
            Path = path;

        public override string Path { get; }

        public T Object { get; private set; }

        public override async UniTask LoadAsync() =>
            Object = await Resources.LoadAsync<T>(Path).ToUniTask() as T
                     ?? throw new NullReferenceException($"Can't load resource {typeof(T)} from path {Path}");

        public override void Unload() =>
            Resources.UnloadAsset(Object);
    }
}