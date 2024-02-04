using Sample2.Domain.Entities;

namespace Sample2.Application.Common.Interfaces;

public interface IOrderRepository : IGenericRepository<Order>
{
    Task <Order?> GetById(int orderId, bool trackingChanges = false, CancellationToken cancellationToken = default);
    Task <Order?> GetByIdWithOrderItem(int orderId, bool trackingChanges = false, CancellationToken cancellationToken = default);
}