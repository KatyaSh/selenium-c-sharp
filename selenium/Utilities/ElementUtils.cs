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

        WaitUtils.WaitForElement(driver, xpath2, timeoutSeconds);
    }

    public static void ClickElementByXPath(IWebDriver driver, By locator, int timeoutSeconds)
    {
        var element = WaitUtils.WaitForElement(driver, locator, timeoutSeconds);
        IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
        js.ExecuteScript("arguments[0].scrollIntoView(true);", element);
        element.Click();
    }

    public static bool IsElementSelected(IWebDriver driver, By locator)
    {
        return driver.FindElement(locator).Selected;
    }

    public static string IsElementText(IWebDriver driver, By locator)
    {
        return driver.FindElement(locator).Text;
    }

    public static SelectElement GetDropdown(IWebDriver driver, By dropdownLocator, int timeoutSeconds)
    {
        var dropdownElement = WaitUtils.WaitForElementVisible(driver, dropdownLocator, timeoutSeconds);
        return new SelectElement(dropdownElement);
    }

}

