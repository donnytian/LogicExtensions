using LogicExtensions;

namespace Tests.Byte;

public class MathTests
{
    [Theory]
    [InlineData(null, null, typeof(ArgumentNullException))]
    [InlineData(new byte[] { 1, 2, 3 }, new byte[] { 254, 253, 252 }, null)]
    public void NotTests(byte[] bytes, byte[] expected, Type? exceptionType)
    {
        // Act & Assert
        if (exceptionType == null)
        {
            Assert.True(expected.Zip(bytes.Not(), (a, b) => a == b).All(p => p));
        }
        else
        {
            Assert.Throws(exceptionType, bytes.Not);
        }
    }

    [Theory]
    [InlineData(null, null, null, typeof(ArgumentNullException))]
    [InlineData(new byte[] { 1, 2, 3 }, null, null, typeof(ArgumentNullException))]
    [InlineData(new byte[] { 1, 2, 5 }, new byte[] { 1, 3, 10 }, new byte[] { 1, 2, 0 }, null)]
    public void AndTests(byte[] bytes, byte[] another, byte[] expected, Type? exceptionType)
    {
        // Act & Assert
        if (exceptionType == null)
        {
            Assert.True(expected.Zip(bytes.And(another), (a, b) => a == b).All(p => p));
        }
        else
        {
            Assert.Throws(exceptionType, () => bytes.And(another));
        }
    }

    [Theory]
    [InlineData(null, null, null, typeof(ArgumentNullException))]
    [InlineData(new byte[] { 1, 2, 3 }, null, null, typeof(ArgumentNullException))]
    [InlineData(new byte[] { 1, 2, 5 }, new byte[] { 1, 3, 10 }, new byte[] { 1, 3, 15 }, null)]
    public void OrTests(byte[] bytes, byte[] another, byte[] expected, Type? exceptionType)
    {
        // Act & Assert
        if (exceptionType == null)
        {
            Assert.True(expected.Zip(bytes.Or(another), (a, b) => a == b).All(p => p));
        }
        else
        {
            Assert.Throws(exceptionType, () => bytes.Or(another));
        }
    }

    [Theory]
    [InlineData(null, null, false, typeof(ArgumentNullException))]
    [InlineData(new byte[] { 1, 2, 3 }, null, false, typeof(ArgumentNullException))]
    [InlineData(new byte[] { 1, 2, 5 }, new byte[] { 1, 3, 10 }, false, null)]
    [InlineData(new byte[] { 1, 2, 5 }, new byte[] { 1, 2, 3 }, true, null)]
    [InlineData(new byte[] { 1, 2, 5 }, new byte[] { 1, 2, 5 }, false, null)]
    [InlineData(new byte[] { 1, 2, 5 }, new byte[] { 2, 5 }, true, null)]
    [InlineData(new byte[] { 1, 2, 5 }, new byte[] { 1, 2 }, true, null)]
    public void GreaterThanTests(byte[] bytes, byte[] another, bool expected, Type? exceptionType)
    {
        // Act & Assert
        if (exceptionType == null)
        {
            Assert.Equal(expected, bytes.GreaterThan(another));
        }
        else
        {
            Assert.Throws(exceptionType, () => bytes.GreaterThan(another));
        }
    }

    [Theory]
    [InlineData(null, null, false, typeof(ArgumentNullException))]
    [InlineData(new byte[] { 1, 2, 3 }, null, false, typeof(ArgumentNullException))]
    [InlineData(new byte[] { 1, 2, 5 }, new byte[] { 1, 3, 10 }, false, null)]
    [InlineData(new byte[] { 1, 2, 5 }, new byte[] { 1, 2, 3 }, true, null)]
    [InlineData(new byte[] { 1, 2, 5 }, new byte[] { 1, 2, 5 }, true, null)]
    public void GreaterOrEqualTests(byte[] bytes, byte[] another, bool expected, Type? exceptionType)
    {
        // Act & Assert
        if (exceptionType == null)
        {
            Assert.Equal(expected, bytes.GreaterOrEqual(another));
        }
        else
        {
            Assert.Throws(exceptionType, () => bytes.GreaterOrEqual(another));
        }
    }

