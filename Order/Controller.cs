using System.Net.Mime;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Orders.Services;

namespace Orders.Order;

[ApiController]
[Route("/orders")]
[Authorize]
[Consumes(MediaTypeNames.Application.Json)]
[Produces(MediaTypeNames.Application.Json)]
public class Controller : ControllerBase
{
    private readonly IOrderService _orderService;

    public Controller(IOrderService orderService)
    {
        _orderService = orderService;
    }

    /// <summary>Create a new order.</summary>
    /// <param name="createOrderDto">Object of order.</param>
    /// <response code="200">Created order.</response>
    [HttpPost]
    [Authorize(Roles = "User")]
    [ProducesResponseType(typeof(ReadOrderDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> CreateOrder([FromBody] CreateOrderDto createOrderDto)
    {
        var newOrder = createOrderDto.Map();
        _orderService.Create(newOrder);
        await _orderService.SaveAsync();

        var readOrderDto = newOrder.Map();
        return Ok(readOrderDto);
    }

    /// <summary>Get orders.</summary>
    /// <response code="200">Orders received.</response>
    [HttpGet]
    [Authorize(Roles = "Admin, Worker")]
    [ProducesResponseType(typeof(ReadOrderDto[]),StatusCodes.Status200OK)]
    public async Task<IActionResult> GetOrders()
    {
        var orders = await _orderService.GetAllAsync();
        var readOrderDto = orders.Select(order => order.Map());

        return Ok(readOrderDto);
    }

    /// <summary>Get order with matching id.</summary>
    /// <param name="id" example="xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx">Id of order.</param>
    /// <response code="200">Order received.</response>
    /// <response code="404">Order not found.</response>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(ReadOrderDto),StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetOrder([FromRoute] Guid id)
    {
        var order = await _orderService.GetByIdAsync(id);
        if (order is null)
            return NotFound();

        var readOrderDto = order.Map();
        return Ok(readOrderDto);
    }

    /// <summary>Update order with matching id.</summary>
    /// <param name="id" example="xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx">Order id.</param>
    /// <param name="updateOrderDto">Object order.</param>
    /// <response code="204">Updated order.</response>
    /// <response code="404">Order with specified id was not found.</response>
    [HttpPut("{id:guid}")]
    [Authorize(Roles = "Admin, User")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateOrder([FromRoute] Guid id,UpdateOrderDto updateOrderDto)
    {
        var oldOrder = await _orderService.GetByIdAsync(id);
        if (oldOrder is null)
            return NotFound();
        oldOrder = updateOrderDto.Map();

        await _orderService.SaveAsync();
        return NoContent();
    }

    /// <summary>Delete order with matching id.</summary>
    /// <param name="id" example="xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx">Order id.</param>
    /// <response code="200">Deleted order.</response>
    /// <response code="404">Order with specified id was not found.</response>
    [HttpDelete("{id:guid}")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(ReadOrderDto),StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteOrder([FromRoute] Guid id)
    {
        var deleteOrder = await _orderService.GetByIdAsync(id);
        if (deleteOrder is null)
            return NotFound();
        _orderService.Delete(deleteOrder);
        await _orderService.SaveAsync();
        var readOrderDto = deleteOrder.Map();
        return Ok(readOrderDto);
    }
}