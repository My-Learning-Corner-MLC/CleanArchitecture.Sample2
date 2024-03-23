namespace Sample2.Application.Common.Interfaces;

public interface IUnitOfWork
{
    IOrderRepository Orders { get; }
    IOrderItemRepository OrderItems { get; }
    IProductItemReferenceRepository ProductItemReferences { get; }

    Task SaveChangeAsync(CancellationToken cancellationToken);
}