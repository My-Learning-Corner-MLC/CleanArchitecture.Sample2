using Microsoft.Extensions.Logging;
using Sample2.Application.Common.Interfaces;
using Sample2.Domain.Entities;

namespace Sample2.Infrastructure.Database.Repositories;

public class ProductBrandRepository : GenericRepository<ProductBrand>, IProductBrandRepository
{
    public ProductBrandRepository(ApplicationDbContext context, ILogger<UnitOfWork> logger)
        : base(context, logger) { }

    public async Task<ProductBrand?> GetById(int brandId, CancellationToken cancellationToken)
    {
        return await GetBy(
            predicateExpression: p => p.Id == brandId, 
            cancellationToken: cancellationToken
        );
    }
}