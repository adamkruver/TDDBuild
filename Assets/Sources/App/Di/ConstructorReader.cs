using System;
using System.Linq;

namespace Sources.App.Di
{
    public class ConstructorReader
    {
        public Type[] GetParametersType(Type type) =>
            (from constructor in type.GetConstructors()
                from parameter in constructor.GetParameters()
                select parameter.ParameterType
            ).ToArray();
    }
}