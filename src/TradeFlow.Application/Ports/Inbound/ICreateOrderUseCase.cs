using TradeFlow.Application.DTOs;

namespace TradeFlow.Application.Ports.Inbound;

/// <summary>
/// Inbound Port: 建立訂單。由 Driving Adapter (API Controller) 呼叫。
/// </summary>
public interface ICreateOrderUseCase
{
    Task<OrderDto> ExecuteAsync(CreateOrderRequest request, CancellationToken ct = default);
}
