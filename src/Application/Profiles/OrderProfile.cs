using AutoMapper;
using Sample2.Application.Orders.Queries;
using Sample2.Domain.Entities;

namespace Sample2.Application.Profiles;

public class OrderProfile : Profile
{
    public OrderProfile()
    {
        CreateMap<Order, OrderDto>();
        CreateMap<OrderItem, OrderItemDto>();
        CreateMap<ProductItemOrdered, ProductOrderedDto>();
        CreateMap<Order, OrderBriefDto>();
    }
}