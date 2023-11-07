using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Sources.InfrastructureInterfaces.Services.Resource
{
    public interface IResourceService
    {
        IResourceService Register<T>(string pathName) where T : Object;
        IResourceService RegisterInterfaceImplementationsByType<TOut, TType>(string pathTemplate) where TOut : Object;
        UniTask LoadAllAsync();
    }
}