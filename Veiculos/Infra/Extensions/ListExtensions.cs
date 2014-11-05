using System.Linq;

namespace System.Collections.Generic
{
    public static class ListExtensions
    {

        public static bool IsNullOrEmpty<T>(this IList<T> items)
        {
            return items == null || !items.Any();
        }

    }
}