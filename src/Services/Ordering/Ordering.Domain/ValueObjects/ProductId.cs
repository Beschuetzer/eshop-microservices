namespace Ordering.Domain.ValueObjects;

public record ProductId
{
    public Guid Value { get; }

    private ProductId(Guid value) => Value = value;

    // Factory method to create a ProductId from a Guid
    public static ProductId Of(Guid value)
    {
        ArgumentException.ThrowIfNullOrEmpty(value.ToString(), nameof(value));
        if (value == Guid.Empty)
        {
            throw new DomainException("ProductId cannot be empty.");
        }
        return new ProductId(value);
    }
}
