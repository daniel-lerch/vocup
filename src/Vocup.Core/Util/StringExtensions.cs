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
}
