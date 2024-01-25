using Microsoft.EntityFrameworkCore;
using Sample2.Domain.Entities;

namespace Sample2.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<ProductItem> ProductItems { get; }

    DbSet<ProductBrand> ProductBrands { get; }

    DbSet<ProductType> ProductTypes { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
