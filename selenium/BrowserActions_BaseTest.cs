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
        return webDriverWait.Until(
            SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(locator));
    }

    public IWebElement WaitForElementVisible(IWebDriver driver, By locator, int timeoutSeconds)
    {
        WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutSeconds));
        return wait.Until(ExpectedConditions.ElementIsVisible(locator));
    }

    public void ClickLinkByText(string linkText)
    {
        var element = webDriverWait.Until(
            SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(
                By.XPath($"//li/a[text()='{linkText}']")));

        ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", element);

        element.Click();

        WaitForElement(By.XPath($"//div/h3[contains(text(), '{linkText}')]"));
    }

    public void ClickElementByXPath(string xpath)
    {
        var element = WaitForElement(By.XPath(xpath));
        IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
        js.ExecuteScript("arguments[0].scrollIntoView(true);", element);
        element.Click();
    }
}
