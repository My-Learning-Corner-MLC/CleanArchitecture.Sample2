using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sample2.Domain.Entities;

namespace Sample2.Infrastructure.Database.Configurations;

class ProductItemConfiguration : IEntityTypeConfiguration<ProductItem>
{
    public void Configure(EntityTypeBuilder<ProductItem> builder)
    {
        builder.ToTable("Product");

        builder.Property(pi => pi.Name)
            .HasMaxLength(50);

        builder.HasOne(pi => pi.ProductBrand)
            .WithMany();

        builder.HasOne(pi => pi.ProductType)
            .WithMany();

        builder.HasIndex(pi => pi.Name);
    }
}
