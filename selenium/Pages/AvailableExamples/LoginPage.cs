using OpenQA.Selenium;

public class LoginPage
{
    private IWebDriver driver;

    private readonly By loginPageHeader = By.XPath($"//div/h2[contains(text(), 'Login Page')]");
    private readonly By usernameInput = By.Id("username");
    private readonly By passwordInput = By.Id("password");
    private readonly By loginButton = By.XPath("//button[@type='submit']");
    private readonly By invaliLogindMessage = By.Id("flash");
    private readonly By logoutButton = By.XPath("//a[@class='button secondary radius']");

    public LoginPage(IWebDriver driver)
    {
        this.driver = driver;
    }

    public void EnterUsername(string username) => driver.FindElement(usernameInput).SendKeys(username);

    public void EnterPassword(string password) => driver.FindElement(passwordInput).SendKeys(password);

    public void ClickLoginButton() => driver.FindElement(loginButton).Click();

    public void ClickLogoutButton() => driver.FindElement(logoutButton).Click();

    public bool IsUnsuccessLogin()
    {
        return WaitUtils.WaitForElementVisible(driver, invaliLogindMessage, 20).Displayed;
    }

    public bool IsLoginPageOpened()
    {
        return WaitUtils.WaitForElementVisible(driver, loginPageHeader, 20).Displayed;
    }

    public string GetInvalidLoginMessageText()
    {
        var text = WaitUtils.WaitForElementVisible(driver, invaliLogindMessage, 20).Text;
        return System.Text.RegularExpressions.Regex.Match(text, @"^.*?!").Value.Trim();
    }
}


