using System;
using UnityEngine;

namespace Sources.Attributes
{
    public class TypedPopupAttribute : PropertyAttribute
    {
        public TypedPopupAttribute(Type type) =>
            Type = type;

        public Type Type { get; }
    }
}