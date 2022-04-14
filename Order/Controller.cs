using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Orders.Order;

[ApiController]
[Route("/orders")]
[Consumes(MediaTypeNames.Application.Json)]
[Produces(MediaTypeNames.Application.Json)]
public class Controller : ControllerBase
{
    private readonly DatabaseContext _context;

    public Controller(DatabaseContext context)
    {
        _context = context;
    }

    /// <summary>Create a new order.</summary>
    /// <param name="createOrderDto">Object of order.</param>
    /// <response code="200">Created order.</response>
    [HttpPost]
    [ProducesResponseType(typeof(ReadOrderDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> CreateOrder([FromBody] CreateOrderDto createOrderDto)
    {
        var newOrder = new OrderEntity
        {
            Id = Guid.NewGuid(),
            AdressReceiver = createOrderDto.AdressReceiver,
            AdressSender = createOrderDto.AdressSender,
            CityReceiver = createOrderDto.CityReceiver,
            CitySender = createOrderDto.CitySender,
            Weight = createOrderDto.Weight,
            DateReceiving = DateTime.Now
        };
        _context.Orders.Add(newOrder);
        await _context.SaveChangesAsync();

        var readOrderDto = new ReadOrderDto
        {
            Id = newOrder.Id,
            AdressReceiver = newOrder.AdressReceiver,
            AdressSender = newOrder.AdressSender,
            CityReceiver = newOrder.CityReceiver,
            CitySender = newOrder.CitySender,
            Weight = newOrder.Weight,
            DateReceiving = newOrder.DateReceiving
        };
        return Ok(readOrderDto);
    }

    /// <summary>Get orders.</summary>
    /// <response code="200">Orders received.</response>
    [HttpGet]
    [ProducesResponseType(typeof(ReadOrderDto[]),StatusCodes.Status200OK)]
    public async Task<IActionResult> GetOrders()
    {
        var orders = await _context.Orders.ToListAsync();
        var readOrderDto = orders.Select(order => new ReadOrderDto
        {
            Id = order.Id,
            AdressReceiver = order.AdressReceiver,
            AdressSender = order.AdressSender,
            CityReceiver = order.CityReceiver,
            CitySender = order.CitySender,
            Weight = order.Weight,
            DateReceiving = order.DateReceiving
        });

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
        var order = await _context.Orders.SingleOrDefaultAsync(order => order.Id == id);
        if (order is null)
            return NotFound();

        var readOrderDto = new ReadOrderDto
        {
            Id = order.Id,
            AdressReceiver = order.AdressReceiver,
            AdressSender = order.AdressSender,
            CityReceiver = order.CityReceiver,
            CitySender = order.CitySender,
            Weight = order.Weight,
            DateReceiving = order.DateReceiving
        };
        return Ok(readOrderDto);
    }

    /// <summary>Delete order with matching id.</summary>
    /// <param name="id" example="xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx">Order id.</param>
    /// <response code="200">Deleted order.</response>
    /// <response code="404">Order with specified id was not found.</response>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(typeof(ReadOrderDto),StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteOrder([FromRoute] Guid id)
    {
        var deleteOrder = await _context.Orders.SingleOrDefaultAsync(order => order.Id == id);
        if (deleteOrder is null)
            return NotFound();
        _context.Orders.Remove(deleteOrder);
        await _context.SaveChangesAsync();
        var readOrderDto = new ReadOrderDto
        {
            Id = deleteOrder.Id,
            AdressReceiver = deleteOrder.AdressReceiver,
            AdressSender = deleteOrder.AdressSender,
            CityReceiver = deleteOrder.CityReceiver,
            CitySender = deleteOrder.CitySender,
            Weight = deleteOrder.Weight,
            DateReceiving = deleteOrder.DateReceiving
        };
        return Ok(readOrderDto);
    }
}