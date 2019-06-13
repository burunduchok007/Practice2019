using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;

namespace VKTests
{
    internal class PersonPage: BasePage
    {
        internal PersonPage(IWebDriver driver): base(driver) {}

        private string _login;
        public string Login { get; set; }
        private string _password;
        public string Password { get; set; }
        private string _urlString;
        public string UrlString { get; set; }

        private string PageTitle => "Михаил Землянухин | ВКонтакте";

        private IWebElement EmailLog => Driver.FindElement(By.Id("quick_email"));

        private IWebElement PassLog => Driver.FindElement(By.Id("quick_pass"));

        private IWebElement LoginButtom => Driver.FindElement(By.Id("quick_login_button"));

        private IWebElement PageImage => Driver.FindElement(By.XPath("//*[@class='page_square_photo crisp_image']"));

        private IWebElement WallNote => Driver.FindElement(By.XPath("//*[@id='wall_tabs']/li[2]/a"));

        public PersonPage LoginAction()
        {
            PersonPage personPage = new PersonPage(Driver);
            Login = "z.t.1997@rambler.ru";
            Password = "TB1tsR8c15aS";
            UrlString = "https://vk.com/id136721861";
            GoToURL <PersonPage> (personPage, UrlString, PageTitle);
            FillInformationAndSubmitLogin(Login, Password);
            return personPage;
        }

        //internal PersonPage GoToURL(string url)
        //{
        //    PersonPage personPage = new PersonPage(Driver);
        //    Driver.Navigate().GoToUrl(url);
        //    Driver.Manage().Window.Maximize();
        //    Assert.IsTrue(IsVisible(), $"Source page was not visible. Expected=>{PageTitle}." +
        //        $"Actual=>{Driver.Title}");
        //    return personPage;
        //}

        public override bool IsVisible()
        {
            if (Driver.FindElement(By.XPath("//*[@id='quick_login']")).Displayed == true)
            {
                return true;
            }
            else return false;
        }

        public override void Wait()
        {
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(20));
            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[@id='top_profile_link']")));
        }

        internal void FillInformationAndSubmitLogin(string login, string password)
        {
            EmailLog.Clear();
            EmailLog.SendKeys(login);
            PassLog.Clear();
            PassLog.SendKeys(password);
            LoginButtom.Click();
            
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