using MediatR;
using Microsoft.EntityFrameworkCore;
using Sample2.Application.Common.Constants;
using Sample2.Application.Common.Exceptions;
using Sample2.Application.Common.Interfaces;

namespace Sample2.Application.ProductItems.Commands.DeleteProductItem;

public record DeleteProductItemCommand : IRequest
{
    public int Id { get; init; }
}

public class DeleteProductItemCommandHandler : IRequestHandler<DeleteProductItemCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteProductItemCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(DeleteProductItemCommand request, CancellationToken cancellationToken)
    {
        var productItem = await _unitOfWork.Products.GetById(request.Id, cancellationToken);
        if (productItem is null) throw new NotFoundException(
            errorMessage: ExceptionConst.ErrorMessages.RESOURCE_NOT_FOUND, 
            errorDescription: ExceptionConst.ErrorDescriptions.COULD_NOT_FOUND_ITEM_WITH_ID(request.Id)
        );

        _unitOfWork.Products.Update(productItem);
        productItem.IsDeleted = true;

        await _unitOfWork.SaveChangeAsync(cancellationToken);
    }
}