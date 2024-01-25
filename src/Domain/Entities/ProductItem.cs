using Sample2.Domain.Common;

namespace Sample2.Domain.Entities;

public class ProductItem : BaseAuditableEntity
{
    public string? Name { get; set; }

    public string? Description { get; set; }

    public decimal Price { get; set; }

    public string? PictureFileName { get; set; }

    public string? PictureUri { get; set; }

    public int ProductTypeId { get; set; }

    public int ProductBrandId { get; set; }


    public ProductType? ProductType { get; set; }

    public ProductBrand? ProductBrand { get; set; }
}