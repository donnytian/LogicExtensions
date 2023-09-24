using System.Linq.Expressions;
using LogicExtensions;
#pragma warning disable CS8602 // Dereference of a possibly null reference.
#pragma warning disable CS8603 // Possible null reference return.

namespace Tests.Expression;

public class ExpressionSetterTests
{
    [Fact]
    public void CreateConditionalSetter_DirectProperty_NotNull()
    {
        // Arrange
        Expression<Func<Customer, string>> selector = c => c.Name;
        var customer1 = new Customer();

        // Act
        selector.CreateConditionalSetter()(customer1, TestData.CustomerName);

        // Assert
        Assert.Equal(TestData.CustomerName, customer1.Name);
    }

    [Fact]
    public void CreateConditionalSetter_NestedProperty_NotNull()
    {
        // Arrange
        Expression<Func<Customer, int>> selector = c => c.Billing.Address.AddressId;
        var customer1 = new Customer();

        // Act
        selector.CreateConditionalSetter()(customer1, TestData.MyAddressId);

        // Assert
        Assert.Equal(TestData.MyAddressId, customer1.Billing.Address.AddressId);
    }

    [Fact]
    public void CreateConditionalSetter_NestedProperty_SetNull()
    {
        // Arrange
        Expression<Func<Customer, string>> selector = c => c.Billing.Contact.Name;
        var customer1 = new Customer
        {
            Billing = new CustomerBilling
            {
                Contact = new BillingContact { Name = TestData.DummyString },
            },
        };

        // Act
        selector.CreateConditionalSetter()(customer1, null);

        // Assert
        Assert.Null(customer1.Billing.Contact.Name);
    }

    [Fact]
    public void CreateConditionalSetter_NestedProperty_SamePropertyNameInTheChain()
    {
        // Arrange
        Expression<Func<Customer, string>> selector = c => c.Billing.Billing;
        var customer1 = new Customer();

        // Act
        selector.CreateConditionalSetter()(customer1, TestData.DummyString);

        // Assert
        Assert.Equal(TestData.DummyString, customer1.Billing.Billing);
    }

    [Fact]
    public void CreateConditionalSetter_NestedProperty_AsObject_SetNullableToNull()
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
        selector.CreateConditionalSetter()(customer1, null);

        // Assert
        Assert.Null(customer1.Billing.Address.NullableNumber);
    }

    [Fact]
    public void CreateConditionalSetter_NestedProperty_RootIsNull_Throws()
    {
        // Arrange
        Expression<Func<Customer, int?>> selector = c => c.Billing.Address.AddressId;

        // Act
        var act = selector.CreateConditionalSetter();

        // Assert
        Assert.Throws<NullReferenceException>(() => act(null, 11));
    }

    [Fact]
    public void CreateConditionalSetter_NestedProperty_PathHasNull()
    {
        // Arrange
        Expression<Func<Customer, int>> selector = c => c.Billing.Address.AddressId;
        var billing = new CustomerBilling();
        var customer1 = new Customer
        {
            Billing = billing,
        };

        // Act
        selector.CreateConditionalSetter()(customer1, TestData.MyAddressId);

        // Assert
        Assert.Same(billing, customer1.Billing);
        Assert.NotNull(customer1.Billing.Address);
        Assert.Equal(TestData.MyAddressId, customer1.Billing.Address.AddressId);
    }

    [Fact]
    public void CreateConditionalSetter_NestedProperty_PathHasNull_NoObjCreation()
    {
        // Arrange
        Expression<Func<Customer, int>> selector = c => c.Billing.Address.AddressId;
        var billing = new CustomerBilling();
        var customer1 = new Customer
        {
            Billing = billing,
        };

        // Act
        selector.CreateConditionalSetter(false)(customer1, TestData.MyAddressId);

        // Assert
        Assert.Same(billing, customer1.Billing);
        Assert.Null(customer1.Billing.Address);
    }

    [Fact]
    public void CreateConditionalSetter_NestedPropertyOnValueType()
    {
        // Arrange
        var now = DateTimeOffset.Now;
        Expression<Func<Customer, DateTimeOffset>> selector = c => c.Billing.Dates.Dto;
        var customer1 = new Customer
        {
            Billing = new CustomerBilling(),
        };

        // Act
        selector.CreateConditionalSetter()(customer1, now);

        // Assert
        Assert.Equal(now, customer1.Billing.Dates.Dto);
    }

    [Fact]
    public void CreateConditionalSetter_NestedPropertyOnValueType_NoObjCreation()
    {
        // Arrange
        var now = DateTimeOffset.Now;
        Expression<Func<Customer, DateTimeOffset>> selector = c => c.Billing.Dates.Dto;
        var customer1 = new Customer
        {
            Billing = new CustomerBilling(),
        };

        // Act
        selector.CreateConditionalSetter(false)(customer1, now);

        // Assert
        Assert.Equal(now, customer1.Billing.Dates.Dto);
    }
}
