using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
public class BaseTest
{
    protected IWebDriver driver;
    protected WebDriverWait webDriverWait;
    protected const string BaseUrl = "https://www.saucedemo.com/";

    [OneTimeSetUp]
    public void OneTimeSetup()
    {
        var options = new ChromeOptions();
        options.AddArgument("--disable-infobars");
        options.AddArgument("--disable-notifications");
        options.AddUserProfilePreference("credentials_enable_service", false);
        options.AddUserProfilePreference("profile.password_manager_enabled", false);
        options.AddArgument("--incognito");

        driver = new ChromeDriver(options);
        driver.Manage().Window.Maximize();
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
        driver?.Close();
        driver?.Quit();
    }

    public IWebElement WaitForElement(By locator)
    {
        return webDriverWait?.Until(drv => drv.FindElement(by: locator))!;
    }

    public void Login(string username, string password)
    {
        WaitForElement(By.ClassName("login_logo"));
        var usernameInput = driver.FindElement(By.Id("user-name"));
        usernameInput.SendKeys(username);
        var passwordInput = driver.FindElement(By.Id("password"));
        passwordInput.SendKeys(password);
        var loginButton = driver.FindElement(By.Id("login-button"));
        loginButton.Click();
    }

    public void SuccesfullLogin(string username, string password)
    {
        Login(username, password);
        WaitForElement(By.ClassName("title"));
        var actualUrl = driver.Url;
        var expectedUrl = "https://www.saucedemo.com/inventory.html";

        Assert.That(actualUrl, Is.EqualTo(expectedUrl), "Wrong URL for main page");
    }

    public void ClearCartBeforeTest()
    {
        driver.Navigate().GoToUrl("https://www.saucedemo.com/inventory.html");

        var cartLink = driver.FindElement(By.CssSelector("[data-test='shopping-cart-link']"));
        cartLink.Click();

        driver.FindElements(By.XPath("//button[starts-with(@id, 'remove-')]"))
       .ToList()
       .ForEach(btn => btn.Click());
    }
}
