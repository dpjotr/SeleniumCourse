using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Linq;

namespace SeleniumCourse.Tasks
{
    class Lesson5Task9
    {
        private WebDriver driver;
        private WebDriverWait wait;

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
            this.driver.Url = "http://localhost/litecart/admin/?app=geo_zones&doc=geo_zones";

            this.driver.FindElement(By.Name("username")).SendKeys("admin");
            this.driver.FindElement(By.Name("password")).SendKeys("admin");
            this.driver.FindElement(By.Name("login")).Click();

            // Check that countries ordered by name and select countries for testing of geozones ordering.
            int amountOfCountries = this.driver.FindElements(By.CssSelector("tbody tr.row")).Count;

            Assert.Multiple(() =>
            {
                for (int i = 0; i < 
                amountOfCountries; i++)
                {
                    this.driver.Url = "http://localhost/litecart/admin/?app=geo_zones&doc=geo_zones";
                    this.driver.FindElements(By.CssSelector("tbody tr.row"))[i].FindElement(By.CssSelector("a")).Click();

                    var geozones = this.driver.FindElements(By.CssSelector("table#table-zones tr:not(.header)"));

                    var previousGeozone = geozones[0].FindElements(By.CssSelector("td"))[2]
                                                    .FindElement(By.CssSelector("option[selected]"))
                                                    .GetAttribute("textContent");
                                                        

                    foreach (var geozone in geozones)
                    {
                        if (geozone.FindElement(By.CssSelector("td>a")).GetAttribute("id") != "add_zone")
                        {
                            var currentGeozone = geozone.FindElements(By.CssSelector("td"))[2]
                                                     .FindElement(By.CssSelector("option[selected]"))
                                                     .GetAttribute("textContent");
                            Assert.True(
                                    String.Compare(previousGeozone, currentGeozone) >= 0,
                                    $"Geozones {previousGeozone} and {currentGeozone} are not ordered properly.");

                                previousGeozone = currentGeozone;
                        }
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
