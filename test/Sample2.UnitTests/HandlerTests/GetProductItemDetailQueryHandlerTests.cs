using Moq;
using Sample2.Application.Common.Exceptions;
using Sample2.Application.Common.Interfaces;
using Sample2.Application.ProductItems.Queries.GetProductItemDetail;
using Sample2.Domain.Entities;

namespace Sample2.UnitTests.HandlerTests;

public class GetProductItemDetailQueryHandlerTest
{
    [Fact]
    public async Task Handler_ProductNotFound_ShouldThrowNotFoundException()
    {
        // Arrange
        var mockUnitOfWork = new Mock<IUnitOfWork>();
        mockUnitOfWork
            .Setup(uow => uow.Products.GetById(It.IsAny<int>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(default(ProductItem));

        var autoMapper = AutoMapperHelper.GetMapperInstance();

        var queryHandler = new GetProductItemDetailQueryHandler(mockUnitOfWork.Object, autoMapper);

        // Act and Assert
        await Assert.ThrowsAsync<NotFoundException>(() => queryHandler.Handle(new() { Id = 0 }, CancellationToken.None));
    }

    [Fact]
    public async Task Handler_ProductFound_ShouldReturnProductItemDetailDto()
    {
        // Arrange
        var productMock = DataMockHelper.GetProductItemMock();
        var mockUnitOfWork = new Mock<IUnitOfWork>();
        mockUnitOfWork
            .Setup(uow => uow.Products.GetById(It.IsAny<int>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(productMock);

        var autoMapper = AutoMapperHelper.GetMapperInstance();

        var queryHandler = new GetProductItemDetailQueryHandler(mockUnitOfWork.Object, autoMapper);

        // Act
        var queryResult = await queryHandler.Handle(new() { Id = productMock.Id }, CancellationToken.None);

        // Assert
        Assert.NotNull(queryResult);
        Assert.Equal(productMock.Id, queryResult.Id);
    }
}