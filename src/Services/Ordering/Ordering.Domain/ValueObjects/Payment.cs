namespace Ordering.Domain.ValueObjects;

public record Payment
{
    // CardName, CardNumber, ExpirationDate, CVV, PaymentMethod
    public string? CardName { get; } = default!;
    public string CardNumber { get; } = default!;
    public DateTime ExpirationDate { get; } = default!;
    public string CVV { get; } = default!;
    public string PaymentMethod { get; } = default!;
}
