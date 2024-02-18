using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json.Linq;
using RefactorThis.Api;
using RefactorThis.Core.DTOs.Requests;

namespace RefactorThis.Tests.Component.OptionsEndpoints;

public class DeleteEndpointTests : IClassFixture<WebApplicationFactory<Startup>>
{
    private const string ProductId = "8f2e9176-35ee-4f0a-ae55-83023d2db1a3";
    private const string BaseRequestUrl = $"api/v1/Products/{ProductId}/options";
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
        var optionId = JObject.Parse(content)["OptionId"]!.ToString();

        // Act
        var response = await client.DeleteAsync($"{BaseRequestUrl}/{optionId}");

        // Assert
        response.EnsureSuccessStatusCode();
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    private static async Task<string> SetUpData(HttpClient client)
    {
        var createRequest = new CreateProductRequest("Test product", "to be deleted", 500, 10);
        var response = await client.PostAsJsonAsync(BaseRequestUrl, createRequest);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsStringAsync();
    }

    [Fact]
    public async Task Delete_EndpointsReturnBadRequestWhenRequestIsInvalid()
    {
        // Arrange
        var optionId = Guid.NewGuid();
        var client = _factory.CreateClient();
    
        // Act
        var response = await client.DeleteAsync($"{BaseRequestUrl}/{optionId}");
    
        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }
}