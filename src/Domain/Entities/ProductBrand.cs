using Sample2.Domain.Common;

namespace Sample2.Domain.Entities;

public class ProductBrand : BaseAuditableEntity
{
    public string Brand { get; set; }
}
