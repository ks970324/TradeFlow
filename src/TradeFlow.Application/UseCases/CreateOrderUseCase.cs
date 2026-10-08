using TradeFlow.Application.DTOs;
using TradeFlow.Application.Ports.Inbound;
using TradeFlow.Application.Ports.Outbound;
using TradeFlow.Domain.Entities;
using TradeFlow.Domain.ValueObjects;

namespace TradeFlow.Application.UseCases;

public class CreateOrderUseCase : ICreateOrderUseCase
{
    private readonly IOrderRepository _orderRepository;

    public CreateOrderUseCase(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<OrderDto> ExecuteAsync(CreateOrderRequest request, CancellationToken ct = default)
    {
        var address = new Address(request.Street, request.City, request.State, request.ZipCode, request.Country);
        var order = Order.Create(request.CustomerId, address);

        foreach (var item in request.Items)
        {
            order.AddItem(item.ProductId, item.ProductName, item.Quantity, new Money(item.UnitPrice, "TWD"));
        }

        await _orderRepository.AddAsync(order, ct);
        await _orderRepository.SaveChangesAsync(ct);

        return order.ToDto();
    }
}
