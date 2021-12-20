using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using System;
using System.Threading;

namespace SeleniumCourse.Tasks
{
    class Lesson4Task6
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

            var menu = this.driver.FindElement(By.CssSelector("ul#box-apps-menu"));
            int menuItemsAmount = menu.FindElements(By.CssSelector("li")).Count;

            Assert.Multiple(() =>
            {
                for (int i = 0; i < menuItemsAmount; i++)
                {
                    this.driver.Url = "http://localhost/litecart/admin/?app=users&doc=users";
                    this.driver.FindElements(By.CssSelector("ul#box-apps-menu > li"))[i].Click();

                    int subMenuItemsAmount = this.driver.FindElements(By.CssSelector("ul#box-apps-menu > li"))[i]
                                                        .FindElements(By.CssSelector("li")).Count;

                    System.Collections.ObjectModel.ReadOnlyCollection<IWebElement> headers;

                    if (subMenuItemsAmount > 0)
                    {
                        for (int j = 0; j < subMenuItemsAmount; j++)
                        {
                            this.driver.FindElements(By.CssSelector("ul#box-apps-menu > li"))[i]
                                                        .FindElements(By.CssSelector("li"))[j].Click();
                            headers = this.driver.FindElements(By.CssSelector("h1"));
                            Assert.True(headers.Count > 0, $"Page does not have header");
                        }
                    }
                    else
                    {
                        headers = this.driver.FindElements(By.CssSelector("h1"));
                        Assert.True(headers.Count > 0, $"Page does not have header");
                    }
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

