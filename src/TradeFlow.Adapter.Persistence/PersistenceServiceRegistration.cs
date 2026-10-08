using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TradeFlow.Adapter.Persistence.Repositories;
using TradeFlow.Application.Ports.Outbound;

namespace TradeFlow.Adapter.Persistence;

public static class PersistenceServiceRegistration
{
    public static IServiceCollection AddPersistenceAdapter(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<TradeFlowDbContext>(options =>
            options.UseSqlServer(
                configuration.GetConnectionString("DefaultConnection"),
                b => b.MigrationsAssembly(typeof(TradeFlowDbContext).Assembly.FullName)));

        services.AddScoped<IOrderRepository, OrderRepository>();

        return services;
    }
}
