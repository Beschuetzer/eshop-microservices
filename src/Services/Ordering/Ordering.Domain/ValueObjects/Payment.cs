namespace Ordering.Domain.ValueObjects;

public record Payment
{
    private const int CvvLength = 3;
    // needed for EF Core
    protected Payment() { }

    // Private constructor to enforce the use of the factory method
    private Payment(string cardName, string cardNumber, DateTime expiration, string cvv, string paymentMethod)
    {
        CardName = cardName;
        CardNumber = cardNumber;
        Expiration = expiration;
        CVV = cvv;
        PaymentMethod = paymentMethod;
    }


    // CardName, CardNumber, ExpirationDate, CVV, PaymentMethod
    public string CardName { get; } = default!;
    public string CardNumber { get; } = default!;
    public DateTime Expiration { get; } = default!;
    public string CVV { get; } = default!;
    public string PaymentMethod { get; } = default!;

    // Factory method to create a Payment instance
    public static Payment Of(
        string cardName,
        string cardNumber,
        DateTime expiration,
        string cvv,
        string paymentMethod)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(cardName, nameof(cardName));
        ArgumentException.ThrowIfNullOrWhiteSpace(cardNumber, nameof(cardNumber));
        ArgumentException.ThrowIfNullOrWhiteSpace(cvv, nameof(cvv));
        ArgumentOutOfRangeException.ThrowIfGreaterThan(cvv.Length, CvvLength);
        return new Payment(cardName, cardNumber, expiration, cvv, paymentMethod);
    }
}
