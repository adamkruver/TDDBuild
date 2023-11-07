using UnityEngine;

namespace Sources.InfrastructureInterfaces.Providers
{
    public interface IResourceProvider
    {
        T Load<T>(string path) where T : Object;
    }
}