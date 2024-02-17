using System;
using RefactorThis.Core.Domain;
using RefactorThis.Core.DTOs.Requests;

namespace RefactorThis.Core.Unit.Domain;

public class ProductTests
{
    [Fact]
    public void CreateProductFromRequest_ShouldBeCreatedWhenRequestIsValid()
    {
        // Arrange
        var request = new CreateProductRequest("someName", "someText", 20, 2);
        // Act
        var result = Product.CreateProductFromRequest(request);
        
        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(request.Name, result.Value.Name);
        Assert.Equal(request.Description, result.Value.Description);
        Assert.Equal(request.DeliveryPrice, result.Value.DeliveryPrice);
        Assert.Equal(request.Price, result.Value.Price);
    }
    
    [Fact]
    public void CreateProductFromRequest_ShouldNotBeCreatedWhenPriceIsLessThanZero()
    {
        // Arrange
        var request = new CreateProductRequest("someName", "someText", -5, 0);

        // Act
        var result = Product.CreateProductFromRequest(request);
        
        // Assert
        Assert.True(result.IsFailure);
        Assert.Equal("Invalid amount", result.Error);
    }
    
    [Fact]
    public void CreateProductFromRequest_ShouldNotBeCreatedWhenNameIsWhitespace()
    {
        // Arrange
        var request = new CreateProductRequest(" ", "someText", -5, 0);

        // Act
        var result = Product.CreateProductFromRequest(request);
        
        // Assert
        Assert.True(result.IsFailure);
        Assert.Contains("cannot be an empty string", result.Error);
    }

    [Fact]
    public void CreateProductFromUpdateRequest_ShouldBeCreatedWhenRequestIsValid()
    {
        // Arrange
        var request = new UpdateProductRequest("someName", "someText", 20, 2);
        // Act
        var result = Product.CreateProductFromUpdateRequest(Guid.NewGuid(), request);
        
        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(request.Name, result.Value.Name);
        Assert.Equal(request.Description, result.Value.Description);
        Assert.Equal(request.DeliveryPrice, result.Value.DeliveryPrice);
        Assert.Equal(request.Price, result.Value.Price);
    }
    
    
    [Theory]
    [InlineData(" ", "someText", 5, 0)]
    [InlineData("someName", "someText", -5, 0)]
    [InlineData("someName", "someText", 95, -1)]
    public void CreateProductFromRequest_ShouldNotBeCreatedWhenRequestIsInvalid(string name, string description, decimal price, decimal deliveryPrice)
    {
        // Arrange
        var request = new CreateProductRequest(name, description, price, deliveryPrice);

        // Act
        var result = Product.CreateProductFromRequest(request);
        
        // Assert
        Assert.True(result.IsFailure);
    }
    
    [Theory]
    [InlineData("someName", "someText", 99.999, 0, 100.00, 0)]
    [InlineData("someName", "someText", 8, 8.88888, 8, 8.89)]
    public void CreateProductFromRequest_ShouldBeRoundedToTwoDecimalPlacesWhenAmountsHaveMoreDecimalPlaces(string name, string description, decimal price, decimal deliveryPrice, decimal expectedPrice, decimal expectedDelivery)
    {
        // Arrange
        var request = new CreateProductRequest(name, description, price, deliveryPrice);

        // Act
        var result = Product.CreateProductFromRequest(request);
        
        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(expectedPrice, result.Value.Price);
        Assert.Equal(expectedDelivery, result.Value.DeliveryPrice);
    }
}