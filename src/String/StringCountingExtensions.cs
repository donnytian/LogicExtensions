using System.Text;

namespace LogicExtensions;

/// <summary>
/// Provides counting extensions for the <see cref="string"/> class.
/// </summary>
public static class StringCountingExtensions
{
    /// <summary>
    /// Calculates the amount of bytes occupied by the input string.
    /// </summary>
    /// <param name="s">The input string to check</param>
    /// <returns>The total size of the input string in bytes</returns>
    public static int Size(this string? s)
    {
        if (string.IsNullOrEmpty(s))
        {
            return 0;
        }

        // Simple implementation for utf16 which is the default encoding where chars are of a fixed size.
        return s!.Length * sizeof(char);
    }

    /// <summary>
    /// Calculates the amount of bytes occupied by the input string encoded as the encoding specified.
    /// </summary>
    /// <param name="s">The input string to check</param>
    /// <param name="encoding">The encoding to use</param>
    /// <returns>The total size of the input string in bytes</returns>
    public static int Size(this string? s, Encoding? encoding)
    {
        if (encoding == null)
        {
            return s.Size();
        }

        return string.IsNullOrEmpty(s) ? 0 : encoding.GetByteCount(s);
    }
}
