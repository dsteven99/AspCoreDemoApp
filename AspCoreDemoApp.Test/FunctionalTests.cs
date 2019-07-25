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
    public class FunctionalTests
    {
        public IWebDriver driver;
        public FunctionalTests()
        {
            try
            {
                driver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
            }
            catch(Exception e)
            {
                Console.WriteLine("Exception while starting chrome..." + e);
            }
        }

        [Fact]
        [Trait("Category", "Functional")]
        public void ValidateChannelPageTitle()
        {
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl("https://msdn.microsoft.com/magazine/dd767791");
            Assert.Equal("MSDN Magazine", driver.Title);
        }

    }
}
