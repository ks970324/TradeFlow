using TradeFlow.Domain.Enums;
using TradeFlow.Domain.ValueObjects;

namespace TradeFlow.Domain.Entities;

public class Order
{
    private readonly List<OrderItem> _items = [];

    public Guid Id { get; private set; }
    public string OrderNumber { get; private set; } = default!;
    public Guid CustomerId { get; private set; }
    public OrderStatus Status { get; private set; }
    public Address ShippingAddress { get; private set; } = default!;
    public IReadOnlyList<OrderItem> Items => _items.AsReadOnly();
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }

    private Order() { } // EF Core

    public static Order Create(Guid customerId, Address shippingAddress)
    {
        return new Order
        {
            Id = Guid.NewGuid(),
            OrderNumber = $"ORD-{DateTime.UtcNow:yyyyMMdd}-{Guid.NewGuid().ToString()[..8].ToUpper()}",
            CustomerId = customerId,
            Status = OrderStatus.Pending,
            ShippingAddress = shippingAddress,
            CreatedAt = DateTime.UtcNow
        };
    }

    public void AddItem(Guid productId, string productName, int quantity, Money unitPrice)
    {
        if (Status != OrderStatus.Pending)
            throw new InvalidOperationException("Can only add items to pending orders.");

        var item = new OrderItem(productId, productName, quantity, unitPrice);
        _items.Add(item);
        UpdatedAt = DateTime.UtcNow;
    }

    public Money TotalAmount
    {
        get
        {
            if (_items.Count == 0)
                return new Money(0, "TWD");

            return _items
                .Select(i => i.Subtotal)
                .Aggregate((a, b) => a.Add(b));
        }
    }

    public void Confirm()
    {
        EnsureTransition(OrderStatus.Pending, OrderStatus.Confirmed);
    }

    public void StartProcessing()
    {
        EnsureTransition(OrderStatus.Confirmed, OrderStatus.Processing);
    }

    public void Ship()
    {
        EnsureTransition(OrderStatus.Processing, OrderStatus.Shipped);
    }

    public void Deliver()
    {
        EnsureTransition(OrderStatus.Shipped, OrderStatus.Delivered);
    }

    public void Cancel()
    {
        if (Status is OrderStatus.Shipped or OrderStatus.Delivered)
            throw new InvalidOperationException($"Cannot cancel an order that is already {Status}.");

        Status = OrderStatus.Cancelled;
        UpdatedAt = DateTime.UtcNow;
    }

    private void EnsureTransition(OrderStatus expectedCurrent, OrderStatus next)
    {
        if (Status != expectedCurrent)
            throw new InvalidOperationException(
                $"Cannot transition from {Status} to {next}. Expected current status: {expectedCurrent}.");

        Status = next;
        UpdatedAt = DateTime.UtcNow;
    }
}
