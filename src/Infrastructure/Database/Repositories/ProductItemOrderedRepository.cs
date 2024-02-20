using Microsoft.Extensions.Logging;
using Sample2.Application.Common.Interfaces;
using Sample2.Domain.Entities;

namespace Sample2.Infrastructure.Database.Repositories;

public class ProductItemReferenceRepository : GenericRepository<ProductItemReference>, IProductItemReferenceRepository
{
    public ProductItemReferenceRepository(ApplicationDbContext context, ILogger<UnitOfWork> logger)
        : base(context, logger) { }

    public async Task<ProductItemReference?> GetById(int itemOrderedId, bool trackingChanges = false, CancellationToken cancellationToken = default)
    {
        return await GetBy(
            predicateExpression: p => p.Id == itemOrderedId,
            trackingChanges: trackingChanges, 
            cancellationToken: cancellationToken
        );
    }

    public async Task<ProductItemReference?> GetByName(string name, bool trackingChanges = false, CancellationToken cancellationToken = default)
    {
        return await GetBy(
            predicateExpression: p => p.Name == name,
            trackingChanges: trackingChanges,
            cancellationToken: cancellationToken
        );
    }
}