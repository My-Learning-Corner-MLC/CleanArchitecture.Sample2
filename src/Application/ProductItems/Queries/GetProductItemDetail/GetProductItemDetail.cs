using AutoMapper;
using MediatR;
using Sample2.Application.Common.Exceptions;
using Sample2.Application.Common.Interfaces;

namespace Sample2.Application.ProductItems.Queries.GetProductItemDetail;

public record GetProductItemDetailQuery : IRequest<ProductItemDetailDto>
{
    public int Id { get; init; }
}

public class GetProductItemDetailQueryHandler : IRequestHandler<GetProductItemDetailQuery, ProductItemDetailDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetProductItemDetailQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    => (_unitOfWork, _mapper) = (unitOfWork, mapper);

    public async Task<ProductItemDetailDto> Handle(GetProductItemDetailQuery request, CancellationToken cancellationToken)
    {
        var productItem = await _unitOfWork.Products.GetById(request.Id, cancellationToken);

        if (productItem is null) throw new NotFoundException(
            errorMessage: "Resource not found", 
            errorDescription: $"Could not found item with id: {request.Id}"
        );

        return _mapper.Map<ProductItemDetailDto>(productItem);
    }
}