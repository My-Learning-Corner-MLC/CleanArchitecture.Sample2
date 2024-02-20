using Sample2.Domain.Entities;

namespace Sample2.Application.Common.Interfaces;

public interface IProductItemReferenceRepository : IGenericRepository<ProductItemReference>
{
    Task <ProductItemReference?> GetById(int itemOrderedId, bool trackingChanges = false, CancellationToken cancellationToken = default);
    Task<ProductItemReference?> GetByName(string name, bool trackingChanges = false, CancellationToken cancellationToken = default);
}