namespace LogicExtensions;

/// <summary>
/// Provides manipulation extensions for <see cref="string"/> class.
/// </summary>
public static class StringManipulationExtensions
{
    /// <summary>
    /// Gets a truncated string based on the input.
    /// </summary>
    /// <param name="s">The string to be truncated.</param>
    /// <param name="maxLength">The maximum length of the result string. Not included suffix.</param>
    /// <param name="suffix">The suffix of the result string if there is any character(s) truncated.</param>
    /// <returns>The truncated string.</returns>
    public static string Truncate(this string? s, int maxLength, string suffix = "")
    {
        if (string.IsNullOrEmpty(s))
        {
            return string.Empty;
        }

        return s.Length <= maxLength ? s : s.Substring(0, maxLength) + suffix;
    }

    /// <summary>
    /// Reverses all characters in input string.
    /// </summary>
    /// <param name="s">The input string to reverse.</param>
    /// <returns>A reversed version from input.</returns>
    public static string Reverse(this string? s)
    {
        return string.IsNullOrEmpty(s) ? string.Empty : Reverse(s, 0, s.Length);
    }

    /// <summary>
    /// Reverses all characters in input within the range from startIndex till length.
    /// </summary>
    /// <param name="s">The input string to reverse.</param>
    /// <param name="startIndex">The startIndex startIndex of the input string to begin reversing.</param>
    /// <param name="count">The amount of characters to reverse.</param>
    /// <returns>A reversed version from input.</returns>
    public static string Reverse(this string? s, int startIndex, int count)
    {
        if (string.IsNullOrEmpty(s))
        {
            return string.Empty;
        }

        if (startIndex < 0)
        {
            startIndex = 0;
        }

        if (startIndex >= s.Length - 1)
        {
            return s;
        }

        if (count > s.Length - startIndex)
        {
            count = s.Length - startIndex;
        }

        if (count <= 1)
        {
            return s;
        }

        var characters = s.ToCharArray();

        Array.Reverse(characters, startIndex, count);

        return new string(characters);
    }

    /// <summary>
    /// Extracts left most n characters.
    /// </summary>
    /// <param name="s">The string to extract.</param>
    /// <param name="length">Length of extract.</param>
    /// <returns>The left n characters of the given string.</returns>
    public static string Left(this string? s, int length)
    {
        if (string.IsNullOrEmpty(s) || length <= 0)
        {
            return string.Empty;
        }

        if (length > s.Length)
        {
            return s;
        }

        return s.Substring(0, length);
    }

    /// <summary>
    ///  Extracts the left part of the first occurrence of the given token.
    /// </summary>
    /// <param name="s">The string to extract.</param>
    /// <param name="token">The token to extract its left part sub-string.</param>
    /// <param name="comparison">The string comparison type.</param>
    /// <returns>The left part of the first occurrence of the given toke; empty string if not found.</returns>
    public static string LeftOf(this string? s, string token, StringComparison comparison = StringComparison.CurrentCulture)
    {
        return s.LeftOf(token, 0, comparison);
    }

    /// <summary>
    ///  Extracts the left part of the specified occurrence of the given token.
    /// </summary>
    /// <param name="s">The string to extract.</param>
    /// <param name="token">The token to extract its left part sub-string.</param>
    /// <param name="skip">Number of token occurrences to skip.</param>
    /// <returns>The left part of the found occurrence of the given token; empty string if not found.</returns>
    public static string LeftOf(this string? s, char token, int skip = 0)
    {
        return s.LeftOf(token.ToString(), skip, StringComparison.Ordinal);
    }

    /// <summary>
    ///  Extracts the left part of the specified occurrence of the given token.
    /// </summary>
    /// <param name="s">The string to extract.</param>
    /// <param name="token">The token to extract its left part sub-string.</param>
    /// <param name="skip">Number of token occurrences to skip.</param>
    /// <param name="comparison">The string comparison type.</param>
    /// <returns>The left part of the found occurrence of the given token; empty string if not found.</returns>
    public static string LeftOf(this string? s, string token, int skip, StringComparison comparison = StringComparison.CurrentCulture)
    {
        if (string.IsNullOrEmpty(s) || string.IsNullOrEmpty(token) || skip < 0)
        {
            return string.Empty;
        }

        var skipped = 0;
        var index = s.IndexOf(token, comparison);

        while (index >= 0)
        {
            if (skipped < skip)
            {
                skipped++;
                index = s.IndexOf(token, index + 1, comparison);
            }
            else
            {
                return s.Substring(0, index);
            }
        }

        return string.Empty;
    }

