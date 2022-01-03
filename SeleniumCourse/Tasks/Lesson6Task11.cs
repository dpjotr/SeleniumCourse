using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Linq;

namespace SeleniumCourse.Tasks
{
    class Lesson6Task11
    {
        private WebDriver driver;
        private WebDriverWait wait;
        private DriverOptions options;
        private string emailFirstPart;

        [SetUp]
        public void Setup()
        {
            this.driver = new ChromeDriver();
            driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(10);
            this.wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            this.emailFirstPart = "email" + new Random().Next(1000);
        }

        [Test]
        public void Test()
        {
            this.driver.Url = "http://localhost/litecart";

            Assert.Multiple(() =>
            {

                // Verify that there is a link for the new user creation on the main page
                Assert.DoesNotThrow(() =>
                    driver.FindElements(By.CssSelector("#box-account-login > div > form > table > tbody > tr > td > a"))
                        .Where(x => x.GetAttribute("textContent") == "New customers click here").First().Click(),
                    "Unable to locate link to Registration page");

                // Verify the user is on the Create Account page
                Assert.True(
                    this.driver.Url == @"http://localhost/litecart/en/create_account",
                    "Unable to get to the Create Account page"
                    );

                // Verify that all registration fields are available and editable
                Assert.DoesNotThrow(() =>
                {
                    driver.FindElement(By.CssSelector("#create-account input[name=\"tax_id\"]")).SendKeys("TaxId");
                    driver.FindElement(By.CssSelector("#create-account input[name=\"company\"]")).SendKeys("Company");
                    driver.FindElement(By.CssSelector("#create-account input[name=\"firstname\"]")).SendKeys("FirstName");
                    driver.FindElement(By.CssSelector("#create-account input[name=\"lastname\"]")).SendKeys("LastName");
                    driver.FindElement(By.CssSelector("#create-account input[name=\"address1\"]")).SendKeys("Address");
                    driver.FindElement(By.CssSelector("#create-account input[name=\"postcode\"]")).SendKeys("000000");
                    driver.FindElement(By.CssSelector("#create-account input[name=\"city\"]")).SendKeys("Novosibirsk");
                    driver.FindElement(By.CssSelector("#create-account .selection [role=\"combobox\"]")).Click();
                    driver.FindElement(By.CssSelector(".select2-search__field")).SendKeys("Russian Federation\n");
                    driver.FindElement(By.CssSelector("#create-account input[name=\"email\"]")).SendKeys(this.emailFirstPart + "@gmail.com");
                    driver.FindElement(By.CssSelector("#create-account input[name=\"phone\"]")).SendKeys("3151930");
                    driver.FindElement(By.CssSelector("#create-account input[name=\"password\"]")).SendKeys("DerParol");
                    driver.FindElement(By.CssSelector("#create-account input[name=\"confirmed_password\"]")).SendKeys("DerParol");
                },
                    "Unable to fill registration fields");

                //Verify Create Account button exists and is clickable
                Assert.DoesNotThrow(() =>
                    driver.FindElement(By.CssSelector("#create-account button[name=\"create_account\"]")).Click(),
                    "Unable to click Create Account button");

                // Verify account created and user has logged in and moved into his account page
                Assert.True(
                    this.driver.Url == @"http://localhost/litecart/en/",
                    "Unable to get into new user account"
                    );

                // Verify logout
                Assert.DoesNotThrow(() =>
                    driver.FindElement(By.CssSelector("#box-account > div > ul > li:nth-child(4) > a")).Click(),
                    "Unable to logout");

                // Verify login is possible with new user credentials
                Assert.DoesNotThrow(() =>
                {
                    driver.FindElement(By.CssSelector("#box-account-login input[name=\"email\"]")).SendKeys(this.emailFirstPart + "@gmail.com");
                    driver.FindElement(By.CssSelector("#box-account-login input[name=\"password\"]")).SendKeys("DerParol");
                    driver.FindElement(By.CssSelector(".button-set > button[name=\"login\"]")).Click();
                },
                    "Unable to login with new credentials");
            });

            // Logout
            driver.FindElement(By.CssSelector("#box-account > div > ul > li:nth-child(4) > a")).Click();
        }

        [TearDown]
        public void CleanUp()
        {   
            driver.Quit();
            driver = null;
        }
    }
}
