using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json.Linq;
using RefactorThis.Api;
using RefactorThis.Core.DTOs.Requests;

namespace RefactorThis.Tests.Component.OptionsEndpoints;

public class PostEndpointTests : IClassFixture<WebApplicationFactory<Startup>>
{
    private const string ProductId = "8f2e9176-35ee-4f0a-ae55-83023d2db1a3";
    private const string BaseRequestUrl = $"api/v1/Products/{ProductId}/options";
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
        var createRequest = new CreateProductOptionRequest("Titanium Grey", "The coolest grey color");

        // Act
        var response = await client.PostAsJsonAsync(BaseRequestUrl, createRequest);

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
        var createRequest = new CreateProductOptionRequest("Titanium Grey", "  ");

        // Act
        var response = await client.PostAsJsonAsync(BaseRequestUrl, createRequest);

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }
    
    [Fact]
    public async Task Post_EndpointsReturnBadRequestWhenProductIdIsInvalid()
    {
        // Arrange
        var invalidProductId = Guid.NewGuid();
        var client = _factory.CreateClient();
        var createRequest = new CreateProductOptionRequest("Titanium Grey", "some text");

        // Act
        var response = await client.PostAsJsonAsync($"api/v1/Products/{invalidProductId}/Options", createRequest);

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        var message = await response.Content.ReadAsStringAsync();
        Assert.Equal("Product does not exist", message);
    }
    
    
    private static async Task CleanUpData(HttpClient client, HttpResponseMessage response)
    {
        var content = await response.Content.ReadAsStringAsync();
        var productId = JObject.Parse(content)["OptionId"];
        await client.DeleteAsync($"{BaseRequestUrl}/{productId}");
        response.EnsureSuccessStatusCode();
    }
}