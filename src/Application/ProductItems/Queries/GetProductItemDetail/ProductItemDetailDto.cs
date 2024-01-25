namespace Sample2.Application.ProductItems.Queries.GetProductItemDetail;

public class ProductItemDetailDto
{
    public int Id { get; init; }

    public string? Name { get; init; }

    public string? Description { get; init; }

    public decimal Price { get; init; }

    public string? PictureFileName { get; init; }

    public string? PictureUri { get; init; }
}