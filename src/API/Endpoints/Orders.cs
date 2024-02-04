using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Sample2.API.Infrastructure;
using Sample2.Application.Common.Constants;
using Sample2.Application.Common.Exceptions;
using Sample2.Application.OrderItems.Queries.GetOrderDetail;
using Sample2.Application.Orders.Commands.CreateOrder;
using Sample2.Application.Orders.Commands.UpdateOrder;
using Sample2.Application.Orders.Queries;

namespace Sample1.API.Endpoints;

public class Orders : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this, "orders")
            .MapGet(GetOrderDetail, "/{id}")
            .MapPost(CreateOrder)
            .MapPut(UpdateOrder, "/{id}");
    }

    public async Task<Results<Ok<OrderDto>, NotFound, BadRequest>> GetOrderDetail(ISender sender, int id)
    {
        if (id < 0) throw new ValidationException(
            errorDescription: OrderConst.ErrorMessages.ORDER_ID_AT_LEAST_GREATER_THAN_0
        );

        var item = await sender.Send(new GetOrderDetailQuery { Id = id });

        return TypedResults.Ok(item);
    }

    public async Task<Results<Ok<int>, BadRequest>> CreateOrder(ISender sender, CreateOrderCommand command)
    {
        return TypedResults.Ok(await sender.Send(command));
    }

    public async Task<Results<NoContent, BadRequest, NotFound>> UpdateOrder(ISender sender, int id, UpdateOrderCommand command)
    {
        if (id != command.Id) 
            throw new ValidationException(
                errorDescription: $"The order id - {id} in url and id - {command.Id} in command do not match");
        
        await sender.Send(command);

        return TypedResults.NoContent();
    }
}