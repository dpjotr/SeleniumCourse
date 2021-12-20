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
    class Lesson2Task2
    {
        private WebDriver driver;
        private WebDriverWait wait;
        private DriverOptions options;
        
        [SetUp]
        public void Setup()
        {
            driver = new FirefoxDriver();
            options = new FirefoxOptions();
            ((FirefoxOptions)options).BrowserExecutableLocation = @"C:\Program Files\Firefox Developer Edition\firefox.exe";
            Console.WriteLine(driver.Capabilities);
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

        [Test]
        public void Test()
        {
            this.driver.Url = "http://localhost/litecart/admin/login.php";
            
            this.driver.FindElement(By.Name("username")).SendKeys("admin");
            this.driver.FindElement(By.Name("password")).SendKeys("admin");
            this.driver.FindElement(By.Name("login")).Click();
        }

        [TearDown]
        public void CleanUp()
        {
            driver.Quit();
            driver = null;
        }
    }
}
