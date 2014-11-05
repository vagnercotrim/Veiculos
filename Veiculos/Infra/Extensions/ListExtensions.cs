using System.Collections.Generic;
using System.Linq;

namespace Veiculos.Infra.Extensions
{
    public static class ListExtensions
    {

        public static bool IsNullOrEmpty<T>(this IList<T> items)
        {
            return items == null || !items.Any();
        }

    }
}