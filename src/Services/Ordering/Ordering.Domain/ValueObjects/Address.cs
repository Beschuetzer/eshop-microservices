namespace Ordering.Domain.ValueObjects;

// Record types are great for comparing values and are immutable by default.
public record Address
{
    // needed for EF Core
    protected Address() { }
    private Address(string firstName, string lastName, string emailAddress, string addressLine, string country, string city, string state, string zipCode)
    {
        FirstName = firstName;
        LastName = lastName;
        EmailAddress = emailAddress;
        AddressLine = addressLine;
        Country = country;
        City = city;
        State = state;
        ZipCode = zipCode;
    }

    // firstame, last name, email, phone number, street, city, state, zip code
    public string FirstName { get; init; } = default!;
    public string LastName { get; init; } = default!;
    //email 
    public string EmailAddress { get; init; } = default!;
    public string AddressLine { get; init; } = default!;
    public string Country { get; init; } = default!;
    public string City { get; init; } = default!;
    public string State { get; init; } = default!;
    public string ZipCode { get; init; } = default!;

    // Factory method to create an Address instance
    public static Address Of(
        string firstName,
        string lastName,
        string emailAddress,
        string addressLine,
        string country,
        string city,
        string state,
        string zipCode)
    {
        ArgumentException.ThrowIfNullOrEmpty(emailAddress, nameof(firstName));
        ArgumentException.ThrowIfNullOrEmpty(addressLine, nameof(lastName));
        return new Address(firstName, lastName, emailAddress, addressLine, country, city, state, zipCode);
    }
}
