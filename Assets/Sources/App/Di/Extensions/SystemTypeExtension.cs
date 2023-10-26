using System;
using Sources.App.Di.Activation;

namespace Sources.App.Di.Extensions
{
    internal static class SystemTypeExtension
    {
        public static ObjectType ToObjectType(this Type type)
        {
            if (type.IsInterface)
                return ObjectType.Interface;
            
            if (type.IsClass)
                return ObjectType.Class;


            return ObjectType.Unknown;
        }
    }
}