using Moq;
using Sample2.Application.Common.Exceptions;
using Sample2.Application.Common.Interfaces;
using Sample2.Application.ProductItems.Commands.CreateProductItem;
using Sample2.Domain.Entities;

namespace Sample2.UnitTests.HandlerTests;

public class CreateProductItemCommandHandlerTests
{
    [Fact]
    public async Task Handler_ProductNameExists_ShouldThrowNotFoundException()
    {
        // Arrange
        var createProductCommandFaker = DataMockHelper.CreateProductItemCommandMock(name: "Summit Pro Harness");
        
        var productItemFaker = DataMockHelper.GetProductItemMock(name: "Summit Pro Harness");
        var mockUnitOfWork = new Mock<IUnitOfWork>();
        mockUnitOfWork
            .Setup(uow => uow.Products.GetByName(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(productItemFaker);

        var commandHandler = new CreateProductItemCommandHandler(mockUnitOfWork.Object);

        // Act and Assert
        await Assert.ThrowsAsync<ValidationException>(() => commandHandler.Handle(createProductCommandFaker, CancellationToken.None));
    }

    [Fact]
    public async Task Handler_ProductTypeDoesNotExists_ShouldReturnProductId()
    {
        // Arrange
        var createProductCommandFaker = DataMockHelper.CreateProductItemCommandMock();
        
        var mockUnitOfWork = new Mock<IUnitOfWork>();
        mockUnitOfWork
            .Setup(uow => uow.Products.GetByName(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(default(ProductItem));
        
        mockUnitOfWork
            .Setup(uow => uow.ProductTypes.GetById(It.IsAny<int>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(default(ProductType));

        var commandHandler = new CreateProductItemCommandHandler(mockUnitOfWork.Object);
        
        // Act and Assert
        await Assert.ThrowsAsync<ValidationException>(() => commandHandler.Handle(createProductCommandFaker, CancellationToken.None));
    }

    [Fact]
    public async Task Handler_ProductBrandDoesNotExists_ShouldReturnProductId()
    {
        // Arrange
        var createProductCommandFaker = DataMockHelper.CreateProductItemCommandMock();
        
        var mockUnitOfWork = new Mock<IUnitOfWork>();
        mockUnitOfWork
            .Setup(uow => uow.Products.GetByName(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(default(ProductItem));
        
        var productTypeFaker = DataMockHelper.GetProductTypeMock();
        mockUnitOfWork
            .Setup(uow => uow.ProductTypes.GetById(It.IsAny<int>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(productTypeFaker);

        mockUnitOfWork
            .Setup(uow => uow.ProductBrands.GetById(It.IsAny<int>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(default(ProductBrand));

        var commandHandler = new CreateProductItemCommandHandler(mockUnitOfWork.Object);
        
        // Act and Assert
        await Assert.ThrowsAsync<ValidationException>(() => commandHandler.Handle(createProductCommandFaker, CancellationToken.None));
    }
}