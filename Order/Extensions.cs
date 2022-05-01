namespace Orders.Order;

public static class OrderExtensions
{
    public static ReadOrderDto Map(this OrderEntity orderEntity)
        => new ReadOrderDto
        {
            Id = orderEntity.Id,
            AddressReceiver = orderEntity.AddressReceiver,
            AddressSender = orderEntity.AddressSender,
            CityReceiver = orderEntity.CityReceiver,
            CitySender = orderEntity.CitySender,
            DateReceiving = orderEntity.DateReceiving,
            Weight = orderEntity.Weight
        };
    public static OrderEntity Map(this CreateOrderDto createOrderDto)
        => new OrderEntity
        {
            Id = Guid.NewGuid(),
            AddressReceiver = createOrderDto.AddressReceiver,
            AddressSender = createOrderDto.AddressSender,
            CityReceiver = createOrderDto.CityReceiver,
            CitySender = createOrderDto.CitySender,
            Weight = createOrderDto.Weight,
            DateReceiving = DateTime.Now
        };

    public static OrderEntity Map(this UpdateOrderDto updateOrderDto)
        => new OrderEntity
        {
            AddressReceiver = updateOrderDto.AddressReceiver,
            AddressSender = updateOrderDto.AddressSender,
            CityReceiver = updateOrderDto.CityReceiver,
            CitySender = updateOrderDto.CitySender,
            Weight = updateOrderDto.Weight
        };
}