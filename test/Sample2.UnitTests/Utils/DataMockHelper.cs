using Bogus;
using Sample2.Application.ProductItems.Commands.CreateProductItem;
using Sample2.Application.ProductItems.Commands.DeleteProductItem;
using Sample2.Application.ProductItems.Commands.UpdateProductItem;
using Sample2.Domain.Entities;

namespace Sample2.UnitTests.Utils;

public static class DataMockHelper
{
    public static ProductItem GetProductItemMock(int? id = default, string? name = default)
    {
        Faker<ProductItem> productFaker = new Faker<ProductItem>()
            .RuleFor(o => o.Id, f => id ?? f.UniqueIndex)
            .RuleFor(o => o.Name, f => name ?? f.Name.FullName())
            .RuleFor(o => o.Description, f => f.Lorem.Paragraph())
            .RuleFor(o => o.Price, f => f.Random.Decimal(1, 100))
            .RuleFor(o => o.PictureFileName, f => f.Lorem.Text())
            .RuleFor(o => o.PictureUri, f => f.Internet.Url())
            .RuleFor(o => o.ProductTypeId, f => f.Random.Int(1, 100))
            .RuleFor(o => o.ProductBrandId, f => f.Random.Int(1, 100));

        return productFaker;
    }

    public static ProductBrand GetProductBrandMock(int? id = default)
    {
        Faker<ProductBrand> brandFaker = new Faker<ProductBrand>()
            .RuleFor(o => o.Id, f => id ?? f.UniqueIndex)
            .RuleFor(o => o.Brand, f =>  f.Name.FullName());

        return brandFaker;
    }

    public static ProductType GetProductTypeMock(int? id = default)
    {
        Faker<ProductType> typeFaker = new Faker<ProductType>()
            .RuleFor(o => o.Id, f => id ?? f.UniqueIndex)
            .RuleFor(o => o.Type, f =>  f.Name.FullName());

        return typeFaker;
    }

    public static CreateProductItemCommand CreateProductItemCommandMock(string? name = default)
    {
        Faker<CreateProductItemCommand> commandFaker = new Faker<CreateProductItemCommand>()
            .RuleFor(o => o.Name, f => name ?? f.Name.FullName())
            .RuleFor(o => o.Description, f => f.Lorem.Paragraph())
            .RuleFor(o => o.Price, f => f.Random.Decimal(1, 100))
            .RuleFor(o => o.PictureFileName, f => f.Lorem.Text())
            .RuleFor(o => o.PictureUri, f => f.Internet.Url())
            .RuleFor(o => o.ProductTypeId, f => f.Random.Int(1, 100))
            .RuleFor(o => o.ProductBrandId, f => f.Random.Int(1, 100));

        return commandFaker;
    }

    public static UpdateProductItemCommand UpdateProductItemCommandMock(int? id = default, string? name = default)
    {
        Faker<UpdateProductItemCommand> commandFaker = new Faker<UpdateProductItemCommand>()
            .RuleFor(o => o.Id, f => id ?? f.UniqueIndex)
            .RuleFor(o => o.Name, f => name ?? f.Name.FullName())
            .RuleFor(o => o.Description, f => f.Lorem.Paragraph())
            .RuleFor(o => o.Price, f => f.Random.Decimal(1, 100))
            .RuleFor(o => o.PictureFileName, f => f.Lorem.Text())
            .RuleFor(o => o.PictureUri, f => f.Internet.Url())
            .RuleFor(o => o.ProductTypeId, f => f.Random.Int(1, 100))
            .RuleFor(o => o.ProductBrandId, f => f.Random.Int(1, 100));

        return commandFaker;
    }

    public static DeleteProductItemCommand DeleteProductItemCommandMock(int? id = default)
    {
        Faker<DeleteProductItemCommand> commandFaker = new Faker<DeleteProductItemCommand>()
            .RuleFor(o => o.Id, f => id ?? f.UniqueIndex);

        return commandFaker;
    }
}