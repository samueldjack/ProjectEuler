using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace ProjectEuler
{
    public static class Functional
    {
        public static IEnumerable<TAccumulate> LazilyAggregate<T, TAccumulate>(this IEnumerable<T> sequence, TAccumulate seed, Func<T, TAccumulate, TAccumulate> aggregator)
        {
            var accumulatedValue = seed;
            yield return seed;

            foreach (var item in sequence)
            {
                accumulatedValue = aggregator(item, accumulatedValue);
                yield return accumulatedValue;
            }
        }

        public static IEnumerable<T> Unfold<T>(this IList<T> seedList, Func<IList<T>, T> generator)
        {
            List<T> previousItems = new List<T>(seedList);

            // enumerate all the items in the seed list
            foreach (T item in seedList)
            {
                yield return item;
            }

            // now extend the list
            while (true)
            {
                T newItem = generator(previousItems);
                previousItems.Add(newItem);
                yield return newItem;
            }
        }

        public static IEnumerable<T> Unfold<T>(this T seed, Func<T, T> generator)
        {
            // include seed in the sequence
            yield return seed;

            T current = seed;

            // now continue the sequence
            while (true)
            {
                current = generator(current);
                yield return current;
            }
        }

        public static Func<T, TResult> Memoize<T, TResult>(this Func<T, TResult> function)
        {
            var cache = new Dictionary<T, TResult>();

            return (T arg) =>
            {
                TResult result;
                if (cache.TryGetValue(arg, out result))
                {
                    return result;
                }
                else
                {
                    result = function(arg);
                    cache.Add(arg, result);
                    return result;
                }
            };
        }

        public static IEnumerable<IEnumerable<T>> Zip<T>(this IEnumerable<IEnumerable<T>> sequences)
        {
            // get the enumerators for each sequence
            var enumerators = sequences.Select(sequence => sequence.GetEnumerator()).ToList();

            var activeEnumerators = new List<IEnumerator<T>>();
            var items = new List<T>();

            while (enumerators.Count > 0)
            {
                items.Clear();
                activeEnumerators.Clear();

                foreach (var enumerator in enumerators)
                {
                    if (enumerator.MoveNext())
                    {
                        items.Add(enumerator.Current);
                        activeEnumerators.Add(enumerator);
                    }
                }

                if (items.Count > 0)
                {
                    yield return items;
                }

                enumerators.Clear();
                enumerators.AddRange(activeEnumerators);
            }
        }

        /// <summary>
        /// Slices a sequence into a sub-sequences each containing maxItemsPerSlice, except for the last
        /// which will contain any items left over
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sequence"></param>
        /// <param name="maxItemsPerSlice"></param>
        /// <returns></returns>
        public static IEnumerable<IEnumerable<T>> Slice<T>(this IEnumerable<T> sequence, int maxItemsPerSlice)
        {
            if (maxItemsPerSlice <= 0)
            {
                throw new ArgumentOutOfRangeException("maxItemsPerSlice", "maxItemsPerSlice must be greater than 0");
            }

            List<T> slice = new List<T>(maxItemsPerSlice);

            foreach (var item in sequence)
            {
                slice.Add(item);

                if (slice.Count == maxItemsPerSlice)
                {
                    yield return slice.ToArray();
                    slice.Clear();
                }
            }

            // return the "crumbs" that 
            // didn't make it into a full slice
            if (slice.Count > 0)
            {
                yield return slice.ToArray();
            }
        }
    }
}
