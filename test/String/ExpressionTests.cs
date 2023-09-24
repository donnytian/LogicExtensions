using System.Linq.Expressions;
using LogicExtensions;
#pragma warning disable CS8602 // Dereference of a possibly null reference.
#pragma warning disable CS8603 // Possible null reference return.

namespace Tests.String;

public class StringExpressionTests
{
    #region Getter

    [Fact]
    public void CreateConditionalGetter_DirectProperty_NotNull()
    {
        // Arrange
        var path = "Name";
        var customer1 = new Customer
        {
            Name = TestData.CustomerName,
        };

        // Act
        var result = path.CreateConditionalGetter<Customer, string>()(customer1);

        // Assert
        Assert.Equal(TestData.CustomerName, result);
    }

    [Fact]
    public void CreateConditionalGetter_NestedProperty_NotNull()
    {
        // Arrange
        var path = "Billing.Address.AddressId";
        var customer1 = new Customer
        {
            Billing = new CustomerBilling
            {
                Address = new BillingAddress { AddressId = TestData.MyAddressId },
            },
        };

        // Act
        var result = path.CreateConditionalGetter<Customer, int>()(customer1);

        // Assert
        Assert.Equal(TestData.MyAddressId, result);
    }

    [Fact]
    public void CreateConditionalGetter_NestedProperty_StartsWithHostName()
    {
        // Arrange
        var path = "Customer.Billing.Address.AddressId";
        var customer1 = new Customer
        {
            Billing = new CustomerBilling
            {
                Address = new BillingAddress { AddressId = TestData.MyAddressId },
            },
        };

        // Act
        var result = path.CreateConditionalGetter<Customer, object>(pathStartsWithHostType: true)(customer1);

        // Assert
        Assert.Equal(TestData.MyAddressId, result);
    }

    [Fact]
    public void CreateConditionalGetter_NestedProperty_StartsWithHostName_WrongParameter_Throws()
    {
        // Arrange
        var path = "Customer.Billing.Address.AddressId";

        // Act & Assert
        Assert.Throws<ArgumentException>(() =>  path.CreateConditionalGetter<Customer, object>(pathStartsWithHostType: false));
    }

    [Fact]
    public void CreateConditionalGetter_WrongPath_Throws()
    {
        // Arrange
        var path = "Billing.Wrong.Path";

        // Act & Assert
        Assert.Throws<ArgumentException>(() =>  path.CreateConditionalGetter<Customer, object>());
    }

    [Fact]
    public void CreateConditionalGetter_NestedProperty_HostIsDynamic_WithoutHostInstance_Throws()
    {
        // Arrange
        var path = "Billing.Address.AddressId";

        // act & Assert
        Assert.Throws<ArgumentException>(() => path.CreateConditionalGetter<dynamic, object>());
    }

    [Fact]
    public void CreateConditionalGetter_NestedProperty_HostIsDynamic_WithHostInstance()
    {
        // Arrange
        var path = "Billing.Address.AddressId";
        var customer1 = new Customer
        {
            Billing = new CustomerBilling
            {
                Address = new BillingAddress { AddressId = TestData.MyAddressId },
            },
        };

        // Act
        var result = path.CreateConditionalGetter<dynamic, object>(customer1)(customer1);

        // Assert
        Assert.Equal(TestData.MyAddressId, result);
    }

    #endregion

    #region Setter

    [Fact]
    public void CreateConditionalSetter_DirectProperty_NotNull()
    {
        // Arrange
        var path = "Name";
        var customer1 = new Customer();

        // Act
        path.CreateConditionalSetter<Customer, string>()(customer1, TestData.CustomerName);

        // Assert
        Assert.Equal(TestData.CustomerName, customer1.Name);
    }

    [Fact]
    public void CreateConditionalSetter_NestedProperty_NotNull()
    {
        // Arrange
        var path = "Billing.Address.AddressId";
        var customer1 = new Customer();

        // Act
        path.CreateConditionalSetter<Customer, int>()(customer1, TestData.MyAddressId);

        // Assert
        Assert.Equal(TestData.MyAddressId, customer1.Billing.Address.AddressId);
    }

    [Fact]
    public void CreateConditionalSetter_NestedProperty_StartsWithHostName()
    {
        // Arrange
        var path = "Customer.Billing.Address.AddressId";
        var customer1 = new Customer
        {
            Billing = new CustomerBilling
            {
                Address = new BillingAddress { AddressId = -1 },
            },
        };

        // Act
        path.CreateConditionalSetter<Customer, object>(pathStartsWithHostType: true)(customer1, TestData.MyAddressId);

        // Assert
        Assert.Equal(TestData.MyAddressId, customer1.Billing.Address.AddressId);
    }

    [Fact]
    public void CreateConditionalSetter_NestedProperty_StartsWithHostName_WrongParameter_Throws()
    {
        // Arrange
        var path = "Customer.Billing.Address.AddressId";

        // Act & Assert
        Assert.Throws<ArgumentException>(() =>  path.CreateConditionalSetter<Customer, object>(pathStartsWithHostType: false));
    }

    [Fact]
    public void CreateConditionalSetter_WrongPath_Throws()
    {
        // Arrange
        var path = "Billing.Wrong.Path";

        // Act & Assert
        Assert.Throws<ArgumentException>(() =>  path.CreateConditionalSetter<Customer, object>());
    }

    [Fact]
    public void CreateConditionalSetter_NestedProperty_HostIsDynamic_WithoutHostInstance_Throws()
    {
        // Arrange
        var path = "Billing.Address.AddressId";

        // act & Assert
        Assert.Throws<ArgumentException>(() => path.CreateConditionalSetter<dynamic, object>());
    }

    [Fact]
    public void CreateConditionalSetter_NestedProperty_HostIsDynamic_WithHostInstance()
    {
        // Arrange
        var path = "Billing.Address.AddressId";
        var customer1 = new Customer
        {
            Billing = new CustomerBilling(),
        };

        // Act
        path.CreateConditionalSetter<dynamic, object>(customer1)(customer1, TestData.MyAddressId);

        // Assert
        Assert.NotNull(customer1.Billing.Address);
        Assert.Equal(TestData.MyAddressId, customer1.Billing.Address.AddressId);
    }

    [Fact]
    public void CreateConditionalSetter_NestedProperty_NoObjCreation()
    {
        // Arrange
        var path = "Billing.Address.AddressId";
        var customer1 = new Customer
        {
            Billing = new CustomerBilling(),
        };

        // Act
        path.CreateConditionalSetter<Customer, int>(customer1, newObjectInPath: false)(customer1, TestData.MyAddressId);

        // Assert
        Assert.Null(customer1.Billing.Address);
    }

    #endregion
}
