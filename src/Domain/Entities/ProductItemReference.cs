using Sample2.Domain.Common;

namespace Sample2.Domain.Entities;

public class ProductItemReference : BaseAuditableEntity
{
    public string Name { get; private set; }
    public string PictureUri { get; private set; }
}
