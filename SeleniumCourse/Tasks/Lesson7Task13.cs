using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;

namespace SeleniumCourse.Tasks
{
    class Lesson7Task13
    {
        private WebDriver driver;
        private WebDriverWait wait;
        private DriverOptions options;
        private int cartItemCounter;

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

        [Test]
        public void Test()
        {
            while(this.cartItemCounter < 5)
            {
                this.AddFirstPoularProductToCart();
            }

            this.driver.FindElement(By.CssSelector("#cart a.link")).Click();

            while(this.driver.FindElements(By.CssSelector("#box-checkout-cart ul.shortcuts li.shortcut")).Count > 1)
            {
                this.driver.FindElement(By.CssSelector("#box-checkout-cart ul.shortcuts > li > a")).Click();
                var itemToDeleteRemoveButton = this.driver.FindElement(
                    By.CssSelector("#box-checkout-cart button[name=\"remove_cart_item\"]"));

                var summaryTableRowAmount = this.driver.FindElements(
                    By.CssSelector("#box-checkout-summary table tr")).Count;

                itemToDeleteRemoveButton.Click();
                --summaryTableRowAmount;

                wait.Until((driver) =>
                    driver.FindElements(By.CssSelector("#box-checkout-summary table tr")).Count == summaryTableRowAmount);
            }

            this.driver.FindElement(
                    By.CssSelector("#box-checkout-cart button[name=\"remove_cart_item\"]")).Click();
        }

        // Add first product from the list of the most popular products to cart
        private void AddFirstPoularProductToCart()
        {
            this.driver.Url = "http://localhost/litecart/en/";

            this.driver.FindElement(By.CssSelector("div#box-most-popular.box ul.products:nth-child(1) a.link")).Click();

            this.cartItemCounter = int.Parse(
                this.driver.FindElement(By.CssSelector("#cart a.content span.quantity")).GetAttribute("textContent"));

            if (driver.FindElements(By.CssSelector(".options > select[name=\"options[Size]\"]")).Count > 0)
            {
                new SelectElement(driver.FindElement(By.CssSelector(".options > select[name=\"options[Size]\"]")))
                    .SelectByIndex(1);
            }

            this.driver.FindElement(
                By.CssSelector("#box-product .information .buy_now .quantity > button[name=\"add_cart_product\"]")).Click();

            wait.Until((driver) =>
                int.Parse(driver.FindElement(By.CssSelector("#cart a.content span.quantity")).GetAttribute("textContent"))
                    != cartItemCounter);

            ++this.cartItemCounter;
        }

        [TearDown]
        public void CleanUp()
        {
            driver.Quit();
            driver = null;
        }
    }
}
