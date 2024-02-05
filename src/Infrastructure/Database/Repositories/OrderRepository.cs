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
            predicateExpression: o => o.Id == orderId,
            trackingChanges: trackingChanges,
            cancellationToken: cancellationToken
        );
    }

    public async Task<Order?> GetByIdWithOrderItem(int orderId, bool trackingChanges = false, CancellationToken cancellationToken = default)
    {
        return await GetBy(
            predicateExpression: o => o.Id == orderId,
            includeProperties: "OrderItems,OrderItems.ItemOrdered",
            trackingChanges: trackingChanges,
            cancellationToken: cancellationToken
        );
    }

    public async Task<IEnumerable<Order>?> GetAllByBuyerIdWithOrderItem(string buyerId, int size, int page, bool trackingChanges = false, CancellationToken cancellationToken = default)
    {
        return await GetAll(new() {
            FilterExpression = (o) => o.BuyerId == buyerId,
            Size = size,
            Page = page,
            IncludeProperties = "OrderItems,OrderItems.ItemOrdered",
            TrackingChanges = trackingChanges,
            CancellationToken = cancellationToken
        });
    }

    public async Task <int> CountAllByBuyerId(string buyerId, CancellationToken cancellationToken = default)
    {
        return await CountAll(
            filterExpression: o => o.BuyerId == buyerId,
            cancellationToken: cancellationToken
        );
    }
}