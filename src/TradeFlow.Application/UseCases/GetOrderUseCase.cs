using TradeFlow.Application.DTOs;
using TradeFlow.Application.Ports.Inbound;
using TradeFlow.Application.Ports.Outbound;

namespace TradeFlow.Application.UseCases;

public class GetOrderUseCase : IGetOrderUseCase
{
    private readonly IOrderRepository _orderRepository;

    public GetOrderUseCase(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<OrderDto?> ExecuteAsync(Guid orderId, CancellationToken ct = default)
    {
        var order = await _orderRepository.GetByIdAsync(orderId, ct);
        return order?.ToDto();
    }
}
