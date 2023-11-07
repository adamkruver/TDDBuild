using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Sources.Extensions.Types;
using Sources.Infrastructure.Factories.Resource;
using Sources.Infrastructure.Resource.Generic;
using Sources.InfrastructureInterfaces.Providers;
using Sources.InfrastructureInterfaces.Services.Resource;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Sources.Infrastructure.Resource
{
    public class ResourceService : IResourceProvider, IResourceService, IDisposable
    {
        private readonly Dictionary<string, ResourceObject> _resources = new Dictionary<string, ResourceObject>();
        private readonly ResourceFactory _resourceFactory = new ResourceFactory();

        public IResourceService Register<T>(string pathName) where T : Object
        {
            if (_resources.ContainsKey(pathName))
                throw new InvalidOperationException($"Duplicated resource {pathName}");

            _resources[pathName] = new ResourceObject<T>(pathName);

            return this;
        }

        public IResourceService RegisterInterfaceImplementationsByType<TOut, TInterface>(string pathTemplate) where TOut : Object
        {
            foreach (Type type in typeof(TInterface).GetAllImplementations())
            {
                string resourcePath = string.Format(pathTemplate, type.Name);
                
                ResourceObject<TOut> resourceObject = new ResourceObject<TOut>(resourcePath);
                
                if (_resources.ContainsKey(resourcePath))
                    throw new InvalidOperationException($"Duplicated resource {resourcePath}");
                
                _resources[resourcePath] = resourceObject;
            }

            return this;
        }

        public T Load<T>(string path) where T : Object
        {
            if (_resources.ContainsKey(path) == false)
                throw new KeyNotFoundException(path);

            if (_resources[path] is not ResourceObject<T> resource)
                throw new InvalidCastException(typeof(T).Name);

            if (resource.Object is null)
                throw new NullReferenceException("Use LoadAllAsync method before Load<>");

            return resource.Object;
        }

        public async UniTask LoadAllAsync()
        {
            foreach (ResourceObject resource in _resources.Values)
                await resource.LoadAsync();
        }

        public void Dispose()
        {
            foreach (ResourceObject resource in _resources.Values) 
                resource.Unload();
        }
    }
}