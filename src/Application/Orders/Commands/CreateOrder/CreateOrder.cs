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

        var allProducts = await _unitOfWork.ItemOrdereds.GetAll(new() {Size = 0, Page = 0, CancellationToken = cancellationToken});
        var productsNotExist = orderItems.Select(oi => oi.ProductId).Except(allProducts.Select(p => p.Id)); 
        if (productsNotExist is not null && productsNotExist.Any()) throw new ValidationException(
            errorDescription: OrderConst.ErrorMessages.ORDER_HAS_PRODUCTS_THAT_DO_NOT_EXIST(string.Join( ", ", productsNotExist))
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