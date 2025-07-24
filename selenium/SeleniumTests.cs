using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
public class SeleniumTestExample
{
    private IWebDriver driver;
    private WebDriverWait webDriverWait;

    private IWebElement WaitForElement(By locator)
    {
        return webDriverWait?.Until(drv => drv.FindElement(by: locator))!;
    }


    [OneTimeSetUp]
    public void OneTimeSetup()
    {
        driver = new ChromeDriver();
        driver?.Manage().Window.Maximize();
    }

    [SetUp]
    public void SetUp()
    {
        driver.Navigate().GoToUrl("https://www.saucedemo.com/");
    }

    [OneTimeTearDown]
    public void OneTimeTearDown()
    {
        driver?.Close();
        driver?.Quit();
    }

    [Test]
    public void FirstTest()
    {
        var loginPageTitle = WaitForElement(By.ClassName("login_logo"));
        var usernameInput = driver.FindElement(By.Id("user-name"));
        usernameInput.SendKeys("standard_user");
        var passwordInput = driver.FindElement(By.Id("password"));
        passwordInput.SendKeys("secret_sauce");
        var loginButton = driver.FindElement(By.Id("login-button"));
        loginButton.Click();
        var mainPageTitle = WaitForElement(By.ClassName("title"));
        var actualUrl = driver.Url;
        var expectedUrl = "https://www.saucedemo.com/inventory.html";

        Assert.That(actualUrl, Is.EqualTo(expectedUrl), "Wrong URL for main page");

        var productDetails = driver.FindElements(By.CssSelector("[data-test='inventory-item-desc']"));
        var firtsProductText = productDetails[0].Text;
        var productElementsTitles = driver.FindElements(By.XPath("//div[@data-test='inventory-item-name']"));
        productElementsTitles[0].Click();
        var backButton = WaitForElement(By.Id("back-to-products"));
        var description = driver.FindElement(By.XPath("//div[@data-test='inventory-item-desc']")).Text;

        Assert.That(description, Is.EqualTo(firtsProductText), "Wrong product details text is on product page");
    }

    [Test]
    public void UnsuccessfulLoginEmptyCredentials()
    {
        var loginPageTitle = WaitForElement(By.ClassName("login_logo"));
        var usernameInput = driver.FindElement(By.Id("user-name"));
        var passwordInput = driver.FindElement(By.Id("password"));
        var loginButton = driver.FindElement(By.Id("login-button"));
        loginButton.Click();
        var errorMessage = driver.FindElement(By.CssSelector("[data-test='error']"));

        Assert.That(errorMessage.Displayed, Is.True, "Error message is not displayed");

        var actualErrorText = errorMessage.Text;
        var expectedErrorText = "Epic sadface: Username is required";

        Assert.That(actualErrorText, Is.EqualTo(expectedErrorText), "Wrong error message text");
    }

    [Test]
    public void UnsuccessfulLoginInvalidCredentials()
    {
        var loginPageTitle = WaitForElement(By.ClassName("login_logo"));
        var usernameInput = driver.FindElement(By.Id("user-name"));
        usernameInput.SendKeys("invalid_user");
        var passwordInput = driver.FindElement(By.Id("password"));
        passwordInput.SendKeys("invalid_password");
        var loginButton = driver.FindElement(By.Id("login-button"));
        loginButton.Click();
        var errorMessage = driver.FindElement(By.CssSelector("[data-test='error']"));

        Assert.That(errorMessage.Displayed, Is.True, "Error message is not displayed");

        var actualErrorText = errorMessage.Text;
        var expectedErrorText = "Epic sadface: Username and password do not match any user in this service";

        Assert.That(actualErrorText, Is.EqualTo(expectedErrorText), "Wrong error message text");
    }
}