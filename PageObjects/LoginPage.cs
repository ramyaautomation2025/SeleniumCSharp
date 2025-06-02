using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetSelenium.PageObjects
{
    public class LoginPage
    {
        private readonly IWebDriver driver;
        public LoginPage(IWebDriver driver)
        {
            this.driver = driver;
            
        }

        public void Login(string username, string password)
        {
            UserNameInput.EnterText(username);
            PasswordInput.EnterText(username);
            LoginLinkButton.Submit();
        }

        IWebElement LoginLinkButton => driver.FindElement(By.Name("login-button"));
        IWebElement UserNameInput => driver.FindElement(By.Name("user-name"));
        IWebElement PasswordInput => driver.FindElement(By.Name("password"));
    }
}
