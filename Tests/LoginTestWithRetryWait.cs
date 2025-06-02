using DotNetSelenium.Models;
using DotNetSelenium.PageObjects;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using Polly;
using Polly.Retry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetSelenium.Tests
{
    public class LoginTestWithRetryWait
    {
        private IWebDriver _driver;
        WebDriverWait wait;

        [SetUp]
        public void Setup()
        {
            _driver = new ChromeDriver();
            _driver.Navigate().GoToUrl("https://www.saucedemo.com/");
            _driver.Manage().Window.Maximize();

        }
        [Test]
        [Category("Smoke")]      
        public void MultipleLoginDataTest()
        {
            var loginPagePO = new LoginPage(_driver);
            loginPagePO.Login("standard_user", "secret_sauce");
            Console.WriteLine("Navigation successful.");

        }

        //protected IWebElement RetryWebElement(By Element)
        //{
        //    var retryPolicy = new RetryPolicy();
        //    return retryPolicy.Execute(() => _driver.FindElement(Element));
        //}

        private RetryPolicy RetryPolicy()
        {
            //Retry wait with Polly
            var retryPolicy = Policy.Handle<NoSuchElementException>().Or<StaleElementReferenceException>()
                .WaitAndRetry(retryCount: 4, sleepDurationProvider: attempt => TimeSpan.FromSeconds(10),
                onRetry:(exception, timeSpan, retryCount, context) =>
                {
                    Console.WriteLine($"Retrying...(Attempt{retryCount} with Exception");
                });
            return retryPolicy;
        }
        [TearDown]
        public void TearDown()
        {

            _driver.Dispose();
        }
    }
}
