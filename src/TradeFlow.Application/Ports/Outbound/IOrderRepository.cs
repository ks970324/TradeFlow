using TradeFlow.Domain.Entities;

namespace TradeFlow.Application.Ports.Outbound;

/// <summary>
/// Outbound Port: 定義核心需要的資料存取能力，由 Driven Adapter 實作。
/// </summary>
public interface IOrderRepository
{
    Task<Order?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<Order?> GetByOrderNumberAsync(string orderNumber, CancellationToken ct = default);
    Task<List<Order>> GetByCustomerIdAsync(Guid customerId, CancellationToken ct = default);
    Task AddAsync(Order order, CancellationToken ct = default);
    Task SaveChangesAsync(CancellationToken ct = default);
}
