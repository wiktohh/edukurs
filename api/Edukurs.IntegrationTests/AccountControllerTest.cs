using System.Net;
using FluentAssertions;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Mvc.Testing;
using Moq;

namespace Edukurs.IntegrationTests;

public class AccountControllerTest : IClassFixture<WebApplicationFactory<Program>>
{

    private HttpClient client;
    public AccountControllerTest(WebApplicationFactory<Program> factory)
    {
        client = factory.WithWebHostBuilder(builder =>
        {
            builder.ConfigureServices(services =>
            {
                services.AddSingleton<IPolicyEvaluator, FakePolicyEvaluator>();
            });
        }).CreateClient();
    }
    
    [Fact]
    public async Task GetAllUsers_ShouldReturnUnauthorized_WhenUserIsNotLogged()
    {
        // Arrange
        // Act
        var response = await client.GetAsync("/api/user/all");
        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }
    
    [Fact]
    public async Task GetAllUsers_ShouldReturnOk_WhenUserIsLogged()
    {
        // Arrange
        // Act
        var response = await client.GetAsync("/api/user/all");
        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }
    
    [Fact]
    public async Task GetUser_ShouldReturnUnauthorized_WhenUserIsNotLogged()
    {
        // Arrange
        // Act
        var response = await client.GetAsync("/api/user/1");
        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }
    
    [Fact]
    public async Task GetMe_ShouldReturnUnauthorized_WhenUserIsNotLogged()
    {
        // Arrange
        // Act
        var response = await client.GetAsync("/api/me");
        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }
}