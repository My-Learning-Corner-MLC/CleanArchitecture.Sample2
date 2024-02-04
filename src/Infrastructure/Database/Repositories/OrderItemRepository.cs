using Microsoft.Extensions.Logging;
using Sample2.Application.Common.Interfaces;
using Sample2.Domain.Entities;

namespace Sample2.Infrastructure.Database.Repositories;

public class OrderItemRepository : GenericRepository<OrderItem>, IOrderItemRepository
{
    public OrderItemRepository(ApplicationDbContext context, ILogger<UnitOfWork> logger)
        : base(context, logger) { }

    public async Task<OrderItem?> GetById(int orderItemId, bool trackingChanges = false, CancellationToken cancellationToken = default)
    {
        return await GetBy(
            predicateExpression: p => p.Id == orderItemId,
            trackingChanges: trackingChanges,
            cancellationToken: cancellationToken
        );
    }
}