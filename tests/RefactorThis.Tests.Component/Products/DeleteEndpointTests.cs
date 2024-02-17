using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json.Linq;
using RefactorThis.Api;
using RefactorThis.Core.DTOs.Requests;

namespace RefactorThis.Tests.Component.Products;

public class DeleteEndpointTests : IClassFixture<WebApplicationFactory<Startup>>
{
    private readonly WebApplicationFactory<Startup> _factory;

    public DeleteEndpointTests(WebApplicationFactory<Startup> factory)
    {
        _factory = factory;
    }
    
    [Fact]
    public async Task Delete_EndpointsReturnSuccess()
    {
        // Arrange
        var client = _factory.CreateClient();
        var content = await SetUpData(client);
        var productId = JObject.Parse(content)["id"]!.ToString();

        // Act
        var response = await client.DeleteAsync($"api/v1/Products/{productId}");

        // Assert
        response.EnsureSuccessStatusCode();
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    private static async Task<string> SetUpData(HttpClient client)
    {
        var createRequest = new CreateProductRequest("Test product", "to be deleted", 500, 10);
        var response = await client.PostAsJsonAsync("api/v1/Products", createRequest);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsStringAsync();
    }

    [Fact]
    public async Task Post_EndpointsReturnBadRequestWhenRequestIsInvalid()
    {
        // Arrange
        var client = _factory.CreateClient();
        var createRequest = new CreateProductRequest("Apple Vision Pro", "MR headset", 3500, -10);
    
        // Act
        var response = await client.PostAsJsonAsync("api/v1/Products", createRequest);
    
        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }
}