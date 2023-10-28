using System.Collections.Generic;

namespace Sources.Extensions.Lists
{
    public static partial class IListExtensions
    {
        public static void Replace<T>(this List<T> list, T oldValue, T newValue)
        {
            list.Remove(oldValue);
            list.Add(newValue);
        }
    }
}