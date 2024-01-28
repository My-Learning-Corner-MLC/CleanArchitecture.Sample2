using Sample2.Domain.Entities;

namespace Sample2.Application.Common.Interfaces;

public interface IOrderRepository : IGenericRepository<Order>
{
    Task <Order?> GetById(int orderId, CancellationToken cancellationToken);
    Task <Order?> GetByIdWithOrderItem(int orderId, CancellationToken cancellationToken);
}