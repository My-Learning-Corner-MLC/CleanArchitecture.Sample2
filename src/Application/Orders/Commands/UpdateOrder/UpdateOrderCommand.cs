using MediatR;
using Sample2.Application.Common.Constants;
using Sample2.Application.Common.Exceptions;
using Sample2.Application.Common.Interfaces;

namespace Sample2.Application.Orders.Commands.UpdateOrder;

public record UpdateOrderCommand : IRequest<int>
{
    public int Id { get; init; }
    public string Status { get; init; }
    public string ShipToAddress { get; init; }
}

public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand, int>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateOrderCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<int> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
    {
        var order = await _unitOfWork.Orders.GetById(request.Id, trackingChanges: true, cancellationToken);
        if (order is null) throw new NotFoundException(
            errorMessage: ExceptionConst.ErrorMessages.RESOURCE_NOT_FOUND, 
            errorDescription: ExceptionConst.ErrorDescriptions.COULD_NOT_FOUND_ITEM_WITH_ID(request.Id)
        );

        if (order.Status != OrderStatus.ORDERD && order.Status != OrderStatus.IN_TRANSIT) 
            throw new ValidationException(errorDescription: OrderConst.ErrorMessages.ORDER_COULD_NOT_UPDATE);

        order.Status = request.Status;
        order.ShipToAddress = request.ShipToAddress;

        await _unitOfWork.SaveChangeAsync(cancellationToken: cancellationToken);

        return order.Id;
    }
}