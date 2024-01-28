namespace Sample2.Application.Common.Interfaces;

public interface IUnitOfWork
{
    IOrderRepository Orders { get; }
    IOrderItemRepository OrderItems { get; }
    IProductItemOrderedRepository ItemOrdereds { get; }

    Task SaveChangeAsync(CancellationToken cancellationToken);
}