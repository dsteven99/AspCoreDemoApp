using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Xunit;

namespace AspCoreDemoApp.Test
{
    public class SeleniumIntegrationTests
    {
        public IWebDriver driver;

        public SeleniumIntegrationTests()
        {
            driver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
        }

        [Fact]
        [Trait("Category", "Integration")]
        public void ValidateChannelPageTitle()
        {
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl("https://msdn.microsoft.com/magazine/dd767791");
            Assert.Equal("MSDN Magazine", driver.Title);
        }
    }
}
