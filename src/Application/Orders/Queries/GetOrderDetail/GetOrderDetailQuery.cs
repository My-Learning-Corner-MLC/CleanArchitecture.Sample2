using AutoMapper;
using MediatR;
using Sample2.Application.Common.Exceptions;
using Sample2.Application.Common.Interfaces;
using Sample2.Application.Orders.Queries;
using Sample2.Domain.Entities;

namespace Sample2.Application.OrderItems.Queries.GetOrderDetail;

public record GetOrderDetailQuery : IRequest<OrderDto>
{
    public int Id { get; init; }
}

public class GetOrderDetailQueryHandler : IRequestHandler<GetOrderDetailQuery, OrderDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetOrderDetailQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    => (_unitOfWork, _mapper) = (unitOfWork, mapper);

    public async Task<OrderDto> Handle(GetOrderDetailQuery request, CancellationToken cancellationToken)
    {
        var order = await _unitOfWork.Orders.GetByIdWithOrderItem(request.Id, cancellationToken);

        if (order is null) throw new NotFoundException(
            errorMessage: "Resource not found", 
            errorDescription: $"Could not found item with id: {request.Id}"
        );

        return _mapper.Map<OrderDto>(order);
    }
}