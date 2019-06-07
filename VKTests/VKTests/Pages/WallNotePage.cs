using System;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

namespace VKTests
{
    internal class WallNotePage: BasePage, ILike, IPrintContent<string>
    {
        public string CountElements { get; set; }

        internal WallNotePage(IWebDriver driver) : base(driver) { }       
        
        private IWebElement CountWallNotes => Driver.FindElement(By.XPath("//*[@id='fw_summary']"));

        public void EnterPage(PersonPage personPage)
        {
            personPage.EnterWallNotePage();
        }

        public void GoToURL(PersonPage personPage)
        {
            personPage.GoToURL(personPage.UrlString);
        }

        public bool IsVisible()
        {
            if (Driver.FindElement(By.XPath("//*[@id='wall_search']")).Displayed == true)
            {
                return true;
            }
            else return false;
        }
        
        public void Wait()
        {
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(20));
            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[@id='wall_search']")));
        }

        public void Like(int likeCount)
        {
            //int countIntWallNotes = Convert.ToInt32(CountWallNotes.Text);
            Actions actions = new Actions(Driver);
            
            for (int i =0; i<likeCount; i++)
            {
                var likeButtons = Driver.FindElements(By.XPath("//*[@class='_post_content']/div[2]/div/div[2]/div/div[1]/a[1]/div[1]"));
                               
                //actions.MoveToElement(likeButtons[i]).Perform();
                var countLikeBeforeElement = Driver.FindElements(By.XPath("//*[@class='_post_content']/div[2]/div/div[2]/div/div[1]/a[1]/div[3]"));
                string countLikeBeforeString = countLikeBeforeElement[i].Text.ToString();
                if(countLikeBeforeString == " ")
                {
                    likeButtons[i].Click();
                    Thread.Sleep(5000);
                }
                else
                {
                    int countLikeBefore = Convert.ToInt32(countLikeBeforeString);
                    likeButtons[i].Click();
                    Thread.Sleep(5000);
                    var countLikeAfterElement = Driver.FindElements(By.XPath("//*[@class='_post_content']/div[2]/div/div[2]/div/div[1]/a[1]/div[3]"));
                    string countLikeAfterSrting = countLikeAfterElement[i].Text.ToString();
                    int countLikeAfter = Convert.ToInt32(countLikeAfterSrting);
                    if (countLikeAfter != countLikeBefore + 1)
                    {
                        likeButtons[i].Click();
                        Thread.Sleep(5000);
                    }
                }

                scrollWithOffset(likeButtons[i],0,10);
            }

        }
        private void scrollWithOffset(IWebElement webElement, int x, int y)
        {

            String code = "window.scroll(" + (webElement.Location.X + x) + ","
            + (webElement.Location.Y + y) + ");";

            ((IJavaScriptExecutor)Driver).ExecuteScript(code, webElement, x, y);

        }
    }
}
