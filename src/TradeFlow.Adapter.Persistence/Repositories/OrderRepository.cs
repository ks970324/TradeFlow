using Microsoft.EntityFrameworkCore;
using TradeFlow.Application.Ports.Outbound;
using TradeFlow.Domain.Entities;

namespace TradeFlow.Adapter.Persistence.Repositories;

/// <summary>
/// Driven Adapter: 實作 Outbound Port IOrderRepository，用 EF Core 存取 SQL Server。
/// </summary>
public class OrderRepository : IOrderRepository
{
    private readonly TradeFlowDbContext _context;

    public OrderRepository(TradeFlowDbContext context)
    {
        _context = context;
    }

    public async Task<Order?> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        return await _context.Orders
            .Include(o => o.Items)
            .FirstOrDefaultAsync(o => o.Id == id, ct);
    }

    public async Task<Order?> GetByOrderNumberAsync(string orderNumber, CancellationToken ct = default)
    {
        return await _context.Orders
            .Include(o => o.Items)
            .FirstOrDefaultAsync(o => o.OrderNumber == orderNumber, ct);
    }

    public async Task<List<Order>> GetByCustomerIdAsync(Guid customerId, CancellationToken ct = default)
    {
        return await _context.Orders
            .Include(o => o.Items)
            .Where(o => o.CustomerId == customerId)
            .OrderByDescending(o => o.CreatedAt)
            .ToListAsync(ct);
    }

    public async Task AddAsync(Order order, CancellationToken ct = default)
    {
        await _context.Orders.AddAsync(order, ct);
    }

    public async Task SaveChangesAsync(CancellationToken ct = default)
    {
        await _context.SaveChangesAsync(ct);
    }
}
