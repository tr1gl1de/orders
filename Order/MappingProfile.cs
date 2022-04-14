using AutoMapper;

namespace Orders.Order;

public class OrderMappingProfile : Profile
{
    public OrderMappingProfile()
    {
        CreateMap<OrderEntity, ReadOrderDto>();
        CreateMap<CreateOrderDto, OrderEntity>();
    }
}