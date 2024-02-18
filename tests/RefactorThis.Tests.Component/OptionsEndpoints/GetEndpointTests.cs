using System.Net;
using Microsoft.AspNetCore.Mvc.Testing;
using RefactorThis.Api;

namespace RefactorThis.Tests.Component.OptionsEndpoints;

public class GetEndpointTests : IClassFixture<WebApplicationFactory<Startup>>
{
    private const string ProductId = "8f2e9176-35ee-4f0a-ae55-83023d2db1a3";
    private const string BaseRequestUrl = $"api/v1/Products/{ProductId}/options";
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
        var content = await response.Content.ReadAsStringAsync();
    }
    
    [Fact]
    public async Task GetById_EndpointsReturnSuccess()
    {
        // Arrange
        const string optionId = "0643CCF0-AB00-4862-B3C5-40E2731ABCC9";
        var client = _factory.CreateClient();

        // Act
        var response = await client.GetAsync($"{BaseRequestUrl}/{optionId}");

        // Assert
        response.EnsureSuccessStatusCode();
    }
    
    [Fact]
    public async Task GetById_EndpointsReturn404WhenIdDoesNotExist()
    {
        // Arrange
        var optionId = Guid.NewGuid();
        var client = _factory.CreateClient();

        // Act
        var response = await client.GetAsync($"{BaseRequestUrl}/{optionId}");

        // Assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }
}