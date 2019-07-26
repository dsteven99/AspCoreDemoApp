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
            driver.Navigate().GoToUrl("http:/localhost:5000");
            Assert.Equal("Home page - AspCoreDemoApp", driver.Title);
            driver.Close();
        }

        [Fact]
        [Trait("Category", "Integration")]
        public void SearchSongwritersChannel()
        {
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl("http:/localhost:5000");
            driver.FindElement(By.ClassName("form-control"))
                .SendKeys("Songwriters");
            driver.FindElement(By.ClassName("form-control"))
                .SendKeys(Keys.Enter);
            driver.Close();
        }

        [Fact]
        [Trait("Category", "Integration")]
        public void AddNewChannel()
        {
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl("http:/localhost:5000");
            driver.FindElement(By.XPath("//a[@href='/AddChannel']")).Click();
            driver.FindElement(By.Name("Channel.Title"))
                .SendKeys("Test Channel Title");
            driver.FindElement(By.Name("Channel.Description"))
               .SendKeys("Test Channel Description");
            driver.FindElement(By.XPath("//button[@type='submit']")).Click();
            driver.Close();
        }
    }
}
