using MediatR;
using Sample2.Application.Common.Constants;
using Sample2.Application.Common.Exceptions;
using Sample2.Application.Common.Interfaces;
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

        var allProduct = await _unitOfWork.ItemOrdereds.GetAll(size: 0, page: 0, cancellationToken: cancellationToken);
        var productNotExists = orderItems.Select(oi => oi.ProductName).Except(allProduct.Select(p => p.ProductName)); 
        if (productNotExists is not null && productNotExists.Any()) throw new ValidationException(
            errorDescription: OrderConst.ErrorMessages.ORDER_CONTAINS_NOT_EXISTS_PRODUCT(string.Join( ", ", productNotExists))
        );

        // Insert OrderItem
        var orderItemsEntity = new List<OrderItem>();
        foreach (var orderItem in orderItems)
        {
            var itemOrdered = await _unitOfWork.ItemOrdereds.GetByName(orderItem.ProductName, cancellationToken: cancellationToken);
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