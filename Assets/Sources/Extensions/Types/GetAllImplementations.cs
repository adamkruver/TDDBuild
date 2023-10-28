using System;
using System.Collections.Generic;
using System.Linq;

namespace Sources.Extensions.Types
{
    public static partial class TypeExtensions 
    {
        public static IEnumerable<Type> GetAllImplementations(this Type desiredType) =>
            desiredType.Assembly
                .GetTypes()
                .Where(type => desiredType.IsAssignableFrom(type) && type.IsInterface == false);
    }
}