using LogicExtensions;

namespace Tests.String;

public class PatternMatchTests
{
    private const string SimpleString = "This is a test string!";
    private const string LocalizedString = "七个隆冬强, 八个隆冬强-> 不服咋滴!";

    [Theory]
    [InlineData(SimpleString, "This?is a ??st strin??")]
    [InlineData(SimpleString, "This is a test string?")]
    [InlineData(SimpleString, "?his is a test string!")]
    [InlineData(LocalizedString, "?个隆冬强, ?个隆冬强?> ??咋滴!")]
    public void IsMatch_SingleWildcard_ShouldMatched(string s, string pattern)
    {
        // Arrange

        // Act
        var isMatched = s.IsMatch(pattern);

        // Assert
        Assert.True(isMatched);
    }

    [Theory]
    [InlineData(SimpleString, "?")]
    [InlineData(SimpleString, "?his?is a ?est string ")]
    [InlineData(SimpleString, "?his?is a ?est string")]
    [InlineData(LocalizedString, "?个隆冬强, ?个隆冬强-> ??咋滴")]
    public void IsMatch_SingleWildcard_ShouldNotMatched(string s, string pattern)
    {
        // Arrange

        // Act
        var isMatched = s.IsMatch(pattern);

        // Assert
        Assert.False(isMatched);
    }

    [Theory]
    [InlineData(SimpleString, "*")]
    [InlineData(SimpleString, "This* is a test string!")]
    [InlineData(SimpleString, "This is a test string?*")]
    [InlineData(SimpleString, "*This is *test *!")]
    [InlineData(LocalizedString, "*个隆冬强, *> *咋滴!")]
    public void IsMatch_MultipleWildcard_ShouldMatched(string s, string pattern)
    {
        // Arrange

        // Act
        var isMatched = s.IsMatch(pattern);

        // Assert
        Assert.True(isMatched);
    }

    [Theory]
    [InlineData(SimpleString, " *")]
    [InlineData(SimpleString, "This is not a test string!")]
    [InlineData(SimpleString, "This is * string")]
    [InlineData(LocalizedString, "*个隆冬强, *个隆冬强")]
    public void IsMatch_MultipleWildcard_ShouldNotMatched(string s, string pattern)
    {
        // Arrange

        // Act
        var isMatched = s.IsMatch(pattern);

        // Assert
        Assert.False(isMatched);
    }

    [Theory]
    [InlineData(SimpleString, "?*", true)]
    [InlineData(SimpleString, "This?* a test string!", true)]
    [InlineData(SimpleString, "Th?s is *?string*", true)]
    [InlineData(SimpleString, "Thi??is * ", false)]
    public void IsMatch_CombinedWildcards(string s, string pattern, bool expected)
    {
        // Arrange

        // Act
        var isMatched = s.IsMatch(pattern);

        // Assert
        Assert.Equal(expected, isMatched);
    }
}
