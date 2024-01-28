using Sample2.Domain.Entities;

namespace Sample2.Application.Common.Interfaces;

public interface IProductItemOrderedRepository : IGenericRepository<ProductItemOrdered>
{
    Task <ProductItemOrdered?> GetById(int itemOrderedId, CancellationToken cancellationToken);
}