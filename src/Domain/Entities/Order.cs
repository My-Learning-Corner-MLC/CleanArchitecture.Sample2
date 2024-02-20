using Sample2.Domain.Common;
using Sample2.Domain.Constants;

namespace Sample2.Domain.Entities;

public class Order : BaseAuditableEntity
{
    public string BuyerId { get; set; }
    public string ShipToAddress { get; set; }
    public IList<OrderItem> OrderItems { get; set; }
    public string Status { get; set; } = OrderStatus.ORDERED;

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