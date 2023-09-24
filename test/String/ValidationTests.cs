using LogicExtensions;

namespace Tests.String;

public class ValidationTests
{
    [Theory]
    [InlineData(new[] { "Bob", "Alice", "Carl" }, "David", StringComparison.OrdinalIgnoreCase, false)]
    [InlineData(new[] { "Bob", "Alice", null }, null, StringComparison.OrdinalIgnoreCase, true)]
    [InlineData(null, "Me", StringComparison.OrdinalIgnoreCase, false)]
    [InlineData(new[] { "Bob", "Alice", "Carl" }, "CARL", StringComparison.CurrentCultureIgnoreCase, true)]
    [InlineData(new[] { "Bob", "Alice", "Carl" }, "alice", StringComparison.CurrentCulture, false)]
    public void EnumerableContainsTests(IEnumerable<string> enumerable, string value, StringComparison comparison, bool result)
    {
        AssertBool(() => enumerable.Contains(value, comparison), result);
    }

    [Theory]
    [InlineData("d", StringComparison.OrdinalIgnoreCase, new[] { "Bob", "Alice", "Carl" }, false)]
    [InlineData(null, StringComparison.OrdinalIgnoreCase, new[] { "Bob", "Alice", null }, true)]
    [InlineData(null, StringComparison.OrdinalIgnoreCase, new[] { "Me" }, false)]
    [InlineData("CARL", StringComparison.CurrentCultureIgnoreCase, new[] { "Bob", "Alice", "Carl" }, true)]
    [InlineData("alice", StringComparison.CurrentCulture, new[] { "Bob", "Alice", "Carl" }, false)]
    public void EqualsAny(string s, StringComparison comparison, string[] targets, bool expected)
    {
        AssertBool(() => s.EqualsAny(comparison, targets), expected);
    }

    private static void AssertBool(Func<bool> action, bool expected)
    {
        // Arrange, Act
        var actual = action();

        // Assert
        Assert.Equal(expected, actual);
    }
}
