using System;
using Sources.Infrastructure.Resource.Generic;

namespace Sources.Infrastructure.Factories.Resource
{
    public class ResourceFactory
    {
        public Infrastructure.Resource.ResourceObject Create(Type type, string path) =>
            (Infrastructure.Resource.ResourceObject)Activator
                .CreateInstance(
                    typeof(ResourceObject<>).MakeGenericType(type),
                    new object[] { path }
                );
    }
}