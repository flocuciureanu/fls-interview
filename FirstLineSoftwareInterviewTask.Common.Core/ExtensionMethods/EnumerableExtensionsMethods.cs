using System.Collections.Generic;
using System.Linq;

namespace FirstLineSoftwareInterviewTask.Common.Core.ExtensionMethods
{
    public static class EnumerableExtensionsMethods
    {
        public static bool HasValue<T>(this IEnumerable<T> enumerable)
        {
            return enumerable != null && enumerable.Any();
        }
    }
}