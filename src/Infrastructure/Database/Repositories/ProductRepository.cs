using Microsoft.Extensions.Logging;
using Sample2.Application.Common.Interfaces;
using Sample2.Domain.Entities;

namespace Sample2.Infrastructure.Database.Repositories;

public class ProductRepository : GenericRepository<ProductItem>, IProductRepository
{
    public ProductRepository(ApplicationDbContext context, ILogger<UnitOfWork> logger)
        : base(context, logger) { }

    public async Task<ProductItem?> GetById(int productId, CancellationToken cancellationToken)
    {
        return await GetBy(
            predicateExpression: p => p.Id == productId, 
            cancellationToken: cancellationToken
        );
    }

    public async Task<ProductItem?> GetByName(string productName, CancellationToken cancellationToken)
    {
        return await GetBy(
            predicateExpression: p => p.Name == productName && p.IsDeleted == false,
            cancellationToken: cancellationToken
        );
    }
}