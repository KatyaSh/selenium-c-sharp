using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

[Parallelizable(ParallelScope.All)]
public class BrowserActions_BaseTest
{
    protected static IWebDriver driver;
    protected static WebDriverWait webDriverWait;

    protected const string BaseUrl = "http://the-internet.herokuapp.com/";

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        driver = WebDriverFactory.GetDriver(BrowserType.Chrome);
        webDriverWait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
    }

    [SetUp]
    public void SetUp()
    {
        driver.Navigate().GoToUrl(BaseUrl);
    }

    [OneTimeTearDown]
    public void OneTimeTearDown()
    {
        WebDriverFactory.QuitDriver();
    }

    public IWebElement WaitForElement(By locator)
    {
        return webDriverWait.Until(drv => drv.FindElement(locator));
    }

    public IWebElement WaitForElementVisible(IWebDriver driver, By locator, int timeoutSeconds)
    {
        WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutSeconds));
        return wait.Until(ExpectedConditions.ElementIsVisible(locator));
    }

}
