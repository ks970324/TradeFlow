using Microsoft.EntityFrameworkCore;
using TradeFlow.Domain.Entities;

namespace TradeFlow.Adapter.Persistence;

public class TradeFlowDbContext : DbContext
{
    public TradeFlowDbContext(DbContextOptions<TradeFlowDbContext> options) : base(options) { }

    public DbSet<Order> Orders => Set<Order>();
    public DbSet<OrderItem> OrderItems => Set<OrderItem>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(TradeFlowDbContext).Assembly);
    }
}
