using Sample2.Domain.Common;

namespace Sample2.Domain.Entities;

public class Order : BaseAuditableEntity
{
    public string BuyerId { get; set; }
    public string ShipToAddress { get; set; }
    public IList<OrderItem> OrderItems { get; set; }
    public int Status { get; set; } = 0;

    public decimal Total()
    {
        var total = 0m;
        foreach (var item in OrderItems)
        {
            total += item.UnitPrice * item.Units;
        }
        return total;
    }
}