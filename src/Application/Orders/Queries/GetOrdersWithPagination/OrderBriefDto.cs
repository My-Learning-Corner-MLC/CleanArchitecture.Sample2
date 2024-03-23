namespace Sample2.Application.Orders.Queries;

public class OrderBriefDto
{
    public int Id { get; init; }
    public string BuyerId { get; init; }
    public string? ShipToAddress { get; init; }
    public decimal Total { get; init; }
    public string Status { get; init; }
    public DateTimeOffset Created { get; init; }
}