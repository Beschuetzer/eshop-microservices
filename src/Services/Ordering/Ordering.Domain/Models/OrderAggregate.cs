namespace Ordering.Domain.Models;

internal class OrderAggregate : Aggregate<Guid>
{
    private readonly List<OrderItem> _orderItems = [];
    public IReadOnlyList<OrderItem> OrderItems => _orderItems.AsReadOnly();
    public Guid CustomerId { get; private set; } = default!;
    public string OrderName { get; private set; } = default!;
    public Address BillingAddress { get; private set; } = default!;
    public Address ShippingAddress { get; private set; } = default!;
    public Payment Payment { get; private set; } = default!;
    public OrderStatus Status { get; private set; } = OrderStatus.Pending;

    // create a TotalPrice property that calculates the total price of the order.  It should only be privately settable.
    public decimal TotalPrice => _orderItems.Sum(item => item.Price * item.Quantity);


}
