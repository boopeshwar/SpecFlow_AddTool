using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Diagnostics;
using System.Threading;
using TechTalk.SpecFlow;

namespace SpecFlow_AddTool
{


    [Binding]
    public class AddToolSteps
    {
        public static IWebDriver driver;
        private String toolno;
        [Given(@"User Login to the ERP portal")]
        public void GivenUserLoginToTheERPPortal()
        {
            driver = new ChromeDriver();
            driver.Navigate().GoToUrl("http://localhost:64237/");
            driver.Manage().Window.Maximize();
            WaitUntil.FindElement(driver, By.Id("identifierId"), 60);
            driver.FindElement(By.Id("identifierId")).SendKeys("itmanager8589@gmail.com");
            driver.FindElement(By.Id("identifierNext")).Click();
            WaitUntil.FindElement(driver, By.XPath("//input[@type='password']"), 60);
            driver.FindElement(By.XPath("//input[@type='password']")).SendKeys("Envirocal@123");
            driver.FindElement(By.Id("passwordNext")).Click();
            Thread.Sleep(3000);
            Assert.AreEqual(driver.Title, "EnviroCal-DA | Dashboard");
            
        }

        [Given(@"Click on the AddTool page")]
        public void GivenClickOnTheAddToolPage()
        {
            WaitUntil.FindElement(driver, By.XPath("//a[@ui-sref='tool']"), 60);
            driver.FindElement(By.XPath("//a[@ui-sref='tool']")).Click();
            WaitUntil.FindElement(driver, By.XPath("//button[@ng-click='addTool()']"), 60);
            driver.FindElement(By.XPath("//button[@ng-click='addTool()']")).Click();
            
        }
        
        [When(@"User enter the tool details with (.*) and (.*)")]
        public void WhenUserEnterTheToolDetailsWithAnd(string p0, string p1)
        {
            toolno = toolnumgen();
            WaitUntil.FindElement(driver, By.XPath("//input[@ng-model='addToolPage.ToolNumber']"), 60);
            driver.FindElement(By.XPath("//input[@ng-model='addToolPage.ToolNumber']")).SendKeys(toolno);
            IWebElement tool_type = driver.FindElement(By.XPath("//select[@ng-model='addToolPage.ToolTypeId']"));
            SelectElement tooltype = new SelectElement(tool_type);
            tooltype.SelectByText(p0);
            IWebElement tool_size = driver.FindElement(By.XPath("//select[@ng-model='addToolPage.ToolSize']"));
            SelectElement toolSize = new SelectElement(tool_size);
            toolSize.SelectByText(p1);
            driver.FindElement(By.XPath("//input[@ng-model='addToolPage.CellType']")).SendKeys("Lithium");
            IWebElement tool_config = driver.FindElement(By.XPath("//select[@ng-model='addToolPage.ToolConfigurationId']"));
            SelectElement toolconfig = new SelectElement(tool_config);
            toolconfig.SelectByText("B.C");
            driver.FindElement(By.XPath("//textarea[@ng-model='addToolPage.ToolDescription']")).SendKeys("Tool #" + toolno + " created");
            IWebElement Submit = driver.FindElement(By.XPath("//button[@ng-click='SaveTool()']"));
            Actions actions = new Actions(driver);
            actions.MoveToElement(Submit).Click().Perform();
            Thread.Sleep(3000);
            driver.SwitchTo().Alert().Accept();
            WaitUntil.FindElement(driver, By.LinkText("Other Inputs"), 60);
            driver.FindElement(By.LinkText("Other Inputs")).Click();
        }
        
        [When(@"upload the pictures")]
        public void WhenUploadThePictures()
        {
            Thread.Sleep(5000);
            WaitUntil.FindElement(driver, By.XPath("//button[@accept='image/*'][contains(text(),'Select and Upload Files')]"), 60);
            driver.FindElement(By.XPath("//button[@accept='image/*'][contains(text(),'Select and Upload Files')]")).Click();
            Thread.Sleep(5000);
            Process process = Process.Start(@"C:\test\uploadimage.exe");
            int id = process.Id;
            Process tempProc = Process.GetProcessById(id);
            tempProc.WaitForExit();
            Thread.Sleep(10000);
            driver.SwitchTo().Alert().Accept();
        }
        
        [Then(@"the tool should be added")]
        public void ThenTheToolShouldBeAdded()
        {
            IWebElement logout = driver.FindElement(By.XPath("//a[@href='../api/logout']"));
            Actions mousehover = new Actions(driver);
            mousehover.MoveToElement(driver.FindElement(By.XPath("//li[@ng-controller='TopBarController']"))).MoveToElement(logout).Click().Build().Perform();
            driver.Quit();
        }
        
        public String toolnumgen()
        {
            Random rand = new Random();
            int n = rand.Next(1000) + 1;
            toolno = n.ToString();
            return toolno;
        }
    }
}