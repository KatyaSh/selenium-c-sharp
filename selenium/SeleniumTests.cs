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


    [SetUp]
    public void SetUp()
    {
        driver = new ChromeDriver();
        driver?.Manage().Window.Maximize();
    }

    [TearDown]
    public void TearDown()
    {
        driver?.Close();
        driver?.Quit();
    }

    [Test]
    public void FirstTest()
    {
        driver?.Navigate().GoToUrl("https://www.saucedemo.com/");
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
}