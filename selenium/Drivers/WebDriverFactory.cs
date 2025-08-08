using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium;

public class WebDriverFactory
{
    [ThreadStatic]
    private static IWebDriver _driver;

    public static IWebDriver GetDriver(BrowserType browser)
    {
        if (_driver == null)
        {
            _driver = CreateDriverInstance(browser);
        }
        return _driver;
    }

    private static IWebDriver CreateDriverInstance(BrowserType browser)
    {
        switch (browser)
        {
            case BrowserType.Chrome:
                var chromeOptions = new ChromeOptions();
                chromeOptions.AddArgument("--disable-infobars");
                chromeOptions.AddArgument("--disable-notifications");
                chromeOptions.AddUserProfilePreference("credentials_enable_service", false);
                chromeOptions.AddUserProfilePreference("profile.password_manager_enabled", false);
                chromeOptions.AddArgument("--incognito");
                return new ChromeDriver(chromeOptions);
            case BrowserType.Edge:
                return new EdgeDriver();
            default:
                throw new NotSupportedException($"Browser '{browser}' not supported.");
        }
    }

    public static void QuitDriver()
    {
        _driver?.Quit();
        _driver = null;
    }
}
