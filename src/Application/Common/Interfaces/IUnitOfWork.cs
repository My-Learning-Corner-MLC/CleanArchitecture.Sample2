namespace Sample2.Application.Common.Interfaces;

public interface IUnitOfWork
{
    IProductRepository Products { get; }
    IProductBrandRepository ProductBrands { get; }
    IProductTypeRepository ProductTypes { get; }

    Task SaveChangeAsync(CancellationToken cancellationToken);
}