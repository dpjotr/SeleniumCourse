using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace SeleniumCourse.Tasks
{
    class Lesson8Task14
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

            this.driver.FindElement(By.CssSelector("#box-apps-menu > li:nth-child(3) > a")).Click();
            this.driver.FindElement(By.CssSelector("table td#content > div > a.button")).Click();

            int amountOfExternalLinks = this.driver.FindElements(By.CssSelector("a > i.fa-external-link")).Count;

            for (int i = 0; i <amountOfExternalLinks; i++)
            {
                Assert.Multiple(() =>
                {
                    Assert.DoesNotThrow(
                        () => VerifyLink(this.driver.FindElements(By.CssSelector("a > i.fa-external-link"))[i]),
                        "Verification of link failed");
                });
            }
        }

        private void VerifyLink(IWebElement link)
        {
            string mainWindow = this.driver.CurrentWindowHandle;
            ReadOnlyCollection<string> oldWindows = this.driver.WindowHandles;

            link.Click();

            wait.Until((driver) =>this.driver.WindowHandles.Count > oldWindows.Count);
            string newWindow = this.driver.WindowHandles.Where(x => !oldWindows.Contains(x)).First();
            
            driver.SwitchTo().Window(newWindow);
            driver.Close();

            driver.SwitchTo().Window(mainWindow);
        }

        private Func<IWebDriver, string> newWindowWasOpened(ReadOnlyCollection<string> oldWindows)
        {
            throw new NotImplementedException();
        }

        [TearDown]
        public void CleanUp()
        {
            driver.Quit();
            driver = null;
        }
    }
}
