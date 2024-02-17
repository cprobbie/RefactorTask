using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json.Linq;
using RefactorThis.Api;
using RefactorThis.Core.DTOs.Requests;

namespace RefactorThis.Tests.Component.Products;

public class PostEndpointTests : IClassFixture<WebApplicationFactory<Startup>>
{
    private readonly WebApplicationFactory<Startup> _factory;

    public PostEndpointTests(WebApplicationFactory<Startup> factory)
    {
        _factory = factory;
    }
    
    [Fact]
    public async Task Post_EndpointsReturnSuccess()
    {
        // Arrange
        var client = _factory.CreateClient();
        var createRequest = new CreateProductRequest("Apple Vision Pro", "MR headset", 3500, 10);

        // Act
        var response = await client.PostAsJsonAsync("api/v1/Products", createRequest);

        // Assert
        response.EnsureSuccessStatusCode();
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        
        // Teardown
        await CleanUpData(client, response);
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
    
    
    private static async Task CleanUpData(HttpClient client, HttpResponseMessage response)
    {
        var content = await response.Content.ReadAsStringAsync();
        var productId = JObject.Parse(content)["id"];
        await client.DeleteAsync($"api/v1/Products/{productId}");
        response.EnsureSuccessStatusCode();
    }
}