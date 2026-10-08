using TradeFlow.Application.DTOs;

namespace TradeFlow.Application.Ports.Inbound;

/// <summary>
/// Inbound Port: 查詢訂單。由 Driving Adapter (API Controller) 呼叫。
/// </summary>
public interface IGetOrderUseCase
{
    Task<OrderDto?> ExecuteAsync(Guid orderId, CancellationToken ct = default);
}
