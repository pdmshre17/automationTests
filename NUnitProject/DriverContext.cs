using System;
using OpenQA.Selenium;
namespace NUnitProject
{
    public static class DriverContext
    {
        private static IWebDriver _driver;

        public static IWebDriver driver
        {
            get
            {
                return _driver;
            }
            set
            {
                _driver = value;
            }
        }
        
    }
}
