namespace LogicExtensions;

/// <summary>
/// Provides math extensions for <see cref="byte"/> class.
/// </summary>
public static class ByteExtensions
{
    /// <summary>
    /// Gets the bitwise NOT result.
    /// </summary>
    /// <param name="bytes">The bytes to be calculated.</param>
    /// <returns>The bitwise NOT result bytes.</returns>
    public static byte[] Not(this byte[] bytes) => bytes.Select(b => (byte)~b).ToArray();

    /// <summary>
    /// Gets the bitwise AND result.
    /// </summary>
    /// <param name="bytes">The bytes to be calculated.</param>
    /// <param name="another">Another operand to be calculated.</param>
    /// <returns>The bitwise AND result bytes.</returns>
    public static byte[] And(this IEnumerable<byte> bytes, IEnumerable<byte> another) => bytes.Zip(another, (a, b) => (byte)(a & b)).ToArray();

    /// <summary>
    /// Gets the bitwise OR result.
    /// </summary>
    /// <param name="bytes">The bytes to be calculated.</param>
    /// <param name="another">Another operand to be calculated.</param>
    /// <returns>The bitwise OR result bytes.</returns>
    public static byte[] Or(this IEnumerable<byte> bytes, IEnumerable<byte> another) => bytes.Zip(another, (a, b) => (byte)(a | b)).ToArray();

    /// <summary>
    /// Checks if the number from given bytes is greater than another number.
    /// </summary>
    /// <param name="bytes">The bytes to be calculated.</param>
    /// <param name="another">Another operand to be calculated.</param>
    /// <returns>True if it's greater; otherwise false.</returns>
    public static bool GreaterThan(this IEnumerable<byte> bytes, IEnumerable<byte> another) => bytes.CompareTo(another) > 0;

    /// <summary>
    /// Checks if the number from given bytes is greater or equals to another number.
    /// </summary>
    /// <param name="bytes">The bytes to be calculated.</param>
    /// <param name="another">Another operand to be calculated.</param>
    /// <returns>True if it's greater or equal; otherwise false.</returns>
    public static bool GreaterOrEqual(this IEnumerable<byte> bytes, IEnumerable<byte> another) => bytes.CompareTo(another) >= 0;

    /// <summary>
    /// Checks if the number from given bytes is less or equals to another number.
    /// </summary>
    /// <param name="bytes">The bytes to be calculated.</param>
    /// <param name="another">Another operand to be calculated.</param>
    /// <returns>True if it's less or equal; otherwise false.</returns>
    public static bool LessOrEqual(this IEnumerable<byte> bytes, IEnumerable<byte> another) => bytes.CompareTo(another) <= 0;

    /// <summary>
    /// Checks if the number from given bytes is less than another number.
    /// </summary>
    /// <param name="bytes">The bytes to be calculated.</param>
    /// <param name="another">Another operand to be calculated.</param>
    /// <returns>True if it's less; otherwise false.</returns>
    public static bool LessThan(this IEnumerable<byte> bytes, IEnumerable<byte> another) => bytes.CompareTo(another) < 0;

    /// <summary>
    /// Checks if the number that represented by the given bytes is equal to another number.
    /// </summary>
    /// <param name="bytes">The bytes to be calculated.</param>
    /// <param name="another">Another operand to be calculated.</param>
    /// <returns>True if it's equal; otherwise false.</returns>
    public static bool EqualsTo(this IEnumerable<byte> bytes, IEnumerable<byte> another) => bytes.CompareTo(another) == 0;

    /// <summary>
    /// Compares the number from bytes to another number.
    /// </summary>
    /// <param name="left">The bytes to be calculated.</param>
    /// <param name="right">Another operand to be calculated.</param>
    /// <param name="bigEndian">True to use big endian, false to use little endian, null to follow system setting.</param>
    /// <returns>A positive integer if it's greater; Zero if they are equal and a negative integer if it's less than.</returns>
    public static int CompareTo(this IEnumerable<byte> left, IEnumerable<byte> right, bool? bigEndian = null)
    {
        var bytes = left as byte[] ?? left.ToArray();
        var another = right as byte[] ?? right.ToArray();
        var length = bytes.Length >= another.Length ? bytes.Length : another.Length;

        if (!bigEndian.HasValue)
        {
            bigEndian = !BitConverter.IsLittleEndian;
        }

        if (bigEndian.Value)
        {
            var aLengthDiff = length - bytes.Length;
            var bLengthDiff = length - another.Length;

            for (var i = 0; i < length; i++)
            {
                var a = i < aLengthDiff ? (byte)0 : bytes[i - aLengthDiff];
                var b = i < bLengthDiff ? (byte)0 : another[i - bLengthDiff];

                if (a == b)
                {
                    continue;
                }

                return a > b ? 1 : -1;
            }
        }
        else
        {
            for (var i = length - 1; i >= 0; i--)
            {
                var a = i >= bytes.Length ? (byte)0 : bytes[i];
                var b = i >= another.Length ? (byte)0 : another[i];

                if (a == b)
                {
                    continue;
                }

                return a > b ? 1 : -1;
            }
        }

        return 0;
    }
}
