namespace Ordering.Domain.Models;

public class OrderItem : Entity<OrderItemId>
{
    // All responsibilities of OrderItem lie on the Order aggregate root, so this class is internal.  
    internal OrderItem(OrderId orderId, ProductId productId, int quantity, decimal price)
    {
        Id = OrderItemId.Of(Guid.NewGuid());
        OrderId = orderId;
        ProductId = productId;
        Price = price;
        Quantity = quantity;
    }
    public OrderId OrderId { get; private set; }
    public ProductId ProductId { get; private set; }
    public decimal Price { get; private set; }
    public int Quantity { get; private set; }
}
