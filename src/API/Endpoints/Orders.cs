using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Sample2.API.Infrastructure;
using Sample2.Application.Common.Constants;
using Sample2.Application.Common.Exceptions;
using Sample2.Application.OrderItems.Queries.GetOrderDetail;
using Sample2.Application.Orders.Queries;

namespace Sample1.API.Endpoints;

public class Orders : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this, "orders")
            .MapGet(GetOrderDetail, "/{id}");
    }

    public async Task<Results<Ok<OrderDto>, NotFound, BadRequest>> GetOrderDetail(ISender sender, int id)
    {
        if (id < 0) throw new ValidationException(
            errorDescription: OrderConst.ErrorMessages.ORDER_ID_AT_LEAST_GREATER_THAN_0
        );

        var item = await sender.Send(new GetOrderDetailQuery { Id = id });

        return TypedResults.Ok(item);
    }
}