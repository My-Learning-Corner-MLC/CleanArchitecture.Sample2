using MediatR;
using Sample2.Application.Common.Constants;
using Sample2.Application.Common.Exceptions;
using Sample2.Application.Common.Interfaces;
using Sample2.Domain.Entities;

namespace Sample2.Application.ProductItems.Commands.CreateProductItem;

public record CreateProductItemCommand : IRequest<int>
{
    public string Name { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public decimal Price { get; init; }
    public string PictureFileName { get; init; } = string.Empty;
    public string PictureUri { get; init; } = string.Empty;
    public int ProductTypeId { get; init; }
    public int ProductBrandId { get; init; }
}

public class CreateProductItemCommandHandler : IRequestHandler<CreateProductItemCommand, int>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateProductItemCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<int> Handle(CreateProductItemCommand request, CancellationToken cancellationToken)
    {
        var isExistsProductName = await _unitOfWork.Products.GetByName(request.Name, cancellationToken);
        if (isExistsProductName is not null) throw new ValidationException(
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

        var entity = new ProductItem
        {
            Name = request.Name,
            Description = request.Description,
            Price = request.Price,
            PictureFileName = request.PictureFileName,
            PictureUri = request.PictureUri,
            ProductTypeId = request.ProductTypeId,
            ProductBrandId = request.ProductBrandId
        };

        _unitOfWork.Products.Add(entity);

        await _unitOfWork.SaveChangeAsync(cancellationToken);

        return entity.Id;
    }
}