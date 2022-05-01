using Microsoft.EntityFrameworkCore;
using Orders.Order;

namespace Orders.Services;

public class OrderService : IOrderService
{
    private readonly DatabaseContext context;
    
    public OrderService(DatabaseContext context)
    {
        this.context = context;
    }
    
    public void Create(OrderEntity order)
    {
        context.Orders.Add(order);
    }

    public async Task<IEnumerable<OrderEntity>> GetAllAsync()
    {
        return await context.Orders.ToListAsync();
    }

    public async Task<OrderEntity> GetByIdAsync(Guid id)
    {
        return await context.Orders
            .SingleOrDefaultAsync(order => order.Id == id);
    }

    public void Delete(OrderEntity orderEntity)
    {
        context.Orders.Remove(orderEntity);
    }

    public async Task SaveAsync()
    {
        await context.SaveChangesAsync();
    }
}