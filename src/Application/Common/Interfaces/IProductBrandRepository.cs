using Sample2.Domain.Entities;

namespace Sample2.Application.Common.Interfaces;

public interface IProductBrandRepository : IGenericRepository<ProductBrand>
{
    Task <ProductBrand?> GetById(int brandId, CancellationToken cancellationToken);
}