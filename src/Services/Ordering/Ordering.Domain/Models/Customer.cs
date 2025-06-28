namespace Ordering.Domain.Models;

public class Customer : Entity<CustomerId>
{
    public string Name { get; private set; } = default!;
    public string Email { get; private set; } = default!;

    public static Customer Create(string name, string email)
    {
        ArgumentException.ThrowIfNullOrEmpty(name, nameof(name));
        ArgumentException.ThrowIfNullOrEmpty(email, nameof(email));
        var customerId = CustomerId.Of(Guid.NewGuid());
        return new Customer
        {
            Id = customerId,
            Name = name,
            Email = email
        };
    }
}
