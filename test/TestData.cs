namespace Tests;

class TestData
{
    public const string CustomerName = "Donny";
    public const int MyAddressId = 333;
    public const string DummyString = "dummyS";
}

class Customer
{
    public string? Name { get; set; }
    public int? Age { get; set; }
    public CustomerBilling? Billing { get; set; }
}

class CustomerBilling
{
    public BillingAddress? Address { get; set; }
    public BillingContact? Contact { get; set; }
    public MyStruct Dates = new();

    public string? Billing { get; set; }
}

class BillingAddress
{
    public int AddressId { get; set; }
    public int? NullableNumber { get; set; }
}

class BillingContact
{
    public string? Name { get; set; }
}

struct MyStruct
{
    public DateTimeOffset Dto { get; set; }
}
