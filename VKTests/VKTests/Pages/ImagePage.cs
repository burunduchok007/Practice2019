using System;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

namespace VKTests
{
    internal class ImagePage : BasePage, IClickableLike, IPrintContent<int>
    {
        public int CountElements {get;set;}
        
        internal ImagePage(IWebDriver driver):base(driver) { }                

        private IWebElement LikeButton => Driver.FindElement(By.XPath("//*[@class='like_button_icon']"));

        private IWebElement ImageClick => Driver.FindElement(By.XPath("//*[@id='pv_photo']/img"));

        private IWebElement CountLikesPannel => Driver.FindElement(By.XPath("//*[@id='pv_narrow']/div[1]/div[1]/div/div/div[1]/div[3]/div/div[1]/a[1]/div[3]"));

        private IWebElement CountImagePannel => Driver.FindElement(By.XPath("//*[@class='pv_counter']"));
               
        private IWebElement ImagePannel => Driver.FindElement(By.Id("layer"));

        
        public override bool IsVisible()
        {
            if (Driver.FindElement(By.Id("side_bar_inner")).Displayed == true)
            {
                return true;
            }
            else return false;
        }
                

        public override void Wait()
        {
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(20));
            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[@id='pv_photo']/img")));
        } 

        public void ClickLike(int likeCount)
        {            
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(20));
            wait.Until(ExpectedConditions.ElementIsVisible(By.Id("layer")));

            Assert.IsTrue(ImagePannel.Displayed);
                      
            var countImage = CountImagePannel.Text.Split();
            //int totalCountImage = Convert.ToInt32(countImage[2]);
            int currentCountImage = Convert.ToInt32(countImage[0]);

            while (currentCountImage < likeCount)
            {
                var countLikesBefore = CountLikesPannel.Text.Split();
                int totalCountLikesBefore = Convert.ToInt32(countLikesBefore[0]);

                LikeButton.Click();
                Thread.Sleep(5000);
                
                var countLikesAfter = CountLikesPannel.Text.Split();
                int totalCountLikesAfter = Convert.ToInt32(countLikesAfter[0]);

                if(totalCountLikesAfter == totalCountLikesBefore+1)
                {
                    ImageClick.Click();
                    Thread.Sleep(5000);
                }
                else
                {
                    LikeButton.Click();
                    Thread.Sleep(5000);
                    ImageClick.Click();
                    Thread.Sleep(10000);
                }

                countImage = CountImagePannel.Text.Split();
                currentCountImage = Convert.ToInt32(countImage[0]);
            }
                        
                                                            
        }

               

    }
}