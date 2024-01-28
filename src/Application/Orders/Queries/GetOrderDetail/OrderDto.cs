using Sample2.Domain.Entities;

namespace Sample2.Application.Orders.Queries;

public class OrderDto
{
    public int Id { get; init; }
    public string BuyerId { get; init; }
    public DateTimeOffset OrderDate { get; init; }
    public string? ShipToAddress { get; init; }
    public IList<OrderItemDto> OrderItems { get; set; }
    public decimal Total { get; init; }
    public DateTimeOffset Created { get; init; }
}