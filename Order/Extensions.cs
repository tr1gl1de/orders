namespace Orders.Order;

public static class OrderExtensions
{
    public static ReadOrderDto ToReadOrderDto(this OrderEntity orderEntity)
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
    
}