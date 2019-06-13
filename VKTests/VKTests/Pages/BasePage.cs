using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;

namespace VKTests
{
    public abstract class BasePage
    {
        protected IWebDriver Driver { get; set; }

        public BasePage(IWebDriver driver)
        {
            Driver = driver;

        }

        public abstract void Wait();

        public abstract bool IsVisible();

        public T GoToURL<T> (T page, string url, string pageTitle) where T: class
        {
            Driver.Navigate().GoToUrl(url);
            Driver.Manage().Window.Maximize();
            Assert.IsTrue(IsVisible(), $"Source page was not visible. Expected=>{pageTitle}." +
                $"Actual=>{Driver.Title}");
            return page;
        }




    }
}