    [Theory]
    [InlineData(null, null, false, typeof(ArgumentNullException))]
    [InlineData(new byte[] { 1, 2, 3 }, null, false, typeof(ArgumentNullException))]
    [InlineData(new byte[] { 1, 2, 5 }, new byte[] { 1, 3, 10 }, true, null)]
    [InlineData(new byte[] { 1, 2, 5 }, new byte[] { 1, 2, 3 }, false, null)]
    [InlineData(new byte[] { 1, 2, 5 }, new byte[] { 1, 2, 5 }, false, null)]
    [InlineData(new byte[] { 1, 2, 5 }, new byte[] { 2, 5 }, false, null)]
    [InlineData(new byte[] { 1, 2, 5 }, new byte[] { 1, 2 }, false, null)]
    public void LessThanTests(byte[] bytes, byte[] another, bool expected, Type? exceptionType)
    {
        // Act & Assert
        if (exceptionType == null)
        {
            Assert.Equal(expected, bytes.LessThan(another));
        }
        else
        {
            Assert.Throws(exceptionType, () => bytes.LessThan(another));
        }
    }

    [Theory]
    [InlineData(null, null, false, typeof(ArgumentNullException))]
    [InlineData(new byte[] { 1, 2, 3 }, null, false, typeof(ArgumentNullException))]
    [InlineData(new byte[] { 1, 2, 5 }, new byte[] { 1, 3, 10 }, true, null)]
    [InlineData(new byte[] { 1, 2, 5 }, new byte[] { 1, 2, 3 }, false, null)]
    [InlineData(new byte[] { 1, 2, 5 }, new byte[] { 1, 2, 5 }, true, null)]
    public void LessOrEqualTests(byte[] bytes, byte[] another, bool expected, Type? exceptionType)
    {
        // Act & Assert
        if (exceptionType == null)
        {
            Assert.Equal(expected, bytes.LessOrEqual(another));
        }
        else
        {
            Assert.Throws(exceptionType, () => bytes.LessOrEqual(another));
        }
    }

    [Theory]
    [InlineData(null, null, false, 0, typeof(ArgumentNullException))]
    [InlineData(new byte[] { 1, 2, 3 }, null, false, 0, typeof(ArgumentNullException))]
    [InlineData(new byte[] { 1, 2, 5 }, new byte[] { 1, 3, 4 }, true, -1, null)]
    [InlineData(new byte[] { 1, 2, 5 }, new byte[] { 1, 3, 4 }, false, 1, null)]
    [InlineData(new byte[] { 1, 2 }, new byte[] { 1, 1, 4 }, true, -1, null)]
    [InlineData(new byte[] { 1, 2, 5 }, new byte[] { 1, 2, 5 }, null, 0, null)]
    public void CompareToTests(byte[] bytes, byte[] another, bool? bigEndian, int expected, Type? exceptionType)
    {
        // Act & Assert
        if (exceptionType == null)
        {
            Assert.Equal(expected, bytes.CompareTo(another, bigEndian));
        }
        else
        {
            Assert.Throws(exceptionType, () => bytes.CompareTo(another, bigEndian));
        }
    }

    [Theory]
    [InlineData(null, null, false, typeof(ArgumentNullException))]
    [InlineData(new byte[] { 1, 2, 3 }, null, false, typeof(ArgumentNullException))]
    [InlineData(new byte[] { 1, 2, 5 }, new byte[] { 1, 3, 10 }, false, null)]
    [InlineData(new byte[] { 1, 2, 5 }, new byte[] { 1, 2, 3 }, false, null)]
    [InlineData(new byte[] { 1, 2, 5 }, new byte[] { 1, 2, 5 }, true, null)]
    public void EqualsToTests(byte[] bytes, byte[] another, bool expected, Type? exceptionType)
    {
        // Act & Assert
        if (exceptionType == null)
        {
            Assert.Equal(expected, bytes.EqualsTo(another));
        }
        else
        {
            Assert.Throws(exceptionType, () => bytes.EqualsTo(another));
        }
    }
}
