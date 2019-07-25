using AspCoreDemoApp.Data;
using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AspCoreDemoApp.Test
{
    public class IntegrationTest : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> factory;

        public IntegrationTest(CustomWebApplicationFactory<Startup> factory)
        {
            this.factory = factory;
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
            var client = factory.CreateClient();

            // Act
            var response = await client.GetAsync(url);

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Assert.Equal("text/html; charset=utf-8",
                response.Content.Headers.ContentType.ToString());
        }
    }
}
