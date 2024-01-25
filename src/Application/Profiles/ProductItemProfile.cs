using AutoMapper;
using Sample2.Application.ProductItems.Queries.GetProductItemDetail;
using Sample2.Application.ProductItems.Queries.GetProductItemsWithPagination;
using Sample2.Domain.Entities;

namespace Sample2.Application.Profiles;

public class ProductItemProfile : Profile
{
    public ProductItemProfile()
    {
        CreateMap<ProductItem, ProductItemDetailDto>();
        CreateMap<ProductItem, ProductItemBriefDto>();
    }
}