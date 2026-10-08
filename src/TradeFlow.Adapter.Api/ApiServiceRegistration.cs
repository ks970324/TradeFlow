using Microsoft.Extensions.DependencyInjection;
using TradeFlow.Application.Ports.Inbound;
using TradeFlow.Application.UseCases;

namespace TradeFlow.Adapter.Api;

public static class ApiServiceRegistration
{
    public static IServiceCollection AddApiAdapter(this IServiceCollection services)
    {
        // 註冊 Inbound Port 的實作（Use Cases）
        services.AddScoped<ICreateOrderUseCase, CreateOrderUseCase>();
        services.AddScoped<IGetOrderUseCase, GetOrderUseCase>();

        return services;
    }
}
