using System;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using OpenQA.Selenium;

namespace NUnitProject
{
    public class Base
    { 
        public string TakeScreenshot()
        {
            string path1 = AppDomain.CurrentDomain.BaseDirectory.Replace("\\bin\\Debug", "");
            string path = path1 + "Screenshot\\" +  ".png";
            Screenshot screenshot = ((ITakesScreenshot)DriverContext.driver).GetScreenshot();
            screenshot.SaveAsFile(path, ScreenshotImageFormat.Png);
            return path;
        }
    }
}
    