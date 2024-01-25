using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Sample2.API.Infrastructure;
using Sample2.Application.Common.Exceptions;
using Sample2.Application.Common.Models;
using Sample2.Application.ProductItems.Commands.CreateProductItem;
using Sample2.Application.ProductItems.Commands.UpdateProductItem;
using Sample2.Application.ProductItems.Commands.DeleteProductItem;
using Sample2.Application.ProductItems.Queries.GetProductItemDetail;
using Sample2.Application.ProductItems.Queries.GetProductItemsWithPagination;
using Sample2.Application.Common.Constants;

namespace Sample2.API.Endpoints;

public class ProductItems : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this, "products")
            .MapGet(GetProductItemDetail, "/{id}")
            .MapPost(GetProductItemsWithPagination, "/query")     
            .MapPost(CreateProductItem)
            .MapPut(UpdateProductItem, "/{id}")
            .MapDelete(DeleteProductItem, "/{id}");
    }

    public async Task<Results<Ok<ProductItemDetailDto>, NotFound, BadRequest>> GetProductItemDetail(ISender sender, int id)
    {
        if (id < 0) throw new ValidationException(ProductConst.ErrorMessages.PRODUCT_ID_AT_LEAST_GREATER_THAN_0);

        var item = await sender.Send(new GetProductItemDetailQuery { Id = id });

        return TypedResults.Ok(item);
    }

    public async Task<Results<Ok<PaginatedList<ProductItemBriefDto>>, BadRequest>> GetProductItemsWithPagination(ISender sender, GetProductItemsWithPaginationQuery query)
    {
        return TypedResults.Ok(await sender.Send(query));
    }
    
    public async Task<Results<Ok<int>, BadRequest>> CreateProductItem(ISender sender, CreateProductItemCommand command)
    {
        return TypedResults.Ok(await sender.Send(command));
    }

    public async Task<Results<NoContent, BadRequest, NotFound>> UpdateProductItem(ISender sender, int id, UpdateProductItemCommand command)
    {
        if (id != command.Id) 
            throw new ValidationException(
                errorDescription: $"The product id - {id} in url and id - {command.Id} in command do not match");
        
        await sender.Send(command);

        return TypedResults.NoContent();
    }

    public async Task<Results<NoContent, NotFound, BadRequest>> DeleteProductItem(ISender sender, int id)
    {
        if (id < 0) throw new ValidationException(ProductConst.ErrorMessages.PRODUCT_ID_AT_LEAST_GREATER_THAN_0);

        await sender.Send(new DeleteProductItemCommand() { Id = id });
        return TypedResults.NoContent();
    }
}