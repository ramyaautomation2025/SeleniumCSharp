using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DotNetSelenium
{
    public static class Utils
    {
        public static void EnterText(this IWebElement locator, string input)
        {
            locator.Clear();
            locator.SendKeys(input);

        }
        public static void SelectByValue(this IWebElement locator, string input)
        {
            SelectElement selectElement = new SelectElement(locator);
            selectElement.SelectByValue(input);          

        }

        public static void SelectByText(this IWebElement locator, string input)
        {
            SelectElement selectElement = new SelectElement(locator);
            selectElement.SelectByText(input);

        }

    }
}
