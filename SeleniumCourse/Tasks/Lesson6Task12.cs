using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.IO;
using System.Linq;

namespace SeleniumCourse.Tasks
{
    public enum status { enabled = 1, disabled }
    public enum gender { female = 2, male, unisex}
    public enum category { root = 1, rubberDuck, subCategory}
    public enum addNewProductTab { general = 1, information, data, prices, options, optionStock}
    class Lesson6Task12
    {
        private WebDriver driver;
        private WebDriverWait wait;
        private DriverOptions options;

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

        [Test]
        public void Test()
        {
            this.driver.Url = "http://localhost/litecart/admin/login.php";

            this.driver.FindElement(By.Name("username")).SendKeys("admin");
            this.driver.FindElement(By.Name("password")).SendKeys("admin");
            this.driver.FindElement(By.Name("login")).Click();

            this.driver.FindElement(By.CssSelector("#box-apps-menu > li:nth-child(2) > a:nth-child(1) > span:nth-child(2)")).Click();
            this.driver.FindElement(By.CssSelector("a.button:nth-child(2)")).Click();

            this.driver.FindElement(By.CssSelector($".index > li:nth-child({(int)addNewProductTab.general}) > a")).Click();

            this.driver.FindElement(
                By.CssSelector($"#tab-general > table > tbody > tr:nth-child(1) > td > label > input:nth-child({(int)status.enabled})")).Click();

            var productName = "Black Duck";

            this.driver.FindElement(
                By.CssSelector($"#tab-general > table > tbody > tr:nth-child(2) > td > span.input-wrapper input")).SendKeys(productName);

            var productId = "rd006";

            this.driver.FindElement(
                By.CssSelector($"#tab-general > table > tbody > tr:nth-child(3) > td > input")).SendKeys(productId);

            this.driver.FindElement(
                By.CssSelector($"#tab-general > table > tbody > tr:nth-child(4) > td [checked]")).Click();

            this.driver.FindElement(
                By.CssSelector(
                    $"#tab-general > table > tbody > tr:nth-child(4) > td table > tbody > tr:nth-child({(int)category.rubberDuck}) > td > input"))
                        .Click();

            this.driver.FindElement(
                By.CssSelector(
                    $"#tab-general > table > tbody > tr:nth-child(7) > td table > tbody > tr:nth-child({(int)gender.unisex}) > td > input"))
                        .Click();

            this.driver.FindElement(
                By.CssSelector("#tab-general > table > tbody > tr:nth-child(8) > td table > tbody > tr > td > input[name=\"quantity\"]"))
                    .Clear();

            this.driver.FindElement(
                By.CssSelector("#tab-general > table > tbody > tr:nth-child(8) > td table > tbody > tr > td > input[name=\"quantity\"]"))
                    .SendKeys("30");

            var unitSelectElement = this.driver.FindElement(
                By.CssSelector(
                    "#tab-general > table > tbody > tr:nth-child(8) > td table > tbody > tr > td > select[name=\"quantity_unit_id\"]"));
            new SelectElement(unitSelectElement).SelectByText("pcs");

            var deliveryStatusSelectElement = this.driver.FindElement(
                By.CssSelector(
                    "#tab-general > table > tbody > tr:nth-child(8) > td table > tbody > tr > td > select[name=\"delivery_status_id\"]"));
            new SelectElement(deliveryStatusSelectElement).SelectByText("3-5 days");

            var soldOutStatusSelectElement = this.driver.FindElement(
                By.CssSelector(
                    "#tab-general > table > tbody > tr:nth-child(8) > td table > tbody > tr > td > select[name=\"sold_out_status_id\"]"));
            new SelectElement(soldOutStatusSelectElement).SelectByText("Sold out");

            var path = (@"Images\blackDuckImage.jpeg");

            while(!File.Exists(path))
            {
                path = @"..\" + path;
            }

            this.driver.FindElement(By.CssSelector("#tab-general > table > tbody > tr:nth-child(9) input[type=\"file\"]"))
                    .SendKeys(Path.GetFullPath(path));

            var currentDate = DateTime.Now;

            this.driver.FindElement(By.CssSelector(
                "#tab-general > table > tbody > tr:nth-child(10) input[name=\"date_valid_from\"]")).Click();
            this.driver.FindElement(By.CssSelector(
                "#tab-general > table > tbody > tr:nth-child(10) input[name=\"date_valid_from\"]"))
                .SendKeys(currentDate.ToString("ddMMyyyy"));

            this.driver.FindElement(By.CssSelector(
                "#tab-general > table > tbody > tr:nth-child(11) input[name=\"date_valid_to\"]")).Click();
            this.driver.FindElement(By.CssSelector(
                "#tab-general > table > tbody > tr:nth-child(11) input[name=\"date_valid_to\"]"))
                .SendKeys(currentDate.AddYears(1).ToString("ddMMyyyy"));

            this.driver.FindElement(By.CssSelector($".index > li:nth-child({(int)addNewProductTab.information}) > a")).Click();
            new SelectElement(this.driver.FindElement(By.CssSelector("#tab-information select[name=\"manufacturer_id\"]"))).SelectByIndex(1);
            
            this.driver.FindElement(By.CssSelector($".index > li:nth-child({(int)addNewProductTab.data}) > a")).Click();
            this.driver.FindElement(By.CssSelector("#tab-data input[name=\"sku\"]")).SendKeys(productId);
            this.driver.FindElement(By.CssSelector("#tab-data input[name=\"gtin\"]")).SendKeys("4250883156684");
            this.driver.FindElement(By.CssSelector("#tab-data input[name=\"taric\"]")).SendKeys("1806 10 15 06");
            this.driver.FindElement(By.CssSelector("#tab-data input[name=\"weight\"]")).Clear();
            this.driver.FindElement(By.CssSelector("#tab-data input[name=\"weight\"]")).SendKeys("1");
            this.driver.FindElement(By.CssSelector("#tab-data input[name=\"dim_x\"]")).Clear();
            this.driver.FindElement(By.CssSelector("#tab-data input[name=\"dim_x\"]")).SendKeys("6");
            this.driver.FindElement(By.CssSelector("#tab-data input[name=\"dim_y\"]")).Clear();
            this.driver.FindElement(By.CssSelector("#tab-data input[name=\"dim_y\"]")).SendKeys("10");
            this.driver.FindElement(By.CssSelector("#tab-data input[name=\"dim_z\"]")).Clear();
            this.driver.FindElement(By.CssSelector("#tab-data input[name=\"dim_z\"]")).SendKeys("10");
            this.driver.FindElement(By.CssSelector("#tab-data textarea[name*=\"attributes\"]"))
                .SendKeys(
                $@" Colors
                    Body: Black
                    Eyes: White
                    Beak: Orange

                    Other
                    Material: Plastic");

            this.driver.FindElement(By.CssSelector($".index > li:nth-child({(int)addNewProductTab.prices}) > a")).Click();
            this.driver.FindElement(By.CssSelector("#tab-prices input[name=\"purchase_price\"]")).Clear();
            this.driver.FindElement(By.CssSelector("#tab-prices input[name=\"purchase_price\"]")).SendKeys("10");
            new SelectElement(this.driver.FindElement(
                By.CssSelector("#tab-prices select[name=\"purchase_price_currency_code\"]")))
                .SelectByText("US Dollars");
            this.driver.FindElement(By.CssSelector("#tab-prices input[name=\"prices[USD]\"]")).SendKeys("25");
            this.driver.FindElement(By.CssSelector("#tab-prices input[name=\"prices[EUR]\"]")).SendKeys("25");

            this.driver.FindElement(By.CssSelector(".button-set > button[name=\"save\"]")).Click();

            Assert.True(
                this.driver.FindElements(By.CssSelector(".dataTable tr > td > a"))
                .Where(x=> x.GetAttribute("textContent") == productName) != null,
                "New product was not found in the product list");            
        }

        [TearDown]
        public void CleanUp()
        {
            driver.Quit();
            driver = null;
        }
    }
}
