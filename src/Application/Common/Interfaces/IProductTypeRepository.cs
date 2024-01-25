using Sample2.Domain.Entities;

namespace Sample2.Application.Common.Interfaces;

public interface IProductTypeRepository : IGenericRepository<ProductType>
{
    Task <ProductType?> GetById(int typeId, CancellationToken cancellationToken);
}