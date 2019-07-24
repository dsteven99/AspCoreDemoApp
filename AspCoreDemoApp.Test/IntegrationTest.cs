using AspCoreDemoApp.Data;
using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AspCoreDemoApp.Test
{
    public class IntegrationTest : IClassFixture<WebApplicationFactory<AspCoreDemoApp.Startup>>
    {
        private readonly WebApplicationFactory<Startup> factory;

        public IntegrationTest(WebApplicationFactory<AspCoreDemoApp.Startup> factory)
        {
            this.factory = factory;
        }

        [Theory]
        [InlineData("/Index")]
        [InlineData("/EditChannel/1")]
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
    }
}
