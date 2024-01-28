using Sample2.Domain.Common;

namespace Sample2.Domain.Entities;

public class ProductItemOrdered : BaseAuditableEntity
{
    public int ProductItemId { get; private set; }
    public string ProductName { get; private set; }
    public string PictureUri { get; private set; }
}
