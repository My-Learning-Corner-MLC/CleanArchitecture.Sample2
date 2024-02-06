using Sample2.Domain.Common;

namespace Sample2.Domain.Entities;

public class OrderItem : BaseAuditableEntity
{
    public int ItemOrderedId { get; set; }
    public ProductItemOrdered ItemOrdered { get; set; }
    public decimal UnitPrice { get; set; }
    public int Units { get; set; }
}
