using Sample2.Domain.Entities;

namespace Sample2.Application.Common.Interfaces;

public interface IProductRepository : IGenericRepository<ProductItem>
{
    Task <ProductItem?> GetById(int productId, CancellationToken cancellationToken);
    Task <ProductItem?> GetByName(string productName, CancellationToken cancellationToken);
}