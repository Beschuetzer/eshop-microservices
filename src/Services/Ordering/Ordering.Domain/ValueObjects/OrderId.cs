namespace Ordering.Domain.ValueObjects;

public record OrderId
{
    public Guid Value { get; }

    private OrderId(Guid value) => Value = value;

    // Factory method to create an OrderId from a Guid
    public static OrderId Of(Guid value)
    {
        ArgumentException.ThrowIfNullOrEmpty(value.ToString(), nameof(value));
        if (value == Guid.Empty)
        {
            throw new DomainException("OrderId cannot be empty.");
        }
        return new OrderId(value);
    }
}
