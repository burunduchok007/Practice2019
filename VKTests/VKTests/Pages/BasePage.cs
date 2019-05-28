using OpenQA.Selenium;

namespace VKTests
{
    public class BasePage
    {
        protected IWebDriver Driver { get; set; }

        public BasePage (IWebDriver driver)
        {
            Driver = driver;
        }
    }
}