using Orders.Order;

namespace Orders.Services;

public interface IOrderService
{
    public void Create(OrderEntity order);
    public Task<IEnumerable<OrderEntity>> GetAllAsync();
    public Task<OrderEntity> GetByIdAsync(Guid id);
    public void Delete(OrderEntity orderEntity);
    public Task SaveAsync();
}