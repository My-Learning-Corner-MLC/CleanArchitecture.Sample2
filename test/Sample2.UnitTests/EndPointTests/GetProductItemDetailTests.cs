using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Moq;
using Sample2.API.Endpoints;
using Sample2.Application.Common.Exceptions;
using Sample2.Application.ProductItems.Queries.GetProductItemDetail;

namespace Sample2.UnitTests.EndPointTests;

public class GetProductItemDetailTests
{    
    [Fact]
    public async Task GetProductItemDetail_InvalidId_ShouldThrowValidationException()
    {
        // Arrange
        var mockInvalidId = -1;
        var mockSender = new Mock<ISender>();
        var productEndpoint = new ProductItems();

        // Act and Assert
        await Assert.ThrowsAsync<ValidationException>(() => productEndpoint.GetProductItemDetail(mockSender.Object, mockInvalidId));
    }

    [Fact]
    public async Task GetProductItemDetail_ValidId_ShouldReturnOkWithProductDto()
    {
        // Arrange
        var validProductId = 1;
        var productItemFaker = DataMockHelper.GetProductItemMock(validProductId);
        var mapper = AutoMapperHelper.GetMapperInstance();
        var mockProductDetailDto = mapper.Map<ProductItemDetailDto>(productItemFaker);
        
        var mockSender = new Mock<ISender>();
        mockSender
            .Setup(s => s.Send(It.IsAny<GetProductItemDetailQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(mockProductDetailDto);

        var productEndpoint = new ProductItems();

        // Act 
        var response = await productEndpoint.GetProductItemDetail(mockSender.Object, validProductId);
        
        // Assert
        Assert.IsType<Results<Ok<ProductItemDetailDto>, NotFound, BadRequest>>(response);

        var result = (Ok<ProductItemDetailDto>)response.Result;
        Assert.NotNull(result.Value);
        Assert.Equal(validProductId, result.Value.Id);
    }
}