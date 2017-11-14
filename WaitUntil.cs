using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpecFlow_AddTool
{
    static class WaitUntil
    {
        public static void FindElement(this IWebDriver driver, By by, int timeoutInSeconds)
        {
            WebDriverWait wait = new WebDriverWait(driver, System.TimeSpan.FromSeconds(40));
            wait.Until(ExpectedConditions.ElementIsVisible(by));
        }
    }
}
