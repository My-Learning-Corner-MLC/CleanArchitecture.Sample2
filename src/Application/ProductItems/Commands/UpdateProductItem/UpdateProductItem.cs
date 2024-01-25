using MediatR;
using Microsoft.EntityFrameworkCore;
using Sample2.Application.Common.Constants;
using Sample2.Application.Common.Exceptions;
using Sample2.Application.Common.Interfaces;

namespace Sample2.Application.ProductItems.Commands.UpdateProductItem;

public record UpdateProductItemCommand : IRequest<int>
{
    public int Id {get; init;}
    public string Name { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public decimal Price { get; init; }
    public string PictureFileName { get; init; } = string.Empty;
    public string PictureUri { get; init; } = string.Empty;
    public int ProductTypeId { get; init; }
    public int ProductBrandId { get; init; }
}

public class UpdateProductItemCommandHandler : IRequestHandler<UpdateProductItemCommand, int>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateProductItemCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<int> Handle(UpdateProductItemCommand request, CancellationToken cancellationToken)
    {
        var productItem = await _unitOfWork.Products.GetById(request.Id, cancellationToken);
        if (productItem is null) throw new NotFoundException(
            errorMessage: ExceptionConst.ErrorMessages.RESOURCE_NOT_FOUND, 
            errorDescription: ExceptionConst.ErrorDescriptions.COULD_NOT_FOUND_ITEM_WITH_ID(request.Id)
        );

        var isExistsProductName = await _unitOfWork.Products.GetByName(request.Name, cancellationToken); 
        if (isExistsProductName is not null && isExistsProductName.Id != request.Id) throw new ValidationException(
            errorDescription: ProductConst.ErrorMessages.PRODUCT_NAME_ALREADY_EXISTS
        );

        var isExistsTypeId = await _unitOfWork.ProductTypes.GetById(request.ProductTypeId, cancellationToken);
        if (isExistsTypeId is null) throw new ValidationException(
            errorDescription: TypeConst.ErrorMessages.TYPE_ID_DOES_NOT_EXISTS
        );

        var isExistsBrandId = await _unitOfWork.ProductBrands.GetById(request.ProductBrandId, cancellationToken);
        if (isExistsBrandId is null) throw new ValidationException(
            errorDescription: BrandConst.ErrorMessages.BRAND_ID_DOES_NOT_EXISTS
        );

        _unitOfWork.Products.Update(productItem);

        productItem.Name = request.Name;
        productItem.Description = request.Description;
        productItem.Price = request.Price;
        productItem.PictureFileName = request.PictureFileName;
        productItem.PictureUri = request.PictureUri;
        productItem.ProductTypeId = request.ProductTypeId;
        productItem.ProductBrandId = request.ProductBrandId;
        productItem.LastModified = DateTime.Now;

        await _unitOfWork.SaveChangeAsync(cancellationToken);

        return productItem.Id;
    }
}