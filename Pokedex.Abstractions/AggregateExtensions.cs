using System;
using System.Collections.Generic;
using System.Linq;

namespace Pokedex.Abstractions
{
    public static class DictionaryExtensions
    {
        public static TItem GetValueOrDefault<TKey, TItem>(this Dictionary<TKey, TItem> dictionary, TKey key)
        {
            if (dictionary.ContainsKey(key))
            {
                return dictionary[key];
            }

            return default;
        }
    }
    
    public static class AggregateExtensions
    {
        public static T AggregateIfPossible<T>(this IEnumerable<T> items, Func<T, T, T> func, T ifDefault = default)
        {
            var list = items.ToList();

            if (list.Count < 2)
            {
                if (list.Count < 1)
                    return ifDefault;

                return list.Single();
            }

            return list.Aggregate(func);
        }
    }
}