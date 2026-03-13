using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using SeleniumExtras.WaitHelpers;

public class Waits
{
    private readonly IWebDriver _driver;
    private readonly int _defaultTimeoutSeconds;
    private readonly WebDriverWait _wait;

    public Waits(IWebDriver driver, WebDriverWait wait)
    {
        _driver = driver;        
        _wait = wait;
    }

    private void SetTimeout(int timeoutSeconds)
    {
        _wait.Timeout = TimeSpan.FromSeconds(timeoutSeconds);
    }

    public IWebElement WaitForClickable(By locator, int timeoutSeconds)
    {
        SetTimeout(timeoutSeconds);
        return _wait.Until(ExpectedConditions.ElementToBeClickable(locator));
    }

    public IWebElement WaitForVisible(By locator, int timeoutSeconds)
    {
        SetTimeout(timeoutSeconds);
        return _wait.Until(ExpectedConditions.ElementIsVisible(locator));
    }

    public void WaitForUrlContains(string value, int timeoutSeconds)
    {
        SetTimeout(timeoutSeconds);
        _wait.Until(d => d.Url.Contains(value, StringComparison.OrdinalIgnoreCase));
    }

    public bool WaitForExists(By locator, int timeoutSeconds)
    {
        try
        {
            SetTimeout(timeoutSeconds);
            return _wait.Until(d => d.FindElements(locator).Count > 0);
        }
        catch
        {
            return false;
        }
    }
}