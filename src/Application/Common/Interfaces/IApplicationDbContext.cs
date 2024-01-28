using Microsoft.EntityFrameworkCore;
using Sample2.Domain.Entities;

namespace Sample2.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Order> Orders { get; }

    DbSet<OrderItem> OrderItems { get; }

    DbSet<ProductItemOrdered> ProductItemOrdereds { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
