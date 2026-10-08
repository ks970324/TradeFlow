using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TradeFlow.Application.DTOs;
using TradeFlow.Application.Ports.Inbound;

namespace TradeFlow.Adapter.Api.Controllers;

/// <summary>
/// Driving Adapter: REST API，透過 Inbound Port 與核心互動。
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly ICreateOrderUseCase _createOrder;
    private readonly IGetOrderUseCase _getOrder;

    public OrdersController(ICreateOrderUseCase createOrder, IGetOrderUseCase getOrder)
    {
        _createOrder = createOrder;
        _getOrder = getOrder;
    }

    [HttpPost]
    [ProducesResponseType(typeof(OrderDto), StatusCodes.Status201Created)]
    public async Task<IActionResult> Create([FromBody] CreateOrderRequest request, CancellationToken ct)
    {
        var order = await _createOrder.ExecuteAsync(request, ct);
        return CreatedAtAction(nameof(GetById), new { id = order.Id }, order);
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(OrderDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(Guid id, CancellationToken ct)
    {
        var order = await _getOrder.ExecuteAsync(id, ct);
        return order is null ? NotFound() : Ok(order);
    }
}
