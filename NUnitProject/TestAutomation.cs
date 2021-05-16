using System;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace NUnitProject
{
    public class TestAutomation
    {
        //Starts Reporter
        public ExtentReports extent = new ExtentReports();

        public static string path = System.Reflection.Assembly.GetCallingAssembly().CodeBase;
        public static string actualPath = path.Substring(0, path.LastIndexOf("bin"));
        public static string prjPath = new Uri(actualPath).LocalPath;
        public static string reportPath = prjPath + "Reports\\MyReport.html";
        ExtentHtmlReporter htmlReport = new ExtentHtmlReporter(reportPath);
        public ExtentTest extentTest;

        Base baseClass = new Base();

        [SetUp]
        public void Initialize()
        {
            System.Environment.SetEnvironmentVariable("webdriver.chrome.driver", "/Users/sohamchaudhary/Downloads/chromedriver");
            DriverContext.driver = new ChromeDriver();
            DriverContext.driver.Navigate().GoToUrl("http://automationpractice.com/");
            extent.AddSystemInfo("Environment", "QA");
            extent.AddSystemInfo("User Name", "Padmashree Bagal");
            extent.AttachReporter(htmlReport);
            DriverContext.driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            
        }

        [Test]
        public void Test_1()
        {

            extentTest = extent.CreateTest("TestOne", "Add items to Cart and purchase them");
            CommonFunctions commFun = new CommonFunctions(); 
            commFun.LoginApplication("Padma@test.com", "FujitsuTest");
            commFun.Add_Tshirt();
            extentTest.Log(Status.Pass, "Tshirt added to cart");
            commFun.Add_Blouse();
            commFun.Verify_CartItems();
            commFun.ConfirmOrder();
            extentTest.Log(Status.Pass, "Confirmed order & Payment Done");

        }

        [Test]
        public void Test_2()
        {
            
            extentTest = extent.CreateTest("TestTwo", "Add a message for order & verify the same");
            CommonFunctions commFun = new CommonFunctions();

            commFun.LoginApplication("padma@test.com", "FujitsuTest");
            commFun.NavigateToOrdersAddMessage();
            commFun.verifySuccessAlertAndMessage();
            extentTest.Log(Status.Pass, "Verified message under the Message Tab");
            commFun.Logout();
        }

        [Test]
        public void Test_3()
        {
            extentTest = extent.CreateTest("TestThree", "Verify Product Colour is Blue");
            CommonFunctions commFun = new CommonFunctions();

            commFun.LoginApplication("padma@test.com", "FujitsuTest");
            commFun.GoToOrders();

            try
            {

                DriverContext.driver.FindElement(By.XPath("//tr[@class='item']//td[2]//label[contains(text(),'Chiffon')][contains(text(),'Blue')]")).Displayed.Equals(true);
                extentTest.Log(Status.Pass, "Product Colour is Blue");
            }
            catch (Exception ex)
            {
                string path = baseClass.TakeScreenshot();

                extentTest.Log(Status.Fail, "Product Colour should have been Blue");
                extentTest.AddScreenCaptureFromPath(path);

            }
            commFun.Logout();

        }

        [TearDown]
        public void closeBrowser()
        {            
            extent.Flush();
            DriverContext.driver.Quit();
        }

    
    }
}
