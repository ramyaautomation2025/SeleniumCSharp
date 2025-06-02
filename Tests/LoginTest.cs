using Allure.Commons;
using DotNetSelenium.Models;
using DotNetSelenium.PageObjects;
using NUnit.Allure.Attributes;
using NUnit.Allure.Core;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System.Net.Http.Json;
using System.Text.Json;

namespace DotNetSelenium.Tests
{
    [TestFixture]
    [AllureNUnit]
    public class Tests
    {
        
        private IWebDriver _driver;
        WebDriverWait wait;

        [SetUp]
        public void Setup()
        {
            _driver = new ChromeDriver();
            _driver.Navigate().GoToUrl("https://www.saucedemo.com/");
            _driver.Manage().Window.Maximize();
            //test

        }

        
        [Test(Description ="Sample website login")]
        [AllureSeverity(SeverityLevel.minor)]
        [AllureSuite("Smoke-Suite")]
        public void ShoppingCartTest()
        {          
            
            var loginPagePO = new LoginPage(_driver);
            loginPagePO.Login("standard_user", "secret_sauce");
            Console.WriteLine("Navigation successful.");



        }
        [Test]
        [Category("Smoke")]
        [TestCaseSource(nameof(LoginTestData))]
        public void MultipleLoginDataTest(LoginModel loginData)
        {
            var loginPagePO = new LoginPage(_driver);
            loginPagePO.Login(loginData.UserName, loginData.Password);
            Console.WriteLine("Navigation successful.");

        }

        public static IEnumerable<LoginModel> LoginTestData()
        {
            yield return new LoginModel()
        {
            UserName = "standard_user",
            Password = "secret_sauce",

            };

            yield return new LoginModel()
            {
                UserName = "locked_out_user",
                Password = "secret_sauce",

            };
            yield return new LoginModel()
            {
                UserName = "visual_user",
                Password = "secret_sauce",

            };

        }
        [Test]
        public void LoginUsingExternalData()
        {
            var loginPagePO = new LoginPage(_driver);
            var inputData = ReadJsonFile();
            foreach (var item in inputData)
            {
                loginPagePO.Login(item.UserName, item.Password);
            }
            
            Console.WriteLine("Navigation successful.");

        }

        private IList<LoginModel> ReadJsonFile()
        {
            string jsonPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources","Login.json");
            var jsonString = File.ReadAllText(jsonPath);
            if (string.IsNullOrWhiteSpace(jsonString))
            {
                throw new Exception("JSON file is empty or contains only whitespace.");
            }
            var loginModelData = JsonSerializer.Deserialize<List<LoginModel>>(jsonString);
            return loginModelData.ToList();
        }

        [TearDown]
        public void TearDown()
        {
          
            _driver.Dispose();
        }
    }
}