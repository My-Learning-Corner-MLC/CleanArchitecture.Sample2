using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sample2.Domain.Entities;

namespace Sample2.Infrastructure.Database.Configurations;

class ProductTypeConfiguration : IEntityTypeConfiguration<ProductType>
{
    public void Configure(EntityTypeBuilder<ProductType> builder)
    {
        builder.ToTable("ProductType");

        builder.Property(cb => cb.Type)
            .HasMaxLength(100);
    }
}
