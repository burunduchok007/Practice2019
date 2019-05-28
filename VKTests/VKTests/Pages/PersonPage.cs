using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System;

namespace VKTests
{
    internal class PersonPage: BasePage
    {
        internal PersonPage(IWebDriver driver): base(driver) {}

        private string _login;
        public string Login { get { return _login; } set { _login = value; } }
        private string _password;
        public string Password { get { return _password; } set { _password = value; } }
        private string _urlString;
        public string UrlString { get { return _urlString; } set { _urlString = value; } }

        private string PageTitle => "Михаил Землянухин | ВКонтакте";

        private IWebElement EmailLog => Driver.FindElement(By.Id("quick_email"));

        private IWebElement PassLog => Driver.FindElement(By.Id("quick_pass"));

        private IWebElement LoigButtom => Driver.FindElement(By.Id("quick_login_button"));

        private IWebElement PageImage => Driver.FindElement(By.XPath("//*[@class='page_square_photo crisp_image']"));

        private IWebElement WallNote => Driver.FindElement(By.XPath("//*[@id='wall_tabs']/li[2]/a"));
        
        internal void GoTo(string url)
        {
            Driver.Navigate().GoToUrl(url);
            Assert.IsTrue(IsVisible(), $"Source page was not visible. Expected=>{PageTitle}." +
                $"Actual=>{Driver.Title}");
        }

        internal bool IsVisible() => Driver.FindElement(By.Id("quick_login_button")).Displayed;

        internal void FillInformationAndSubmitLogin(string login, string password)
        {
            EmailLog.Clear();
            EmailLog.SendKeys(login);
            PassLog.Clear();
            PassLog.SendKeys(password);
            LoigButtom.Click();
            
        }

        internal ImagePage EnterImagePage()
        {
            PageImage.Click();
            return new ImagePage(Driver);
        }


        internal VideoPage EnterVideoPage()
        {
            
            var videoPannel = Driver.FindElements(By.XPath("//*[contains(text(), 'Видеозаписи')] "));
            videoPannel[0].Click();

            return new VideoPage(Driver);
            
        }

        internal WallNotePage EnterWallNotePage()
        {
            var actions = new Actions(Driver);
            actions.DoubleClick(WallNote).Perform();

            return new WallNotePage(Driver);
        }
    }
}