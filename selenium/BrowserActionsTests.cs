using NUnit.Framework;
using OpenQA.Selenium;
using System.Drawing;

class BrowserActionsTests : BrowserActions_BaseTest
{
    [Test]
    public void OpenNewWindowAndHandleTabs()
    {
        var currentWindowHandle = driver.CurrentWindowHandle;
        var expectedTextNewWindow = "New Window";

        var multipleWindowsLink = webDriverWait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//li/a[text()='Multiple Windows']")));
        IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
        js.ExecuteScript("arguments[0].scrollIntoView(true);", multipleWindowsLink);

        multipleWindowsLink.Click();

        WaitForElement(By.XPath("//div/h3[contains(text(), 'Opening a new window')]"));

        var clickHereLink = driver.FindElement(By.XPath("//div/a[contains(text(), 'Click Here')]"));
        clickHereLink.Click();

        foreach (string windowHandle in driver.WindowHandles)
        {
            if (windowHandle != currentWindowHandle)
            {
                driver.SwitchTo().Window(windowHandle);

                var newWindowsContent = driver.FindElement(By.XPath("//div/h3[contains(text(), 'New Window')]"));

                Assert.That(newWindowsContent.Displayed, Is.True, "Content is not displayed");

                var actualContentText = newWindowsContent.Text;

                Assert.That(actualContentText, Is.EqualTo(expectedTextNewWindow), "Wrong content text");

                driver.Close();
            }
        }
    }

    [Test]
    public void NavigateBackAndForward()
    {
        var expectedSuccessfullMessage = "You logged into a secure area!";

        var formAuthenticatuionLink = webDriverWait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//li/a[text()='Form Authentication']")));
        
        IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
        js.ExecuteScript("arguments[0].scrollIntoView(true);", formAuthenticatuionLink);

        formAuthenticatuionLink.Click();

        WaitForElement(By.XPath("//div/h2[contains(text(), 'Login Page')]"));

        driver.FindElement(By.Id("username")).SendKeys("tomsmith");
        driver.FindElement(By.Id("password")).SendKeys("SuperSecretPassword!");
        driver.FindElement(By.CssSelector("[type = 'submit']")).Click();

        var flashMessage = webDriverWait.Until(
    SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.Id("flash")));

        Assert.That(flashMessage.Text.Contains(expectedSuccessfullMessage), $"Expected message to contain '{expectedSuccessfullMessage}', but was '{flashMessage}'");

        driver.Navigate().Back();

        var loginForm = webDriverWait.Until(
     SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.Id("login")));

        Assert.That(loginForm.Displayed, Is.True, "Login form is NOT visible.");

        driver.Navigate().Forward();

        flashMessage = driver.FindElement(By.XPath("//div[@id='flash']"));

        Assert.That(flashMessage.Text.Contains(expectedSuccessfullMessage), $"Expected message to contain '{expectedSuccessfullMessage}', but was '{flashMessage}'");
    }

    [Test]
    public void NavigateToURLAndRefresh()
    {
        var dynamicLoadingLink = webDriverWait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//li/a[text()='Dynamic Loading']")));
        
        IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
        js.ExecuteScript("arguments[0].scrollIntoView(true);", dynamicLoadingLink);
        
        dynamicLoadingLink.Click();

        WaitForElement(By.XPath("//div/h3[contains(text(), 'Dynamically Loaded Page Elements')]"));

        var example1Link = driver.FindElement(By.XPath("//div/a[text()='Example 1: Element on page that is hidden']"));
        example1Link.Click();

        var startButton = driver.FindElement(By.XPath("//button[text()='Start']"));
        startButton.Click();

        var loadingElement = WaitForElementVisible(driver, By.Id("loading"), 10);
        Assert.That(loadingElement.Displayed, Is.True, "Loading element should be visible on the page");

        driver.Navigate().GoToUrl(BaseUrl);
        var currentUrl = driver.Url;

        Assert.That(currentUrl, Is.EqualTo("https://the-internet.herokuapp.com/"), "User is not on the homepage");
    }

    [Test]
    public void MaximizeWindowAndChangeWindowSize()
    {
        var largeDeepDOMLink = webDriverWait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//li/a[text()='Large & Deep DOM']")));

        IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
        js.ExecuteScript("arguments[0].scrollIntoView(true);", largeDeepDOMLink);

        largeDeepDOMLink.Click();

        driver.Manage().Window.Maximize();

        var lastCellBy = By.XPath("//table//tr[last()]/td[last()]");
        var lastCell = webDriverWait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(lastCellBy));

        js.ExecuteScript("arguments[0].scrollIntoView(true);", lastCell);

        var lastValue = lastCell.Text;
        Assert.That(lastValue, Is.EqualTo("50.50"), $"Unexpected last value: '{lastValue}'");

        var newWindowSize = new Size(1024, 720);
        driver.Manage().Window.Size = newWindowSize;

        var actual = driver.Manage().Window.Size;

        Assert.Multiple(() =>
        {
            Assert.That(actual.Width, Is.InRange(newWindowSize.Width - 2, newWindowSize.Width + 2), "Window width not applied");
            Assert.That(actual.Height, Is.InRange(newWindowSize.Height - 2, newWindowSize.Height + 2), "Window height not applied");
        });
    }

    [Test]
    public void HeadlessMode()
    {
        driver = WebDriverFactory.GetDriver(BrowserType.ChromeHeadless);
        driver.Navigate().GoToUrl(BaseUrl);

        var checkboxes = webDriverWait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//li/a[text()='Checkboxes']")));
        
        IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
        js.ExecuteScript("arguments[0].scrollIntoView(true);", checkboxes);

        checkboxes.Click();

        By checkbox1 = By.CssSelector("#checkboxes input[type='checkbox']:nth-of-type(1)");
        By checkbox2 = By.CssSelector("#checkboxes input[type='checkbox']:nth-of-type(2)");

        var cb1 = webDriverWait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(checkbox1));
        var cb2 = webDriverWait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(checkbox2));

        Assert.Multiple(() =>
        {
            Assert.That(cb1.Enabled, Is.True, "Checkbox 1 is not enabled");
            Assert.That(cb2.Enabled, Is.True, "Checkbox 2 is not enabled");
        });
    }
}

