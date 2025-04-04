using System;
using System.Collections.Generic;
using System.Linq;
using Random = UnityEngine.Random;

namespace Mahjong
{
    internal static class Generic
    {
        internal static IEnumerable<int> Range(int from, int to, int step = 1)
        {
            Func<int, int, bool> funcCompare = step > 0 ? (x, y) => x < y : (x, y) => x > y;
            for (int i = from; funcCompare(i, to); i += step)
            {
                yield return i;
            }
        }

        internal static IEnumerable<int> RangeInclusive(int from, int to, int step = 1)
        {
            Func<int, int, bool> funcCompare = step > 0 ? (x, y) => x <= y : (x, y) => x >= y;
            for (int i = from; funcCompare(i, to); i += step)
            {
                yield return i;
            }
        }
    }

    internal static class GenericExtension
    {
        internal static List<T> Swap<T>(this List<T> list, int index1, int index2)
        {
            (list[index1], list[index2]) = (list[index2], list[index1]);
            return list;
        }

        internal static List<T> RandomShuffle<T>(this List<T> list)
        {
            int count = list.Count();
            for (int i = 0; i < count - 1; ++i)
            {
                list.Swap(i, Random.Range(i + 1, count - 1));
            }
            return list;
        }

        internal static IEnumerable<T> RandomShuffle<T>(this IEnumerable<T> enumerable)
        {
            List<int> indices = Generic.Range(0, enumerable.Count()).ToList().RandomShuffle();
            foreach (int index in indices)
            {
                yield return enumerable.ElementAt(index);
            }
        }
    }
}