using Microsoft.Extensions.Logging;
using Sample2.Application.Common.Interfaces;
using Sample2.Domain.Entities;

namespace Sample2.Infrastructure.Database.Repositories;

public class ProductItemOrderedRepository : GenericRepository<ProductItemOrdered>, IProductItemOrderedRepository
{
    public ProductItemOrderedRepository(ApplicationDbContext context, ILogger<UnitOfWork> logger)
        : base(context, logger) { }

    public async Task<ProductItemOrdered?> GetById(int itemOrderedId, CancellationToken cancellationToken)
    {
        return await GetBy(
            predicateExpression: p => p.Id == itemOrderedId, 
            cancellationToken: cancellationToken
        );
    }
}