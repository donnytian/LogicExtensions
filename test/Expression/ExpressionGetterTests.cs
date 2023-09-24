using System.Linq.Expressions;
using LogicExtensions;
#pragma warning disable CS8602 // Dereference of a possibly null reference.
#pragma warning disable CS8603 // Possible null reference return.

namespace Tests.Expression;

public class ExpressionGetterTests
{
    [Fact]
    public void CreateConditionalGetter_DirectProperty_NotNull()
    {
        // Arrange
        Expression<Func<Customer, string>> selector = c => c.Name;
        var customer1 = new Customer
        {
            Name = TestData.CustomerName,
        };

        // Act
        var result = selector.CreateConditionalGetter()(customer1);

        // Assert
        Assert.Equal(TestData.CustomerName, result);
    }

    [Fact]
    public void CreateConditionalGetter_NestedProperty_NotNull()
    {
        // Arrange
        Expression<Func<Customer, int>> selector = c => c.Billing.Address.AddressId;
        var customer1 = new Customer
        {
            Billing = new CustomerBilling
            {
                Address = new BillingAddress { AddressId = TestData.MyAddressId },
            },
        };

        // Act
        var result = selector.CreateConditionalGetter()(customer1);

        // Assert
        Assert.Equal(TestData.MyAddressId, result);
    }

    [Fact]
    public void CreateConditionalGetter_NestedProperty_ReturnNull()
    {
        // Arrange
        Expression<Func<Customer, string>> selector = c => c.Billing.Contact.Name;
        var customer1 = new Customer
        {
            Billing = new CustomerBilling
            {
                Contact = new BillingContact(),
            },
        };

        // Act
        var result = selector.CreateConditionalGetter()(customer1);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public void CreateConditionalGetter_NestedProperty_AsObject_ReturnNull()
    {
        // Arrange
        Expression<Func<Customer, object>> selector = c => c.Billing.Contact.Name;
        var customer1 = new Customer
        {
            Billing = new CustomerBilling
            {
                Contact = new BillingContact(),
            },
        };

        // Act
        var result = selector.CreateConditionalGetter()(customer1);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public void CreateConditionalGetter_NestedProperty_AsObject_NotNull()
    {
        // Arrange
        Expression<Func<Customer, object>> selector = c => c.Billing.Address.NullableNumber;
        var customer1 = new Customer
        {
            Billing = new CustomerBilling
            {
                Address = new BillingAddress { NullableNumber = 1 },
            },
        };

        // Act
        var result = selector.CreateConditionalGetter()(customer1);

        // Assert
        Assert.Equal(1, result);
    }

    [Fact]
    public void CreateConditionalGetter_NestedProperty_RootIsNull_AsNullable_ReturnDefault()
    {
        // Arrange
        Expression<Func<Customer, int?>> selector = c => c.Billing.Address.AddressId;

        // Act
        var result = selector.CreateConditionalGetter()(null);

        // Assert
        Assert.Equal(default, result);
    }

    [Fact]
    public void CreateConditionalGetter_NestedProperty_PathHasNull_AsValueType_ReturnDefault()
    {
        // Arrange
        Expression<Func<Customer, int>> selector = c => c.Billing.Address.AddressId;
        var customer1 = new Customer
        {
            Billing = new CustomerBilling(),
        };

        // Act
        var result = selector.CreateConditionalGetter()(customer1);

        // Assert
        Assert.Equal(default, result);
    }

    [Fact]
    public void CreateConditionalGetter_NestedProperty_SamePropertyNamesInTheChain_ReturnCorrect()
    {
        // Arrange
        Expression<Func<Customer, string>> selector = c => c.Billing.Billing;
        var customer1 = new Customer
        {
            Billing = new CustomerBilling { Billing = TestData.DummyString },
        };

        // Act
        var result = selector.CreateConditionalGetter()(customer1);

        // Assert
        Assert.Equal(TestData.DummyString, result);
    }

    [Fact]
    public void CreateConditionalGetter_NestedPropertyOnValueType_ReturnCorrect()
    {
        // Arrange
        var now = DateTimeOffset.Now;
        Expression<Func<Customer, DateTimeOffset>> selector = c => c.Billing.Dates.Dto;
        var customer1 = new Customer
        {
            Billing = new CustomerBilling(),
        };
        customer1.Billing.Dates.Dto = now;

        // Act
        var result = selector.CreateConditionalGetter()(customer1);

        // Assert
        Assert.Equal(now, result);
    }
}
