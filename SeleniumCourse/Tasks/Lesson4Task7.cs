using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;

namespace SeleniumCourse.Tasks
{
    class Lesson4Task7
    {
        private WebDriver driver;
        private WebDriverWait wait;
        private DriverOptions options;

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(10);
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

        [Test]
        public void Test()
        {
            this.driver.Url = "http://localhost/litecart";
            
            Assert.Multiple(() =>
            {
                var products = this.driver.FindElements(By.CssSelector("li.product"));
                foreach (var product in products)
                {
                    int amountOfStickers = product.FindElements(By.CssSelector("[class*=sticker]")).Count;
                    Assert.True(amountOfStickers != 1);
                }
            });

        }

        [TearDown]
        public void CleanUp()
        {
            driver.Quit();
            driver = null;
        }
    }
}
