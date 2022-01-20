using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Support.Events;
using System;
using System.Collections.Generic;

namespace SeleniumCourse.Tasks
{
    class Lesson10Task17
    {
        private EventFiringWebDriver driver;
        private WebDriverWait wait;
        private DriverOptions options;

        [SetUp]
        public void Setup()
        {
            driver = new EventFiringWebDriver(new ChromeDriver());
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

        [Test]
        public void Test()
        {
            this.driver.Url = "http://localhost/litecart/admin/login.php";

            this.driver.FindElement(By.Name("username")).SendKeys("admin");
            this.driver.FindElement(By.Name("password")).SendKeys("admin");
            this.driver.FindElement(By.Name("login")).Click();

            this.driver.FindElement(By.CssSelector("#box-apps-menu > li:nth-child(2) > a")).Click();
            this.driver.FindElement(By.CssSelector("#content > form > table > tbody > tr > td > a")).Click();

            var catalogItems = this.driver.FindElements(By.CssSelector("#content table.dataTable > tbody > tr.row"));
            int positionOfFirstProductInCatalog = 0;

            foreach (var x in catalogItems)
            {
                if (x.FindElements(By.CssSelector("td > input[name*=\"products\"]")).Count > 0)
                {
                    break;
                }
                positionOfFirstProductInCatalog++;
            }

            List<string> logMessagesDuringProductOpenings = new List <string>();
            int amountOfEntriesInTheLog = this.driver.Manage().Logs.GetLog("browser").Count;

            for (int i = positionOfFirstProductInCatalog; i < catalogItems.Count; i++)
            {
                this.driver.FindElements(By.CssSelector("#content table.dataTable > tbody > tr.row"))[i]
                    .FindElement(By.CssSelector("td > a")).Click();

                wait.Until((driver) => driver.FindElement(By.CssSelector(".tabs .index .active > a[href=\"#tab-general\"]")));

                var browserLog = this.driver.Manage().Logs.GetLog("browser");

                if (amountOfEntriesInTheLog != browserLog.Count)
                {
                    for (int j = amountOfEntriesInTheLog == 0 ? 0 : amountOfEntriesInTheLog - 1; j < browserLog.Count ; j++)
                    {
                        logMessagesDuringProductOpenings.Add(browserLog[j].Message);
                    }
                    amountOfEntriesInTheLog = browserLog.Count;
                }

                this.driver.Url ="http://localhost/litecart/admin/?app=catalog&doc=catalog&category_id=1";
            }
            Assert.True(amountOfEntriesInTheLog == 0);
        }

        [TearDown]
        public void CleanUp()
        {
            driver.Quit();
            driver = null;
        }
    }
}
