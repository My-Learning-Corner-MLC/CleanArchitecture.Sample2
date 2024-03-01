using MediatR;
using Sample2.Application.Constants;
using Sample2.Application.Common.Exceptions;
using Sample2.Application.Common.Interfaces;
using Sample2.Domain.Entities;

namespace Sample2.Application.Orders.Commands.CreateOrder;

public record CreateOrderCommand : IRequest<int>
{
    public string BuyerId { get; init; }
    public string ShipToAddress { get; init; }
    public IList<BasketItem> OrderItems { get; init; }
}

public record BasketItem
{
    public int ProductId { get; init; }
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
        var orderItemIds = orderItems.Select(oi => oi.ProductId).ToList();

        // TODO (services could communicate): Check if product exist in OrderDB first
        // If not exist => Get the product through Product Service, and then Add the Product to OrderDB.
        // If exist => Add Order and Order Item.

        var existedProducts = await _unitOfWork.ProductItemReferences.GetAllByIds(orderItemIds, cancellationToken: cancellationToken);
        var notExistedProducts = orderItemIds.Except(existedProducts.Select(p => p.Id));
        if (notExistedProducts is not null && notExistedProducts.Any()) throw new ValidationException(
            errorDescription: OrderConst.ErrorMessages.ORDER_HAS_PRODUCTS_THAT_DO_NOT_EXIST(string.Join( ", ", notExistedProducts))
        );

        // Insert OrderItem
        var orderItemsEntity = new List<OrderItem>();
        foreach (var orderItem in orderItems)
        {
            orderItemsEntity.Add(new OrderItem
            {
                ItemOrderedId = orderItem.ProductId,
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