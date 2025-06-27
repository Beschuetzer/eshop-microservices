namespace Ordering.Domain.ValueObjects;

// Record types are great for comparing values and are immutable by default.
public record Address
{
    // firstame, last name, email, phone number, street, city, state, zip code
    public string FirstName { get; init; } = default!;
    public string LastName { get; init; } = default!;
    //email 
    public string? EmailAddress { get; init; } = default!;
    public string AddressLine { get; init; } = default!;
    public string Country { get; init; } = default!;
    public string City { get; init; } = default!;
    public string State { get; init; } = default!;
    public string ZipCode { get; init; } = default!;
}
