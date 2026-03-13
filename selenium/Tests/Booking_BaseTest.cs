using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System.Xml.Linq;


public class Booking_BaseTest
{
    protected static IWebDriver driver;
    protected static WebDriverWait webDriverWait;
    protected static Waits waits;
    protected static ElementActions elements;

    protected const string BaseUrl = "https://www.booking.com/";

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        driver = WebDriverFactory.GetDriver(BrowserType.Chrome);
        driver.Manage().Window.Maximize();
        webDriverWait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
        waits = new Waits(driver, webDriverWait);
        elements = new ElementActions(driver, waits);
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
}
