namespace Sample2.Application.Orders.Queries;

public class OrderItemDto
{
    public ProductOrderedDto ItemOrdered { get; set; }
    public decimal UnitPrice { get; set; }
    public int Units { get; set; }
}