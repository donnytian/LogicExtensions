namespace LogicExtensions;

/// <summary>
/// Provides validation extensions for <see cref="string"/> class.
/// </summary>
public static class StringValidationExtensions
{
    /// <summary>
    /// Checks whether the string is equals with anyone in the given string list.
    /// </summary>
    /// <param name="s">The string to be compared.</param>
    /// <param name="comparisonType">The string comparison type.</param>
    /// <param name="enumerable">The list contains candidates.</param>
    /// <returns>
    /// true if the input string equals with anyone in the list; otherwise, false.
    /// </returns>
    public static bool EqualsAny(this string? s, StringComparison comparisonType, params string[] enumerable)
    {
        return enumerable.Any(e => string.Compare(s, e, comparisonType) == 0);
    }

    /// <summary>
    /// Checks whether the enumerable contains the given string.
    /// </summary>
    /// <param name="enumerable">The strings to be checked.</param>
    /// <param name="value">The sub-string to be searched.</param>
    /// <param name="comparisonType">The string comparison type.</param>
    /// <returns>True if sub-string found; otherwise false.</returns>
    public static bool Contains(this IEnumerable<string>? enumerable, string value, StringComparison comparisonType)
    {
        if (enumerable == null)
        {
            return false;
        }

        return enumerable.Any(s => string.Compare(s, value, comparisonType) == 0);
    }

    /// <summary>
    /// Indicates whether the specified string is null or an <see cref="F:System.String.Empty" /> string.
    /// </summary>
    /// <param name="s">The string to test.</param>
    /// <returns>true if the <paramref name="s" /> parameter is null or an empty string (""); otherwise, false.</returns>
    public static bool IsNullOrEmpty(this string? s) => string.IsNullOrEmpty(s);

    /// <summary>
    /// Indicates whether a specified string is null, empty, or consists only of white-space characters.
    /// </summary>
    /// <param name="s">The string to test.</param>
    /// <returns>
    /// true if the <paramref name="s" /> parameter is null or <see cref="F:System.String.Empty" />,
    /// or if <paramref name="s" /> consists exclusively of white-space characters.
    /// </returns>
    public static bool IsNullOrWhiteSpace(this string? s) => string.IsNullOrWhiteSpace(s);

    /* TODO
     * IsInteger
     * IsNumeric
     * IsAlpha
     * IsAlphaNumeric
     * IsValidIPv4
     * IsEmailAddress
     * IsDateTime
     */
}
