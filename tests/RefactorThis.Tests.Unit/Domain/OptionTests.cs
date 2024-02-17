using System;
using RefactorThis.Core.Domain;
using RefactorThis.Core.DTOs.Requests;

namespace RefactorThis.Core.Unit.Domain;

public class OptionTests
{
    [Fact]
    public void CreateProductOptionFromRequest_ShouldBeCreatedWhenRequestIsValid()
    {
        // Arrange
        var request = new CreateProductOptionRequest("someName", "someText");
        // Act
        var result = Option.CreateProductOptionFromRequest(request);
        
        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(request.Name, result.Value.Name);
        Assert.Equal(request.Description, result.Value.Description);
    }
    
    [Theory]
    [InlineData("someName", "      ")]
    [InlineData("  ", "someText")]
    public void CreateProductOptionFromRequest_ShouldNotBeCreatedWhenRequestIsNotValid(string name, string description)
    {
        // Arrange
        var request = new CreateProductOptionRequest(name, description);

        // Act
        var result = Option.CreateProductOptionFromRequest(request);
        
        // Assert
        Assert.True(result.IsFailure);
    }
    
    [Fact]
    public void CreateProductOptionFromUpdateRequest_ShouldBeCreatedWhenRequestIsValid()
    {
        // Arrange
        var request = new UpdateProductOptionRequest("someName", "someText");
        // Act
        var result = Option.CreateOptionFromUpdateRequest(Guid.NewGuid(), request);
        
        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(request.Name, result.Value.Name);
        Assert.Equal(request.Description, result.Value.Description);
    }
    
    
    [Theory]
    [InlineData(" ", "someText")]
    [InlineData("someName", "   ")]
    public void CreateProductOptionFromRequest_ShouldNotBeCreatedWhenRequestIsInvalid(string name, string description)
    {
        // Arrange
        var request = new CreateProductOptionRequest(name, description);

        // Act
        var result = Option.CreateProductOptionFromRequest(request);
        
        // Assert
        Assert.True(result.IsFailure);
    }
}