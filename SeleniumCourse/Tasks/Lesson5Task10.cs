using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace SeleniumCourse.Tasks
{
    class Lesson5Task10
    {
        private WebDriver driver;
        private WebDriverWait wait;

        [SetUp]
        public void Setup() 
        {
            this.driver = new OpenQA.Selenium.Chrome.ChromeDriver();
            this.driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(10);
            wait = new WebDriverWait(this.driver, TimeSpan.FromSeconds(10));
        }

        public void Setup(WebDriver driver)
        {
            this.driver = driver;
            this.driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(10);
            wait = new WebDriverWait(this.driver, TimeSpan.FromSeconds(10));
        }

        [Test]
        public void Test()
        {

            TestRun();
            CleanUp();

            Setup(new OpenQA.Selenium.Edge.EdgeDriver());
            TestRun();
            CleanUp();

            Setup(new OpenQA.Selenium.Firefox.FirefoxDriver());
            TestRun();

        }

        private void TestRun()
        {
            this.driver.Url = "http://localhost/litecart/en/";

            var product = this.driver.FindElement(By.CssSelector("div#box-campaigns.box > div.content > ul > li.product"));

            var mainPageProductDescription = new
            {
                productName = product.FindElement(By.CssSelector(".name")).GetAttribute("textContent").Trim(),

                standardProductPrice = int.Parse(product.FindElement(By.CssSelector(".regular-price")).GetAttribute("textContent")
                        .Replace("$","").Trim()),

                saleProductPrice = int.Parse(product.FindElement(By.CssSelector(".campaign-price")).GetAttribute("textContent")
                        .Replace("$", "").Trim()),

                standardProductPriceStyle = new
                {
                    isCrossedOut = product.FindElement(By.CssSelector(".regular-price")).TagName == "s",
                    color = GetColor(product.FindElement(By.CssSelector(".regular-price")).GetCssValue("color")),
                    size = product.FindElement(By.CssSelector(".regular-price")).Size,
                },

                saleProductPriceStyle = new
                {
                    isStrong = product.FindElement(By.CssSelector(".campaign-price")).TagName == "strong",
                    color = GetColor(product.FindElement(By.CssSelector(".campaign-price")).GetCssValue("color")),
                    size = product.FindElement(By.CssSelector(".campaign-price")).Size,
                },
            };

            product.FindElement(By.CssSelector("a")).Click();

            product = driver.FindElement(By.CssSelector("div#box-product"));

            var productPageProductDescription = new
            {
                productName = product.FindElement(By.CssSelector("h1.title")).GetAttribute("textContent").Trim(),
                standardProductPrice = int.Parse(product.FindElement(By.CssSelector(".regular-price")).GetAttribute("textContent")
                        .Replace("$", "").Trim()),

                saleProductPrice = int.Parse(product.FindElement(By.CssSelector(".campaign-price")).GetAttribute("textContent")
                        .Replace("$", "").Trim()),

                standardProductPriceStyle = new
                {
                    isCrossedOut = product.FindElement(By.CssSelector(".regular-price")).TagName == "s",
                    color = GetColor(product.FindElement(By.CssSelector(".regular-price")).GetCssValue("color")),
                    size = product.FindElement(By.CssSelector(".regular-price")).Size,
                },

                saleProductPriceStyle = new
                {
                    isStrong = product.FindElement(By.CssSelector(".campaign-price")).TagName == "strong",
                    color = GetColor(product.FindElement(By.CssSelector(".campaign-price")).GetCssValue("color")),
                    size = product.FindElement(By.CssSelector(".campaign-price")).Size,
                },
            };

            Assert.Multiple(() =>
            {
                // Verify product name is same
                Assert.True(
                    mainPageProductDescription.productName == productPageProductDescription.productName,
                    @"Product name on the main page differs from product name on the product page");

                // Verify prices
                Assert.True(
                    mainPageProductDescription.standardProductPrice == productPageProductDescription.standardProductPrice,
                    @"Standard product price on the main page differs from product price on the product page");
                Assert.True(
                    mainPageProductDescription.saleProductPrice == productPageProductDescription.saleProductPrice,
                    @"Sale product price on the main page differs from product price on the product page");

                // Verify price style on the main page
                var color = mainPageProductDescription.standardProductPriceStyle.color;
                Assert.True(
                    color[0] == color[1] && color[1] == color[2],
                    @"Standard price color is not grey on the main page");
                Assert.True(
                    mainPageProductDescription.standardProductPriceStyle.isCrossedOut,
                    @"Standard price is not crossed out on the main page");

                color = mainPageProductDescription.saleProductPriceStyle.color;
                Assert.True(
                    color[0] > 0 && color[1] == 0 && color[2] == 0,
                    @"Sale price color is not red on the main page");
                Assert.True(
                    mainPageProductDescription.saleProductPriceStyle.isStrong,
                    @"Sale price is not bold on the main page");

                // Verify price style on the product page
                color = productPageProductDescription.standardProductPriceStyle.color;
                Assert.True(
                    color[0] == color[1] && color[1] == color[2],
                    @"Standard price color is not grey on the product page");
                Assert.True(
                    mainPageProductDescription.standardProductPriceStyle.isCrossedOut,
                    @"Standard price color is not crossed out on the product page");

                color = productPageProductDescription.saleProductPriceStyle.color;
                Assert.True(
                    color[0] > 0 && color[1] == 0 && color[2] == 0,
                    @"Sale price color is not red on the product page");
                Assert.True(
                    productPageProductDescription.saleProductPriceStyle.isStrong,
                    @"Sale price is not bold on the product page");

                // Verify price size
                Assert.True(
                    mainPageProductDescription.standardProductPriceStyle.size.Height < 
                                            mainPageProductDescription.saleProductPriceStyle.size.Height,
                    @"Standard product price is bigger than sale price on the main page");
                Assert.True(
                    productPageProductDescription.standardProductPriceStyle.size.Height < 
                                            productPageProductDescription.saleProductPriceStyle.size.Height,
                    @"Standard product price is bigger than sale price on the product page");
            });
        }

        private int[] GetColor(string colorCode) => Regex.Match(colorCode, @"([^\(]*)(\()([^\(]*)(\))")
                                            .Groups[3].Value
                                            .Split(',').Select(x => int.Parse(x.Trim())).ToArray();  

        [TearDown]
        public void CleanUp()
        {
            if (driver != null)
            {
                driver.Quit();
                driver = null;
            }
        }
    }
}
