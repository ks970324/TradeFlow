using TradeFlow.Application.DTOs;
using TradeFlow.Domain.Entities;

namespace TradeFlow.Application.UseCases;

internal static class OrderMapper
{
    public static OrderDto ToDto(this Order order) => new(
        order.Id,
        order.OrderNumber,
        order.CustomerId,
        order.Status,
        order.TotalAmount.Amount,
        order.TotalAmount.Currency,
        order.Items.Select(i => new OrderItemDto(
            i.Id, i.ProductId, i.ProductName, i.Quantity,
            i.UnitPrice.Amount, i.Subtotal.Amount)).ToList(),
        order.CreatedAt,
        order.UpdatedAt);
}
