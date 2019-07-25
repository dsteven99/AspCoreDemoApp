using AngleSharp.Html.Dom;
using AspCoreDemoApp.Data;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using AspCoreDemoApp.Core;

namespace AspCoreDemoApp.Test
{
    public class IntegrationTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> factory;
        private readonly HttpClient client;
        public IntegrationTests(CustomWebApplicationFactory<Startup> factory)
        {
            this.factory = factory;
            client = factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });
        }

        [Theory]
        [InlineData("/Index")]
        [InlineData("/EditChannel/1")]
        [InlineData("/DeleteChannel/1")]
        [InlineData("/AddChannel")]
        [InlineData("/Videos/Index/1")]
        [InlineData("/Videos/AddVideo/1")]
        [InlineData("/Videos/EditVideo/1")]
        [InlineData("/Videos/DeleteVideo/1")]
        [InlineData("/Videos/Screen/1")]
        [Trait("Category", "Integration")]
        public async Task Get_EndpointsReturnSuccessAndCorrectContentType(string url)
        {
            // Arrange
            var client = factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    var serviceProvider = services.BuildServiceProvider();

                    using (var scope = serviceProvider.CreateScope())
                    {
                        var scopedServices = scope.ServiceProvider;
                        var db = scopedServices
                            .GetRequiredService<VideoDbContext>();
                        var logger = scopedServices
                            .GetRequiredService<ILogger<IntegrationTests>>();

                        try
                        {
                            Utilities.InitializeDbForTests(db);
                        }
                        catch (Exception ex)
                        {
                            logger.LogError(ex, "An error occurred seeding " +
                                "the database with test messages. Error: " +
                                ex.Message);
                        }
                    }
                });
            })
       .CreateClient(new WebApplicationFactoryClientOptions
       {
           AllowAutoRedirect = false
       });

            // Act
            var response = await client.GetAsync(url);

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Assert.Equal("text/html; charset=utf-8",
                response.Content.Headers.ContentType.ToString());
        }

        [Fact]
        [Trait("Category", "Integration")]
        public async Task Post_DeleteVideoHandler_ReturnsRedirectToVideoIndex()
        {
            // Arrange
            var defaultPage = await client.GetAsync("/Videos/DeleteVideo/1");
            var content = await HtmlHelpers.GetDocumentAsync(defaultPage);

            //Act
            var response = await client.SendAsync(
                (IHtmlFormElement)content.QuerySelector("form"),
                (IHtmlButtonElement)content.QuerySelector("button"));

            // Assert
            Assert.Equal(HttpStatusCode.OK, defaultPage.StatusCode);
            Assert.Equal(HttpStatusCode.Redirect, response.StatusCode);
            Assert.Equal("/Videos/1", response.Headers.Location.OriginalString);
        }

        [Fact]
        [Trait("Category", "Integration")]
        public async Task GetChannelData_ProvideChannelNameInPage()
        {
            //Arrange
            var client = factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    services.AddScoped<IData<Channel>, TestSqlChannelData>();
                });
            })
       .CreateClient();

            //Act
            var defaultPage = await client.GetAsync("/Videos/1");
            var content = await HtmlHelpers.GetDocumentAsync(defaultPage);
            var h4Element = content.QuerySelector("h4");

            //Assert
            Assert.Contains("", h4Element.InnerHtml);
                

        }
    }
}
