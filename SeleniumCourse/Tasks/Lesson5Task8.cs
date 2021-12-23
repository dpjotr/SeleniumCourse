using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SeleniumCourse.Tasks
{
    class Lesson5Task8
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
            this.driver.Url = "http://localhost/litecart/admin/?app=countries&doc=countries";

            this.driver.FindElement(By.Name("username")).SendKeys("admin");
            this.driver.FindElement(By.Name("password")).SendKeys("admin");
            this.driver.FindElement(By.Name("login")).Click();

            var IdsOfCountriesWithGeozones = new List<String>();

            // Check that countries ordered by name and select countries for testing of geozones ordering.
            Assert.Multiple(() =>
            {
                var countries = this.driver.FindElements(By.CssSelector("tr.row"));
                var previousCountry = countries[0].FindElements(By.CssSelector("a"))[0].GetAttribute("textContent");
                foreach (var country in countries)
                {
                    var currentCountry = country.FindElements(By.CssSelector("a"))[0].GetAttribute("textContent");
                    Assert.True(
                        String.Compare(previousCountry, currentCountry) <= 0,
                        $"Countries {currentCountry} and {previousCountry} are not ordered properly.");


                    previousCountry = currentCountry;

                    int geozones = int.Parse(country.FindElements(By.CssSelector("td"))[5].GetAttribute("textContent"));
                    if (geozones > 0)
                    {
                        IdsOfCountriesWithGeozones.Add(country.FindElements(By.CssSelector("td"))[2].GetAttribute("textContent"));
                    }
                }
            });

            // Check geozones ordering in Countries having more than 0 geozones

            Assert.Multiple(() =>
            {
                foreach (var id in IdsOfCountriesWithGeozones)
                {
                    this.driver.Url = "http://localhost/litecart/admin/?app=countries&doc=countries";
                    this.driver.FindElements(By.CssSelector("tr.row"))
                                .Where(x => x.FindElements(By.CssSelector("td"))[2].GetAttribute("textContent") == id)
                                .First()
                                .FindElement(By.CssSelector("a"))
                                .Click();

                    var geozones = this.driver.FindElements(By.CssSelector("table#table-zones tr:not(.header)"));
                    
                    var previousGeozone = geozones[0].FindElements(By.CssSelector("td"))[2].GetAttribute("textContent");

                    foreach (var geozone in geozones)
                    {
                        if (geozone.FindElements(By.CssSelector("td"))[2].FindElement(By.CssSelector("input")).GetDomProperty("type") == "hidden")
                        {
                            var currentGeozone = geozone.FindElements(By.CssSelector("td"))[2].GetAttribute("textContent");
                            Assert.True(
                                String.Compare(previousGeozone, currentGeozone) <= 0,
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
