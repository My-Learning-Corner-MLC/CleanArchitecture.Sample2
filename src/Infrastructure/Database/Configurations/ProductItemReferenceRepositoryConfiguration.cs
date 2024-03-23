using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sample2.Domain.Entities;

namespace Sample2.Infrastructure.Database.Configurations;

class ProductItemReferenceConfiguration : IEntityTypeConfiguration<ProductItemReference>
{
    public void Configure(EntityTypeBuilder<ProductItemReference> builder)
    {
        builder.ToTable("ProductItemReference");
    }
}
