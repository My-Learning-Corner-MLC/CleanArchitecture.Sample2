using Microsoft.Extensions.Logging;
using Sample2.Application.Common.Interfaces;
using Sample2.Domain.Entities;

namespace Sample2.Infrastructure.Database.Repositories;

public class ProductTypeRepository : GenericRepository<ProductType>, IProductTypeRepository
{
    public ProductTypeRepository(ApplicationDbContext context, ILogger<UnitOfWork> logger)
        : base(context, logger) { }

    public async Task<ProductType?> GetById(int typeId, CancellationToken cancellationToken)
    {
        return await GetBy(
            predicateExpression: p => p.Id == typeId, 
            cancellationToken: cancellationToken
        );
    }
}