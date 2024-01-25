using Sample2.Domain.Common;

namespace Sample2.Domain.Entities;

public class ProductType : BaseAuditableEntity
{
    public string Type { get; set; }
}
