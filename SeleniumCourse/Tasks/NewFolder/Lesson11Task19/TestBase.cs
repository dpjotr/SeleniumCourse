using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;

namespace SeleniumCourse.Tasks.NewFolder.Lesson11Task19
{
    class TestBase
    {
        protected WebDriver driver;
        protected WebDriverWait wait;
        protected DriverOptions options;
        protected int cartItemCounter;

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }  

        [TearDown]
        public void CleanUp()
        {
            driver.Quit();
            driver = null;
        }
    }
}
