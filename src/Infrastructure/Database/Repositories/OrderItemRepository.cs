using Microsoft.Extensions.Logging;
using Sample2.Application.Common.Interfaces;
using Sample2.Domain.Entities;

namespace Sample2.Infrastructure.Database.Repositories;

public class OrderItemRepository : GenericRepository<OrderItem>, IOrderItemRepository
{
    public OrderItemRepository(ApplicationDbContext context, ILogger<UnitOfWork> logger)
        : base(context, logger) { }

    public async Task<OrderItem?> GetById(int orderItemId, CancellationToken cancellationToken)
    {
        return await GetBy(
            predicateExpression: p => p.Id == orderItemId, 
            cancellationToken: cancellationToken
        );
    }
}