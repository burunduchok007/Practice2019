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
            PersonPage personPage = new PersonPage(Driver);
            personPage.LoginAction();
            personPage.Wait();
            ImagePage imagePage = personPage.EnterImagePage();
            imagePage.Wait();
            Assert.IsTrue(imagePage.IsVisible());
            imagePage.ClickLike(15);

        }

        [TestMethod]
        public void LikeVideo()
        {
            PersonPage personPage = new PersonPage(Driver);
            personPage.LoginAction();
            personPage.Wait();
            VideoPage videoPage = personPage.EnterVideoPage();
            videoPage.Wait();
            Assert.IsTrue(videoPage.IsVisible());
            videoPage.ClickLike(15);

            Refresh(personPage);
            Refresh(videoPage);


        }
                
        [TestMethod]
        public void LikeWallNote()
        {
            PersonPage personPage = new PersonPage(Driver);
            personPage.LoginAction();
            personPage.Wait();
            WallNotePage wallNotePage = personPage.EnterWallNotePage();
            wallNotePage.Wait();
            Assert.IsTrue(wallNotePage.IsVisible());
            wallNotePage.ClickLike(15);
        }

        //[TestMethod]
        //public void LikeEverythingWithILike()
        //{
        //    WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(20));
        //    PersonPage personPage = Login(wait);

        //    List<IClickableLike> listPage = new List<IClickableLike>();

        //    IClickableLike imagePage = personPage.EnterImagePage();
        //    listPage.Add(imagePage);
        //    personPage.GoToURL(personPage.UrlString);
        //    IClickableLike videoPage = personPage.EnterVideoPage();
        //    listPage.Add(videoPage);
        //    personPage.GoToURL(personPage.UrlString);
        //    IClickableLike wallNotePage = personPage.EnterWallNotePage();
        //    listPage.Add(wallNotePage);
            
        //    foreach(IClickableLike c in listPage)
        //    {
        //        c.GoToURL(personPage);
        //        c.EnterPage(personPage);
        //        c.Wait();
        //        Assert.IsTrue(c.IsVisible());
        //        c.ClickLike(5);
        //    }

        //}

        static void OpenPage<T>(T Page) where T: IClickableLike
        {
           
        }

         private void Refresh(BasePage basePage)
         {
            Driver.Navigate().Refresh();

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
