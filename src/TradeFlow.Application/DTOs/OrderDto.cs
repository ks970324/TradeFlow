using TradeFlow.Domain.Enums;

namespace TradeFlow.Application.DTOs;

public record OrderDto(
    Guid Id,
    string OrderNumber,
    Guid CustomerId,
    OrderStatus Status,
    decimal TotalAmount,
    string Currency,
    List<OrderItemDto> Items,
    DateTime CreatedAt,
    DateTime? UpdatedAt);

public record OrderItemDto(
    Guid Id,
    Guid ProductId,
    string ProductName,
    int Quantity,
    decimal UnitPrice,
    decimal Subtotal);

public record CreateOrderRequest(
    Guid CustomerId,
    string Street,
    string City,
    string State,
    string ZipCode,
    string Country,
    List<CreateOrderItemRequest> Items);

public record CreateOrderItemRequest(
    Guid ProductId,
    string ProductName,
    int Quantity,
    decimal UnitPrice);
