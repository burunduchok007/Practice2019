using System;
using System.Collections.Generic;
using AutonationResources;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace VKTests
{
    [TestClass]
    public class VKTests
    {
        private IWebDriver Driver { get; set; }
                
        [TestMethod]
        public void LikeImage()
        {
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(20));
            PersonPage personPage = Login(wait);

            ImagePage imagePage = personPage.EnterImagePage();
            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[@id='pv_photo']/img")));
            Assert.IsTrue(imagePage.IsVisible());
            imagePage.Like(15);

        }

        [TestMethod]
        public void LikeVideo()
        {
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(20));
            PersonPage personPage = Login(wait);

            VideoPage videoPage = personPage.EnterVideoPage();
            wait.Until(ExpectedConditions.ElementIsVisible(By.Id("video_content_all")));
            Assert.IsTrue(videoPage.IsVisible());
            videoPage.Like(15);

            Refresh(personPage);
            Refresh(videoPage);


        }
                
        [TestMethod]
        public void LikeWallNote()
        {
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(20));
            PersonPage personPage = Login(wait);

            WallNotePage wallNotePage = personPage.EnterWallNotePage();
            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[@id='wall_search']")));
            Assert.IsTrue(wallNotePage.IsVisible());
            wallNotePage.Like(15);
        }

        [TestMethod]
        public void LikeEverythingWithILike()
        {
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(20));
            PersonPage personPage = Login(wait);

            List<ILike> listPage = new List<ILike>();

            ILike imagePage = personPage.EnterImagePage();
            listPage.Add(imagePage);
            personPage.GoToURL(personPage.UrlString);
            ILike videoPage = personPage.EnterVideoPage();
            listPage.Add(videoPage);
            personPage.GoToURL(personPage.UrlString);
            ILike wallNotePage = personPage.EnterWallNotePage();
            listPage.Add(wallNotePage);
            
            foreach(ILike c in listPage)
            {
                c.GoToURL(personPage);
                c.EnterPage(personPage);
                c.Wait();
                Assert.IsTrue(c.IsVisible());
                c.Like(5);
            }

        }

        static void OpenPage<T>(T Page) where T: ILike
        {
           
        }

         private void Refresh(BasePage basePage)
         {
            Driver.Navigate().Refresh();

         }
        private PersonPage Login(WebDriverWait wait)
        {
            PersonPage personPage = new PersonPage(Driver);
            personPage.Login = "";
            personPage.Password = "";
            personPage.UrlString = "https://vk.com/id136721861";
            personPage.GoToURL(personPage.UrlString);
            personPage.FillInformationAndSubmitLogin(personPage.Login, personPage.Password);
            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[@id='l_pr']/a/span/span[3]")));
            return personPage;
        }

        [TestInitialize]
        public void SetupForEverySingleTestMethod()
        {
            var factory = new WebDriverFactory();
            Driver = factory.Create(BrowserType.Chrome);
            

        }

        [TestCleanup]
        public void CleanUpAfterEveryTestMethod()
        {
            Driver.Close();
            Driver.Quit();
        }
    }
}
