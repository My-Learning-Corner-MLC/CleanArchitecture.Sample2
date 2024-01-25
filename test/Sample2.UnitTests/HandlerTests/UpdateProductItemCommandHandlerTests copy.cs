using Moq;
using Sample2.Application.Common.Exceptions;
using Sample2.Application.Common.Interfaces;
using Sample2.Application.ProductItems.Commands.UpdateProductItem;
using Sample2.Domain.Entities;

namespace Sample2.UnitTests.HandlerTests;

public class UpdateProductItemCommandHandlerTests
{
    [Fact]
    public async Task Handler_ProductNoFound_ShouldThrowNotFoundException()
    {
        // Arrange
        var updateProductCommandFaker = DataMockHelper.UpdateProductItemCommandMock();
        
        var mockUnitOfWork = new Mock<IUnitOfWork>();
        mockUnitOfWork
            .Setup(uow => uow.Products.GetById(It.IsAny<int>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(default(ProductItem));

        var commandHandler = new UpdateProductItemCommandHandler(mockUnitOfWork.Object);

        // Act and Assert
        await Assert.ThrowsAsync<NotFoundException>(() => commandHandler.Handle(updateProductCommandFaker, CancellationToken.None));
    }

    [Fact]
    public async Task Handler_ProductNameExists_ShouldThrowNotFoundException()
    {
        // Arrange
        var updateProductCommandFaker = DataMockHelper.UpdateProductItemCommandMock(name: "Summit Pro Harness");
        
        var productItemFaker = DataMockHelper.GetProductItemMock();
        var mockUnitOfWork = new Mock<IUnitOfWork>();
        mockUnitOfWork
            .Setup(uow => uow.Products.GetById(It.IsAny<int>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(productItemFaker);

        var productItemWithSameNameFaker = DataMockHelper.GetProductItemMock(name: "Summit Pro Harness");
        mockUnitOfWork
            .Setup(uow => uow.Products.GetByName(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(productItemWithSameNameFaker);

        var commandHandler = new UpdateProductItemCommandHandler(mockUnitOfWork.Object);

        // Act and Assert
        await Assert.ThrowsAsync<ValidationException>(() => commandHandler.Handle(updateProductCommandFaker, CancellationToken.None));
    }

    [Fact]
    public async Task Handler_ProductTypeDoesNotExists_ShouldReturnProductId()
    {
        // Arrange
        var updateProductCommandFaker = DataMockHelper.UpdateProductItemCommandMock();
        
        var mockUnitOfWork = new Mock<IUnitOfWork>();
        var productItemFaker = DataMockHelper.GetProductItemMock();
        mockUnitOfWork
            .Setup(uow => uow.Products.GetById(It.IsAny<int>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(productItemFaker);

        mockUnitOfWork
            .Setup(uow => uow.Products.GetByName(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(default(ProductItem));
        
        mockUnitOfWork
            .Setup(uow => uow.ProductTypes.GetById(It.IsAny<int>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(default(ProductType));

        var commandHandler = new UpdateProductItemCommandHandler(mockUnitOfWork.Object);
        
        // Act and Assert
        await Assert.ThrowsAsync<ValidationException>(() => commandHandler.Handle(updateProductCommandFaker, CancellationToken.None));
    }

    [Fact]
    public async Task Handler_ProductBrandDoesNotExists_ShouldReturnProductId()
    {
        // Arrange
        var updateProductCommandFaker = DataMockHelper.UpdateProductItemCommandMock();

        var mockUnitOfWork = new Mock<IUnitOfWork>();
        var productItemFaker = DataMockHelper.GetProductItemMock();
        mockUnitOfWork
            .Setup(uow => uow.Products.GetById(It.IsAny<int>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(productItemFaker);

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

        var commandHandler = new UpdateProductItemCommandHandler(mockUnitOfWork.Object);
        
        // Act and Assert
        await Assert.ThrowsAsync<ValidationException>(() => commandHandler.Handle(updateProductCommandFaker, CancellationToken.None));
    }
}