using OpenQA.Selenium;

public class AvailableExamplesPage
{
    private IWebDriver driver;
    public AvailableExamplesPage(IWebDriver driver)
    {
        this.driver = driver;
    }

    public void OpenExamplePage(string linkText)
    {
        var linkLocator = By.XPath($"//a[text()='{linkText}']");
        var element = WaitUtils.WaitForElementIsVisible(driver, linkLocator, 20);
        ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", element);
        element.Click();
    }
}

