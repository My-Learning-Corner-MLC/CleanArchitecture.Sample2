using Sample2.Domain.Entities;

namespace Sample2.Application.Common.Interfaces;

public interface IOrderItemRepository : IGenericRepository<OrderItem>
{
    Task <OrderItem?> GetById(int orderItemId, bool trackingChanges = false, CancellationToken cancellationToken = default);
}