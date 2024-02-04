using Microsoft.Extensions.Logging;
using Sample2.Application.Common.Interfaces;
using Sample2.Domain.Entities;

namespace Sample2.Infrastructure.Database.Repositories;

public class OrderRepository : GenericRepository<Order>, IOrderRepository
{
    public OrderRepository(ApplicationDbContext context, ILogger<UnitOfWork> logger)
        : base(context, logger) { }

    public async Task<Order?> GetById(int orderId, bool trackingChanges = false,  CancellationToken cancellationToken = default)
    {
        return await GetBy(
            predicateExpression: p => p.Id == orderId,
            trackingChanges: trackingChanges,
            cancellationToken: cancellationToken
        );
    }

    public async Task<Order?> GetByIdWithOrderItem(int orderId, bool trackingChanges = false, CancellationToken cancellationToken = default)
    {
        return await GetBy(
            predicateExpression: p => p.Id == orderId,
            includeProperties: "OrderItems,OrderItems.ItemOrdered",
            trackingChanges: trackingChanges,
            cancellationToken: cancellationToken
        );
    }
}