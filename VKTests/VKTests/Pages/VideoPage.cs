using System;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

namespace VKTests
{
    internal class VideoPage: BasePage, ILike, IPrintContent<int>
    {
        public int CountElements { get; set; }

        internal VideoPage(IWebDriver driver) : base(driver) { }
               
        private IWebElement CountVideoPannel => Driver.FindElement(By.XPath("//*[@class='like_button_count']"));

        private IWebElement ScrollBar => Driver.FindElement(By.XPath("//*[@class='mv_pl_scrollbar_inner scrollbar_inner']"));

        public void EnterPage(PersonPage personPage)
        {
            personPage.EnterVideoPage();
        }

        public void GoToURL(PersonPage personPage)
        {
            personPage.GoToURL(personPage.UrlString);
        }

        public bool IsVisible()
        {
            if (Driver.FindElement(By.Id("video_content_all")).Displayed == true)
            {
                return true;
            }
            else return false;
        } 

        public void Wait()
        {
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(20));
            wait.Until(ExpectedConditions.ElementIsVisible(By.Id("video_content_all")));
        }

        public void Like(int likeCount)
        {
            var videos = Driver.FindElements(By.XPath("//*[@class='video_item_title'] "));
            videos[0].Click();
            Thread.Sleep(5000);
            var videosBar = Driver.FindElements(By.XPath("//*[@class='mv_playlist_item_thumb']"));
            // var actions = new Actions(Driver);
            

            for(int i = 1; i<likeCount; i++)
            {
                var likeButton = Driver.FindElements(By.XPath("//*[@class='like_button_icon'] "));

                var countLikeBefore = CountVideoPannel.Text.ToString();
                if (countLikeBefore == "")
                {
                    likeButton[0].Click();
                    Thread.Sleep(5000);
                    //actions.DragAndDropToOffset(ScrollBar, 0, 4).Perform();
                    videosBar[i].Click();
                    Thread.Sleep(5000);
                }
                else
                {
                    string likeVideoBeforeString = "";
                    foreach (char x in countLikeBefore)
                    {
                        if (x != ' ')
                        {
                            likeVideoBeforeString += x;
                        }
                    }
                    int likeVideoBefore = Convert.ToInt32(likeVideoBeforeString);

                    
                    likeButton[0].Click();
                    Thread.Sleep(5000);

                    var countLikeAfter = CountVideoPannel.Text.ToString();
                    if (countLikeAfter == "")
                    {
                        likeButton[0].Click();
                        Thread.Sleep(5000);
                        //actions.DragAndDropToOffset(ScrollBar, 0, 4).Perform();
                        videosBar[i].Click();
                        Thread.Sleep(5000);
                    }
                    else
                    {
                        string likeVideoAfterString = "";
                        foreach (char x in countLikeAfter)
                        {
                            if (x != ' ')
                            {
                                likeVideoAfterString += x;
                            }
                        }
                        int likeVideoAfter = Convert.ToInt32(likeVideoAfterString);

                        if (likeVideoAfter == likeVideoBefore + 1)
                        {
                            //actions.DragAndDropToOffset(ScrollBar, 0, 4).Perform();
                            videosBar[i].Click();
                            Thread.Sleep(5000);

                        }
                        else
                        {
                            likeButton[0].Click();
                            Thread.Sleep(5000);
                            // actions.DragAndDropToOffset(ScrollBar, 0, 4).Perform();
                            videosBar[i].Click();
                            Thread.Sleep(5000);
                        }
                    }
                    
                    
                }
                

            }
        }

        //internal void LikeVideo()
        //{
        //    var videos = Driver.FindElements(By.XPath("//*[@class='video_item_title'] "));
        //    int countInt = videos.Count;
        //    //var counts = Driver.FindElements(By.XPath("//*[@class='ui_tab_count'] "));
        //    //string countText = counts[0].Text;
        //    //int countInt = Convert.ToInt32(countText);

        //    for (int i = 0; i <= countInt; i++)
        //    {
        //        videos[i].Click();
        //        Thread.Sleep(5000);
        //        var likeButton = Driver.FindElements(By.XPath("//*[@class='like_button_icon'] "));

        //        var countLikeBefore = CountVideoPannel.Text.ToString();
        //        if (countLikeBefore == "")
        //        {
        //            likeButton[0].Click();
        //            Thread.Sleep(5000);
        //            Driver.Navigate().Back();
        //            Thread.Sleep(5000);
        //        }
        //        else
        //        {
        //            string likeVideoBeforeString = "";
        //            foreach (char x in countLikeBefore)
        //            {
        //                if (x != ' ')
        //                {
        //                    likeVideoBeforeString += x;
        //                }
        //            }
        //            int likeVideoBefore = Convert.ToInt32(likeVideoBeforeString);


        //            likeButton[0].Click();
        //            Thread.Sleep(5000);
        //            var countLikeAfter = CountVideoPannel.Text.ToString();
        //            string likeVideoAfterString = "";
        //            foreach (char x in countLikeAfter)
        //            {
        //                if (x != ' ')
        //                {
        //                    likeVideoAfterString += x;
        //                }
        //            }
        //            int likeVideoAfter = Convert.ToInt32(likeVideoAfterString);

        //            if (likeVideoAfter == likeVideoBefore + 1)
        //            {
        //                Driver.Navigate().Back();
        //                Thread.Sleep(5000);
        //            }
        //            else
        //            {
        //                likeButton[0].Click();
        //                Thread.Sleep(5000);
        //                Driver.Navigate().Back();
        //                Thread.Sleep(5000);
        //            }
        //        }


        //    }
        //}
    }
}