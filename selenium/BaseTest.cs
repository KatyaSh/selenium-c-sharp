using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

[Parallelizable(ParallelScope.All)]
public class BaseTest
{
    protected static IWebDriver driver;
    protected static WebDriverWait webDriverWait;

    protected const string BaseUrl = "https://www.saucedemo.com/";

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

    public void Login(string username, string password)
    {
        WaitForElement(By.ClassName("login_logo"));
        driver.FindElement(By.Id("user-name")).SendKeys(username);
        driver.FindElement(By.Id("password")).SendKeys(password);
        driver.FindElement(By.Id("login-button")).Click();
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
