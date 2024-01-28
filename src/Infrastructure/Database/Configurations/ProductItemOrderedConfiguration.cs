using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sample2.Domain.Entities;

namespace Sample2.Infrastructure.Database.Configurations;

class ProductItemOrderedConfiguration : IEntityTypeConfiguration<ProductItemOrdered>
{
    public void Configure(EntityTypeBuilder<ProductItemOrdered> builder)
    {
        builder.ToTable("ProductItemOrdered");
    }
}
