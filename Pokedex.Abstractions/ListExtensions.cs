using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Pokedex.Abstractions
{
    public static class ListExtensions
    {
        public static IEnumerable<string> Cancel(this IEnumerable<string> source, IEnumerable<string> target)
        {
            var hashSet = target.ToHashSet();

            foreach (var s in source)
            {
                if (hashSet.Contains(s))
                {
                    hashSet.Remove(s);
                    continue;
                }

                yield return s;
            }
        }
    }
}