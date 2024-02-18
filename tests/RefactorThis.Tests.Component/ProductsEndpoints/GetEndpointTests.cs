using System.Net;
using Microsoft.AspNetCore.Mvc.Testing;
using RefactorThis.Api;

namespace RefactorThis.Tests.Component.ProductsEndpoints;

public class GetEndpointTests : IClassFixture<WebApplicationFactory<Startup>>
{
    private const string BaseRequestUrl = "api/v1/Products";
    private readonly WebApplicationFactory<Startup> _factory;

    public GetEndpointTests(WebApplicationFactory<Startup> factory)
    {
        _factory = factory;
    }
    
    [Fact]
    public async Task GetAll_EndpointsReturnSuccess()
    {
        // Arrange
        var client = _factory.CreateClient();

        // Act
        var response = await client.GetAsync(BaseRequestUrl);

        // Assert
        response.EnsureSuccessStatusCode();
    }
    
    [Fact]
    public async Task GetById_EndpointsReturnSuccess()
    {
        // Arrange
        const string productId = "8f2e9176-35ee-4f0a-ae55-83023d2db1a3";
        var client = _factory.CreateClient();

        // Act
        var response = await client.GetAsync($"{BaseRequestUrl}/{productId}");

        // Assert
        response.EnsureSuccessStatusCode();
    }
    
    [Fact]
    public async Task GetById_EndpointsReturn404WhenIdDoesNotExist()
    {
        // Arrange
        var productId = Guid.NewGuid();
        var client = _factory.CreateClient();

        // Act
        var response = await client.GetAsync($"{BaseRequestUrl}/{productId}");

        // Assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }
}