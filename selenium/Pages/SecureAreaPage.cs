using OpenQA.Selenium;

public class SecureAreaPage
{
    private IWebDriver driver;
    private readonly By secureAreaPageHeader = By.XPath("//div[@class='example']");
    private readonly By logoutButton = By.XPath("//a[@class='button secondary radius']");

    public SecureAreaPage(IWebDriver driver)
    {
        this.driver = driver;
    }

    public bool IsLoginSuccessfull()
    {
        return WaitUtils.WaitForElementIsVisible(driver,secureAreaPageHeader,20).Displayed;
    }

    public void ClickLogoutButton() => driver.FindElement(logoutButton).Click();
}

