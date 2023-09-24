using LogicExtensions;

namespace Tests.String;

public class ConversionTests
{
    [Theory]
    [InlineData("", 0)]
    [InlineData(null, 0)]
    [InlineData("dummy", 0)]
    [InlineData("012345", 12345)]
    [InlineData("123456", 0)]
    public void ToInt16Tests(string s, short result)
    {
        // Arrange

        // Act
        var n = s.ToInt16();

        // Assert
        Assert.Equal(result, n);
    }

    [Theory]
    [InlineData("", 0)]
    [InlineData(null, 0)]
    [InlineData("dummy", 0)]
    [InlineData("0123456", 123456)]
    [InlineData("12345678900", 0)]
    public void ToInt32Tests(string s, int result)
    {
        // Arrange

        // Act
        var n = s.ToInt32();

        // Assert
        Assert.Equal(result, n);
    }

    [Theory]
    [InlineData("", 0)]
    [InlineData(null, 0)]
    [InlineData("dummy", 0)]
    [InlineData("012345678900", 12345678900)]
    [InlineData("12345678900123456789", 0)]
    public void ToInt64Tests(string s, long result)
    {
        // Arrange

        // Act
        var n = s.ToInt64();

        // Assert
        Assert.Equal(result, n);
    }

    [Theory]
    [InlineData("", 0)]
    [InlineData(null, 0)]
    [InlineData("dummy", 0)]
    [InlineData("012345678900.1", 12345678900.1f)]
    public void ToSingleTests(string s, float result)
    {
        // Arrange

        // Act
        var n = s.ToSingle();

        // Assert
        Assert.Equal(result, n);
    }

    [Theory]
    [InlineData("", 0)]
    [InlineData(null, 0)]
    [InlineData("dummy", 0)]
    [InlineData("012345678900.1", 12345678900.1d)]
    public void ToDoubleTests(string s, double result)
    {
        // Arrange

        // Act
        var n = s.ToDouble();

        // Assert
        Assert.Equal(result, n);
    }

    [Theory]
    [InlineData("", 0)]
    [InlineData(null, 0)]
    [InlineData("dummy", 0)]
    [InlineData("012345678900.1", 12345678900.1d)]
    public void ToDecimalTests(string s, decimal result)
    {
        // Arrange

        // Act
        var n = s.ToDecimal();

        // Assert
        Assert.Equal(result, n);
    }
}
