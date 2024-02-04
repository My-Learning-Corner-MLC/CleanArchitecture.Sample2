using Sample2.Domain.Entities;

namespace Sample2.Application.Common.Interfaces;

public interface IProductItemOrderedRepository : IGenericRepository<ProductItemOrdered>
{
    Task <ProductItemOrdered?> GetById(int itemOrderedId, bool trackingChanges = false, CancellationToken cancellationToken = default);
    Task<ProductItemOrdered?> GetByName(string name, bool trackingChanges = false, CancellationToken cancellationToken = default);
}