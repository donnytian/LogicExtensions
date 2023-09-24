using LogicExtensions;

namespace Tests.String;

public class ManipulationTests
{
    [Theory]
    [InlineData("", 5, null, "")]
    [InlineData(null, 10, null, "")]
    [InlineData("dummy", 5, ".", "dummy")]
    [InlineData("this is a test", 10, "...", "this is a ...")]
    [InlineData("123456", 10, "...", "123456")]
    public void TruncateTests(string s, int length, string suffix, string expected)
    {
        AssertString(() => s.Truncate(length, suffix), expected);
    }

    [Theory]
    [InlineData("", "")]
    [InlineData(null, "")]
    [InlineData("dummy", "ymmud")]
    public void ReverseTests(string s, string expected)
    {
        AssertString(s.Reverse, expected);
    }

    [Theory]
    [InlineData("", 5, 2, "")]
    [InlineData("dummy", 5, 5, "dummy")]
    [InlineData("dummy", 4, 5, "dummy")]
    [InlineData("dummy", 3, 1, "dummy")]
    [InlineData("dummy", 3, 2, "dumym")]
    public void ReverseByIndexTests(string s, int index, int length, string expected)
    {
        AssertString(() => s.Reverse(index, length), expected);
    }

    [Theory]
    [InlineData("", 1, "")]
    [InlineData(null, 1, "")]
    [InlineData("dummy", 2, "du")]
    [InlineData("dummy", 0, "")]
    [InlineData("dummy", 100, "dummy")]
    [InlineData("dummy", 5, "dummy")]
    public void Left(string s, int length, string expected)
    {
        AssertString(() => s.Left(length), expected);
    }

    [Theory]
    [InlineData("", "a", 1, "")]
    [InlineData(null, "a", 1, "")]
    [InlineData("dummy####hi", "##", 2, "dummy##")]
    [InlineData("dummy@my.com", "@", 0, "dummy")]
    [InlineData("dummy.g.cs", ".", 1, "dummy.g")]
    [InlineData("dummy.g.cs", ".", 5, "")]
    public void LeftOfNOccurrence(string s, string token, int skip, string expected)
    {
        AssertString(() => s.LeftOf(token, skip), expected);
    }

    [Theory]
    [InlineData("", char.MaxValue, "")]
    [InlineData(null, char.MinValue, "")]
    [InlineData("dummy####hi", '#', "dummy")]
    [InlineData("dummy@my.com", '@', "dummy")]
    [InlineData("dummy.g.cs", '.', "dummy")]
    public void LeftOfChar(string s, char token, string expected)
    {
        AssertString(() => s.LeftOf(token), expected);
    }

    [Theory]
    [InlineData("", null, "")]
    [InlineData(null, "a", "")]
    [InlineData("dummy####hi", "##", "dummy")]
    [InlineData("dummy@my.com", "@", "dummy")]
    [InlineData("dummy.g.cs", ".", "dummy")]
    public void LeftOf(string s, string token, string expected)
    {
        AssertString(() => s.LeftOf(token), expected);
    }

    [Theory]
    [InlineData("", null, "")]
    [InlineData(null, "a", "")]
    [InlineData("dummy####hi", "##", "dummy##")]
    [InlineData("dummy@my.com", "@", "dummy")]
    [InlineData("dummy.g.cs.com", ".", "dummy.g.cs")]
    public void LeftOfLast(string s, string token, string expected)
    {
        AssertString(() => s.LeftOfLast(token), expected);
    }

    [Theory]
    [InlineData("", 1, "")]
    [InlineData(null, 1, "")]
    [InlineData("dummy", 2, "my")]
    [InlineData("dummy", 0, "")]
    [InlineData("dummy", 100, "dummy")]
    [InlineData("dummy", 5, "dummy")]
    public void Right(string s, int length, string expected)
    {
        AssertString(() => s.Right(length), expected);
    }

    [Theory]
    [InlineData("", "a", 1, "")]
    [InlineData(null, "a", 1, "")]
    [InlineData("dummy####hi", "##", 2, "hi")]
    [InlineData("dummy@my.com", "@", 0, "my.com")]
    [InlineData("dummy.g.cs", ".", 1, "cs")]
    [InlineData("dummy.g.cs", ".", 5, "")]
    public void RightOfNOccurrence(string s, string token, int skip, string expected)
    {
        AssertString(() => s.RightOf(token, skip), expected);
    }

    [Theory]
    [InlineData("", char.MinValue, "")]
    [InlineData(null, char.MaxValue, "")]
    [InlineData("dummy####hi", '#', "###hi")]
    [InlineData("dummy@my.com", '@', "my.com")]
    [InlineData("dummy.g.cs", '.', "g.cs")]
    public void RightOfChar(string s, char token, string expected)
    {
        AssertString(() => s.RightOf(token), expected);
    }

    [Theory]
    [InlineData("", null, "")]
    [InlineData(null, "a", "")]
    [InlineData("dummy####hi", "##", "##hi")]
    [InlineData("dummy@my.com", "@", "my.com")]
    [InlineData("dummy.g.cs", ".", "g.cs")]
    public void RightOf(string s, string token, string expected)
    {
        AssertString(() => s.RightOf(token), expected);
    }

    [Theory]
    [InlineData("", null, "")]
    [InlineData(null, "a", "")]
    [InlineData("dummy####hi", "##", "hi")]
    [InlineData("dummy@my.com", "@", "my.com")]
    [InlineData("dummy.g.cs.com", ".", "com")]
    public void RightOfLast(string s, string token, string expected)
    {
        AssertString(() => s.RightOfLast(token), expected);
    }

    private void AssertString(Func<string> action, string expected)
    {
        // Arrange, Act
        var actual = action();

        // Assert
        Assert.Equal(expected, actual);
    }
}
