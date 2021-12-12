﻿using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;

namespace SeleniumCourse
{
    public class Lesson2Task1
    {
        private WebDriver driver;
        private WebDriverWait wait;

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
           
            //this.driver.FindElement(By.XPath("//div[. = 'I agree']")).Click();
            //this.driver.FindElement(By.Name("q")).SendKeys("webdriver\n");
            //this.wait.Until(c => c.FindElement(By.XPath("//*[contains(., 'WebDriver')]")));
        }

        [TearDown]
        public void CleanUp()
        {
            driver.Quit();
            driver = null;
        }
    }
}