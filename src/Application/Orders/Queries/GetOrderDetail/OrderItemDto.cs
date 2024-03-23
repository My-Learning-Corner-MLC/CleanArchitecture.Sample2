namespace Sample2.Application.Orders.Queries;

public class OrderItemDto
{
    public ProductOrderedDto ItemOrdered { get; init; }
    public decimal UnitPrice { get; init; }
    public int Units { get; init; }
}