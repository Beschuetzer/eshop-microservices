namespace Ordering.Domain.Models;

public class OrderItem(Guid orderId, Guid productId, decimal price, int quantity) : Entity<Guid>
{
    public Guid OrderId { get; private set; } = orderId;
    public Guid ProductId { get; private set; } = productId;
    public decimal Price { get; private set; } = price;
    public int Quantity { get; private set; } = quantity;
}
