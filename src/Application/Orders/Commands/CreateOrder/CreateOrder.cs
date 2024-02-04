using MediatR;
using Sample2.Application.Common.Constants;
using Sample2.Application.Common.Exceptions;
using Sample2.Application.Common.Interfaces;
using Sample2.Application.Common.Models;
using Sample2.Domain.Entities;

namespace Sample2.Application.Orders.Commands.CreateOrder;

public record CreateOrderCommand : IRequest<int>
{
    public string BuyerId { get; init; }
    public string ShipToAddress { get; init; }
    public IList<OrderItemCommand> OrderItems { get; init; }
}

public record OrderItemCommand
{
    public string ProductName { get; init; }
    public decimal UnitPrice { get; init; }
    public int Units { get; init; }
}

public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, int>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateOrderCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<int> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var orderItems = request.OrderItems;

        var allItems = await _unitOfWork.ItemOrdereds.GetAll(new() {Size = 0, Page = 0, TrackingChanges = true, CancellationToken = cancellationToken});
        var itemsNotExist = orderItems.Select(oi => oi.ProductName).Except(allItems.Select(p => p.ProductName)); 
        if (itemsNotExist is not null && itemsNotExist.Any()) throw new ValidationException(
            errorDescription: OrderConst.ErrorMessages.ORDER_CONTAINS_NOT_EXISTS_ITEMS(string.Join( ", ", itemsNotExist))
        );

        // Insert OrderItem
        var orderItemsEntity = new List<OrderItem>();
        foreach (var orderItem in orderItems)
        {
            var itemOrdered = allItems.Where(p => p.ProductName == orderItem.ProductName).FirstOrDefault();
            orderItemsEntity.Add(new OrderItem
            {
                ItemOrdered = itemOrdered,
                UnitPrice = orderItem.UnitPrice,
                Units = orderItem.Units,
            });
        };
        _unitOfWork.OrderItems.AddRange(orderItemsEntity);
        
        // Insert Order
        var orderEntity = new Order
        {
            BuyerId = request.BuyerId,
            ShipToAddress = request.ShipToAddress,
            OrderItems = orderItemsEntity
        };
        _unitOfWork.Orders.Add(orderEntity);

        await _unitOfWork.SaveChangeAsync(cancellationToken);

        return orderEntity.Id;
    }
}