namespace Ordering.Domain.Models;

public class Order : Aggregate<OrderId>
{
    private readonly List<OrderItem> _orderItems = [];
    public IReadOnlyList<OrderItem> OrderItems => _orderItems.AsReadOnly();
    public CustomerId CustomerId { get; private set; } = default!;
    public OrderName OrderName { get; private set; } = default!;
    public Address BillingAddress { get; private set; } = default!;
    public Address ShippingAddress { get; private set; } = default!;
    public Payment Payment { get; private set; } = default!;
    public OrderStatus Status { get; private set; } = OrderStatus.Pending;

    // create a TotalPrice property that calculates the total price of the order.  It should only be privately settable.
    public decimal TotalPrice => _orderItems.Sum(item => item.Price * item.Quantity);


    // Factory method to create an Order instance
    public static Order Create(
        OrderId id,
        CustomerId customerId,
        OrderName orderName,
        Address billingAddress,
        Address shippingAddress,
        Payment payment)
    {
        // since each ValueObject already has its own validation logic, we can just call the constructor.
        var order = new Order
        {
            Id = id,
            CustomerId = customerId,
            OrderName = orderName,
            BillingAddress = billingAddress,
            ShippingAddress = shippingAddress,
            Payment = payment,
            Status = OrderStatus.Pending
        };

        order.AddDomainEvent(new OrderCreatedEvent(order));

        return order;
    }
    public void Update(OrderName orderName, Address shippingAddress, Address billingAddress, Payment payment, OrderStatus status)
    {
        OrderName = orderName;
        ShippingAddress = shippingAddress;
        BillingAddress = billingAddress;
        Payment = payment;
        Status = status;

        AddDomainEvent(new OrderUpdatedEvent(this));
    }

    public void Add(ProductId productId, int quantity, decimal price)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(quantity, nameof(quantity));
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(price, nameof(price));
        var orderItem = new OrderItem(Id, productId, quantity, price);

        _orderItems.Add(orderItem);
    }

    public void Remove(OrderItemId orderItemId)
    {
        var orderItem = _orderItems.FirstOrDefault(item => item.Id == orderItemId);
        if (orderItem is not null)
        {
            _orderItems.Remove(orderItem);
        }
    }
}
