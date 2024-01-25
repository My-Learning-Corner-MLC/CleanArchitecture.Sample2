namespace Sample2.Application.ProductItems.Queries.GetProductItemsWithPagination;

public class ProductItemBriefDto
{
    public int Id { get; init; }

    public string? Name { get; init; }

    public decimal Price { get; init; }

    public string? PictureFileName { get; init; }

    public string? PictureUri { get; init; }
}