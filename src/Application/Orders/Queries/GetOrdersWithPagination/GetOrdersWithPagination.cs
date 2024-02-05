using AutoMapper;
using MediatR;
using Sample2.Application.Common.Interfaces;
using Sample2.Application.Common.Models;

namespace Sample2.Application.Orders.Queries.GetOrdersWithPagination;

public record GetOrdersWithPaginationQuery : IRequest<PaginatedList<OrderBriefDto>>
{
    public string BuyerID { get; init; } = string.Empty;
    public bool IncludeTotalCount { get; set; } = false;
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 20;
}

public class GetOrdersWithPaginationQueryHandler : IRequestHandler<GetOrdersWithPaginationQuery, PaginatedList<OrderBriefDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetOrdersWithPaginationQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
         => (_unitOfWork, _mapper) = (unitOfWork, mapper);

    public async Task<PaginatedList<OrderBriefDto>> Handle(GetOrdersWithPaginationQuery request, CancellationToken cancellationToken)
    {
        var orders = await _unitOfWork.Orders
            .GetAllByBuyerIdWithOrderItem(
                buyerId: request.BuyerID,
                page: request.PageNumber,
                size: request.PageSize,
                cancellationToken: cancellationToken
            );

        int? count = null;
        if (request.IncludeTotalCount)
        {
            count = await _unitOfWork.Orders.CountAllByBuyerId(request.BuyerID, cancellationToken: cancellationToken);
        }

        var orderBriefs = _mapper.Map<IEnumerable<OrderBriefDto>>(orders);
        return new PaginatedList<OrderBriefDto>(orderBriefs.ToList().AsReadOnly(), count, request.PageNumber, request.PageSize);
    }
}