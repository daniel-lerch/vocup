using System;
using System.Collections.Generic;
using System.Linq;

namespace Vocup.Util;

public static class StringExtensions
{
    /// <summary>
    /// Checks whether one of the specified chars is present in this string.
    /// </summary>
    public static bool ContainsAny(this string value, string chars)
    {
        foreach (char item in chars)
        {
            if (value.IndexOf(item) != -1) return true;
        }
        return false;
    }

    public static string[] SplitAndTrim(this string value, char[] separator, StringSplitOptions options)
    {
        string[] result = value.Split(separator, options);

        for (int i = 0; i < result.Length; i++)
        {
            result[i] = result[i].Trim();
        }

        return result;
    }
}

public static class CollectionExtensions
{
    /// <summary>
    /// Shuffles a <see cref="IList{T}"/> by reordering all elements randomly.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list"></param>
    /// <remarks>https://stackoverflow.com/questions/273313/randomize-a-listt</remarks>
    public static void Shuffle<T>(this IList<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = Random.Shared.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }

    public static int NextIndexOf<T>(this IList<T> collection, Predicate<T> predicate, int previousIndex)
    {
        int first, last;

        if (previousIndex > collection.Count - 2)
        {
            first = 0;
            last = collection.Count - 1;
        }
        else
        {
            first = previousIndex + 1;
            last = previousIndex;
        }

        for (int i = first; i != last; i = (i + 1) % collection.Count)
        {
            if (predicate(collection[i]))
                return i;
        }

        return -1;
    }
}
