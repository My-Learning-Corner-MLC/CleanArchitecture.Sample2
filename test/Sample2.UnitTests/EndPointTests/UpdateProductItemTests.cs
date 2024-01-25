using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Moq;
using Sample2.API.Endpoints;
using Sample2.Application.Common.Exceptions;
using Sample2.Application.ProductItems.Commands.UpdateProductItem;

namespace Sample2.UnitTests.EndPointTests;

public class UpdateProductItemDetailTests
{
    [Fact]
    public async Task UpdateProductItem_IdDoesNotMatch_ShouldThrowValidationException()
    {
        // Arrange
        var mockURLId = 1;
        var mockCommandId = 2;
        var productItemFaker = DataMockHelper.UpdateProductItemCommandMock(id: mockCommandId);
        
        var mockSender = new Mock<ISender>();
        var productEndpoint = new ProductItems();

        // Act and Assert
        await Assert.ThrowsAsync<ValidationException>(() => productEndpoint.UpdateProductItem(mockSender.Object, mockURLId, productItemFaker));
    }
    
    [Fact]
    public async Task UpdateProductItem_UpdateProductSuccess_ShouldThrowValidationException()
    {
        // Arrange
        var mockURLId = 2;
        var mockCommandId = 2;
        var productItemFaker = DataMockHelper.UpdateProductItemCommandMock(id: mockCommandId);
        
        var mockSender = new Mock<ISender>();
        mockSender
            .Setup(s => s.Send(It.IsAny<UpdateProductItemCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(default(int));

        var productEndpoint = new ProductItems();

        // Act
        var response = await productEndpoint.UpdateProductItem(mockSender.Object, mockURLId, productItemFaker);

        //Assert
        Assert.IsType<NoContent>(response.Result);
    }
}