using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

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

    public static IOrderedEnumerable<T> Shuffle<T>(this IEnumerable<T> enumerable)
    {
        return enumerable.OrderBy(x => Random.Shared.Next());
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

public static class AvaloniaWinFormsExtensions
{
    public static Avalonia.Size ToAvaloniaSize(this System.Drawing.Size size)
    {
        return new(size.Width, size.Height);
    }

    public static System.Drawing.Size ToSystemDrawingSize(this Avalonia.Size size)
    {
        return new((int)size.Width, (int)size.Height);
    }

    public static Avalonia.Rect ToAvaloniaRect(this System.Drawing.Rectangle rect)
    {
        return new(rect.X, rect.Y, rect.Width, rect.Height);
    }

    public static System.Drawing.Rectangle ToSystemDrawingRect(this Avalonia.Rect rect)
    {
        return new((int)rect.X, (int)rect.Y, (int)rect.Width, (int)rect.Height);
    }

    public static Avalonia.Controls.WindowState ToAvaloniaWindowState(this FormWindowState state)
    {
        return state switch
        {
            FormWindowState.Normal => Avalonia.Controls.WindowState.Normal,
            FormWindowState.Minimized => Avalonia.Controls.WindowState.Minimized,
            FormWindowState.Maximized => Avalonia.Controls.WindowState.Maximized,
            _ => throw new ArgumentOutOfRangeException(nameof(state), state, null)
        };
    }

    public static FormWindowState ToFormWindowState(this Avalonia.Controls.WindowState state)
    {
        return state switch
        {
            Avalonia.Controls.WindowState.Normal => FormWindowState.Normal,
            Avalonia.Controls.WindowState.Minimized => FormWindowState.Minimized,
            Avalonia.Controls.WindowState.Maximized => FormWindowState.Maximized,
            Avalonia.Controls.WindowState.FullScreen => FormWindowState.Maximized,
            _ => throw new ArgumentOutOfRangeException(nameof(state), state, null)
        };
    }
}
