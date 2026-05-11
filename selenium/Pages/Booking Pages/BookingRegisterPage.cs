using OpenQA.Selenium;

namespace Booking
{
    public class BookingRegisterPage : BasePage
    {
        private readonly By _pageAnchor = By.CssSelector("input[type='email']");

        public BookingRegisterPage(IWebDriver driver, Waits waits, ElementActions elements)
            : base(driver, waits, elements)
        {
        }

        public void WaitUntilLoaded()
        {
            bool isUrlReady =
                _driver.Url.Contains("register", StringComparison.OrdinalIgnoreCase) ||
                _driver.Url.Contains("signup", StringComparison.OrdinalIgnoreCase);

            if (isUrlReady)
            {
                return;
            }
                        
                if (_waits.WaitForExists(_pageAnchor, 10))
                {
                    return;
                }
            
            throw new NoSuchElementException("Register page was not loaded.");
        }

        public bool IsDisplayed()
        {
            bool isUrlReady =
                _driver.Url.Contains("register", StringComparison.OrdinalIgnoreCase) ||
                _driver.Url.Contains("signup", StringComparison.OrdinalIgnoreCase);

            if (isUrlReady)
            {
                return true;
            }

             if (_waits.WaitForExists(_pageAnchor, 5))
                {
                    return true;
                }            

            return false;
        }
    }
}