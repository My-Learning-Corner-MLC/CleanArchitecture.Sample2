using System.Reflection;
using Sample2.Application.Common.Interfaces;
using Sample2.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Sample2.Infrastructure.Database;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<ProductItem> ProductItems => Set<ProductItem>();

    public DbSet<ProductBrand> ProductBrands => Set<ProductBrand>();

    public DbSet<ProductType> ProductTypes => Set<ProductType>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(modelBuilder);
    }
}
