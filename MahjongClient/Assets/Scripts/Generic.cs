using System;
using System.Collections.Generic;
using System.Linq;
using Random = UnityEngine.Random;

namespace Mahjong
{
    internal static class Generic
    {
        internal static bool IsInRange(int number, (int begin, int end) range)
        {
            return IsInRange(number, range.begin, range.end);
        }

        internal static bool IsInRange(int number, int begin, int end)
        {
            return number >= begin && number < end;
        }

        internal static bool IsInRangeInclusive(int number, (int begin, int end) range)
        {
            return IsInRangeInclusive(number, range.begin, range.end);
        }

        internal static bool IsInRangeInclusive(int number, int begin, int end)
        {
            return number >= begin && number <= end;
        }

        internal static IEnumerable<int> Range((int begin, int end) range, int step = 1)
        {
            return Range(range.begin, range.end, step);
        }

        internal static IEnumerable<int> Range(int begin, int end, int step = 1)
        {
            Func<int, int, bool> funcCompare = step > 0 ? (x, y) => x < y : (x, y) => x > y;
            for (int i = begin; funcCompare(i, end); i += step)
            {
                yield return i;
            }
        }

        internal static IEnumerable<int> RangeInclusive((int begin, int end) range, int step = 1)
        {
            return RangeInclusive(range.begin, range.end, step);
        }

        internal static IEnumerable<int> RangeInclusive(int begin, int end, int step = 1)
        {
            Func<int, int, bool> funcCompare = step > 0 ? (x, y) => x <= y : (x, y) => x >= y;
            for (int i = begin; funcCompare(i, end); i += step)
            {
                yield return i;
            }
        }

        internal static int GetNextInRangeInclusive(int number, (int begin, int end) range)
        {
            return GetNextInRangeInclusive(number, range.begin, range.end);
        }

        internal static int GetNextInRangeInclusive(int number, int begin, int end)
        {
            return (number + 1 - begin) % (end - begin + 1) + begin;
        }
    }

    internal static class GenericExtension
    {
        internal static IEnumerable<T> Repeat<T> (this T source, int count)
        {
            for (int i = 0; i < count; ++i)
            {
                yield return source;
            }
        }

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