    /// <summary>
    ///  Extracts the left part of the last occurrence of the given token.
    /// </summary>
    /// <param name="s">The string to extract.</param>
    /// <param name="token">The token to extract its left part sub-string.</param>
    /// <param name="comparison">The string comparison type.</param>
    /// <returns>The left part of the last occurrence of the given token; empty string if not found.</returns>
    public static string LeftOfLast(this string? s, string token, StringComparison comparison = StringComparison.CurrentCulture)
    {
        if (string.IsNullOrEmpty(s) || string.IsNullOrEmpty(token))
        {
            return string.Empty;
        }

        var index = s.LastIndexOf(token, comparison);

        return index > 0 ? s.Substring(0, index) : string.Empty;
    }

    /// <summary>
    /// Extracts right most n characters.
    /// </summary>
    /// <param name="s">The string to extract.</param>
    /// <param name="length">Length of extract.</param>
    /// <returns>The right n characters of the given string.</returns>
    public static string Right(this string? s, int length)
    {
        if (string.IsNullOrEmpty(s) || length <= 0)
        {
            return string.Empty;
        }

        if (length > s.Length)
        {
            return s;
        }

        return s.Substring(s.Length - length);
    }

    /// <summary>
    ///  Extracts the right part of the first occurrence of the given token.
    /// </summary>
    /// <param name="s">The string to extract.</param>
    /// <param name="token">The token to extract its right part sub-string.</param>
    /// <param name="comparison">The string comparison type.</param>
    /// <returns>The right part of the first occurrence of the given token; empty string if not found.</returns>
    public static string RightOf(this string? s, string token, StringComparison comparison = StringComparison.CurrentCulture)
    {
        return s.RightOf(token, 0, comparison);
    }

    /// <summary>
    ///  Extracts the right part of the specified occurrence of the given token.
    /// </summary>
    /// <param name="s">The string to extract.</param>
    /// <param name="token">The token to extract its right part sub-string.</param>
    /// <param name="skip">Number of token occurrences to skip.</param>
    /// <returns>The right part of the found occurrence of the given token; empty string if not found.</returns>
    public static string RightOf(this string? s, char token, int skip = 0)
    {
        return s.RightOf(token.ToString(), skip, StringComparison.Ordinal);
    }

    /// <summary>
    ///  Extracts the right part of the specified occurrence of the given token.
    /// </summary>
    /// <param name="s">The string to extract.</param>
    /// <param name="token">The token to extract its right part sub-string.</param>
    /// <param name="skip">Number of token occurrences to skip.</param>
    /// <param name="comparison">The string comparison type.</param>
    /// <returns>The right part of the found occurrence of the given token; empty string if not found.</returns>
    public static string RightOf(this string? s, string token, int skip, StringComparison comparison = StringComparison.CurrentCulture)
    {
        if (string.IsNullOrEmpty(s) || string.IsNullOrEmpty(token) || skip < 0)
        {
            return string.Empty;
        }

        var skipped = 0;
        var index = s.IndexOf(token, comparison);

        while (index >= 0)
        {
            if (skipped < skip)
            {
                skipped++;
                index = s.IndexOf(token, index + 1, comparison);
            }
            else
            {
                return s.Substring(index + token.Length);
            }
        }

        return string.Empty;
    }

    /// <summary>
    ///  Extracts the right part of the last occurrence of the given token.
    /// </summary>
    /// <param name="s">The string to extract.</param>
    /// <param name="token">The token to extract its right part sub-string.</param>
    /// <param name="comparison">The string comparison type.</param>
    /// <returns>The right part of the last occurrence of the given token.</returns>
    public static string RightOfLast(this string? s, string token, StringComparison comparison = StringComparison.CurrentCulture)
    {
        if (string.IsNullOrEmpty(s) || string.IsNullOrEmpty(token))
        {
            return string.Empty;
        }

        var index = s.LastIndexOf(token, comparison);

        return index >= 0 ? s.Substring(index + token.Length) : string.Empty;
    }

    /* TODO
     * EncodeAs(Encoding)
     * TrimStart(string, StringComparison, int=0)
     * TrimEnd(string, StringComparison, int=0)
     */
}
