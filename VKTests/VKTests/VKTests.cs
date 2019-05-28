using System;
using AutonationResources;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace VKTests
{
    [TestClass]
    public class VKTests
    {
        public IWebDriver Driver { get; private set; }
                
        [TestMethod]
        public void TCID1()
        {
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(20));
            PersonPage personPage = Login(wait);

            ImagePage imagePage = personPage.EnterImagePage();
            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[@id='pv_photo']/img")));
            Assert.IsTrue(imagePage.IsVisible);
            imagePage.LikePhotos();

        }

        [TestMethod]
        public void TCID2()
        {
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(20));
            PersonPage personPage = Login(wait);

            VideoPage videoPage = personPage.EnterVideoPage();
            wait.Until(ExpectedConditions.ElementIsVisible(By.Id("video_content_all")));
            Assert.IsTrue(videoPage.IsVisible);
            videoPage.LikeVideo();


        }
                
        [TestMethod]
        public void TCID3()
        {
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(20));
            PersonPage personPage = Login(wait);

            WallNotePage wallNotePage = personPage.EnterWallNotePage();
            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[@id='wall_search']")));
            Assert.IsTrue(wallNotePage.IsVisible);
            wallNotePage.LikeWallNotes();
        }

        private PersonPage Login(WebDriverWait wait)
        {
            PersonPage personPage = new PersonPage(Driver);
            personPage.Login = "z.t.1997@rambler.ru";
            personPage.Password = "Cd45tRzA12e332";
            personPage.UrlString = "https://vk.com/id136721861";
            personPage.GoTo(personPage.UrlString);
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
