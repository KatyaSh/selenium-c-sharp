using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;

public class ElementUtils
{
    public static void ClickLinkByText(By xpath1, By xpath2, IWebDriver driver, By locator, int timeoutSeconds)
    {
        WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutSeconds));
        var element = wait.Until(
            SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(xpath1));

        ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", element);

        element.Click();

        WaitUtils.WaitForElementToBeClickable(driver, xpath2, timeoutSeconds);
    }

    public static void ClickElementByXPath(IWebDriver driver, By locator, int timeoutSeconds)
    {
        var element = WaitUtils.WaitForElementToBeClickable(driver, locator, timeoutSeconds);
        IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
        js.ExecuteScript("arguments[0].scrollIntoView(true);", element);
        element.Click();
    }

    public static bool IsElementSelected(IWebDriver driver, By locator)
    {
        return driver.FindElement(locator).Selected;
    }

    public static string GetElementText(IWebDriver driver, By locator)
    {
        return driver.FindElement(locator).Text;
    }

    public static SelectElement GetDropdown(IWebDriver driver, By dropdownLocator, int timeoutSeconds)
    {
        var dropdownElement = WaitUtils.WaitForElementIsVisible(driver, dropdownLocator, timeoutSeconds);
        return new SelectElement(dropdownElement);
    }
}

