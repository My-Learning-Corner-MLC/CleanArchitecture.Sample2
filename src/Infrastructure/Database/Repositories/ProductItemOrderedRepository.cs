using Microsoft.Extensions.Logging;
using Sample2.Application.Common.Interfaces;
using Sample2.Domain.Entities;

namespace Sample2.Infrastructure.Database.Repositories;

public class ProductItemOrderedRepository : GenericRepository<ProductItemOrdered>, IProductItemOrderedRepository
{
    public ProductItemOrderedRepository(ApplicationDbContext context, ILogger<UnitOfWork> logger)
        : base(context, logger) { }

    public async Task<ProductItemOrdered?> GetById(int itemOrderedId, bool trackingChanges = false, CancellationToken cancellationToken = default)
    {
        return await GetBy(
            predicateExpression: p => p.Id == itemOrderedId,
            trackingChanges: trackingChanges, 
            cancellationToken: cancellationToken
        );
    }

    public async Task<ProductItemOrdered?> GetByName(string name, bool trackingChanges = false, CancellationToken cancellationToken = default)
    {
        return await GetBy(
            predicateExpression: p => p.ProductName == name,
            trackingChanges: trackingChanges,
            cancellationToken: cancellationToken
        );
    }
}