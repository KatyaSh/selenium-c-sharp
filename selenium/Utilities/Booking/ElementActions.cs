using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

public class ElementActions
{
    private readonly IWebDriver _driver;
    private readonly Waits _waits;

    public ElementActions(IWebDriver driver, Waits waits)
    {
        _driver = driver;
        _waits = waits;
    }

    public void Click(By locator, int timeoutSeconds)
    {
        _waits.WaitForClickable(locator, timeoutSeconds).Click();
    }

    public bool TryClick(By locator, int timeoutSeconds = 3)
    {
        try
        {
            Click(locator, timeoutSeconds);
            return true;
        }
        catch
        {
            return false;
        }
    }

    public void Type(By locator, string text, int timeoutSeconds, bool clear = true)
    {
        var el = _waits.WaitForVisible(locator, timeoutSeconds);
        if (clear) el.Clear();
        el.SendKeys(text);
    }

    public string GetText(By locator, int timeoutSeconds)
    {
        return _waits.WaitForVisible(locator, timeoutSeconds).Text;
    }

    public IWebElement GetVisibleElement(By locator, int timeoutSeconds)
    {
        return _waits.WaitForVisible(locator, timeoutSeconds);
    }

    public bool Exists(By locator, int timeoutSeconds)
    {
        return _waits.WaitForExists(locator, timeoutSeconds);
    }

    public void Clear(By locator, int timeoutSeconds)
    {
        _waits.WaitForVisible(locator, timeoutSeconds).Clear();
    }

    public void SendKeys(By locator, string keys, int timeoutSeconds)
    {
        _waits.WaitForVisible(locator, timeoutSeconds).SendKeys(keys);
    }

    public IWebElement FindFirstExisting(params By[] locators)
    {
        foreach (var locator in locators)
        {
            var els = _driver.FindElements(locator);
            if (els.Count > 0) return els[0];
        }

        throw new NoSuchElementException("None of the provided locators matched any element.");
    }

    public void ClickFirstExisting(int timeoutSeconds, params By[] locators)
    {
        Exception last = null;

        foreach (var locator in locators)
        {
            try
            {
                var element = _waits.WaitForClickable(locator, timeoutSeconds);
                ScrollIntoView(element);
                element.Click();
                return;
            }
            catch (Exception ex)
            {
                last = ex;
            }
        }

        throw new WebDriverException("Failed to click any of the provided locators.", last);
    }

    public void TypeIntoFirstExisting(string text, bool clear, int timeoutSeconds, params By[] locators)
    {
        Exception last = null;

        foreach (var locator in locators)
        {
            try
            {
                Type(locator, text, timeoutSeconds, clear);
                return;
            }
            catch (Exception ex)
            {
                last = ex;
            }
        }

        throw new WebDriverException("Failed to type into any of the provided locators.", last);
    }

    public bool TryClickFirstExisting(int timeoutSeconds, params By[] locators)
    {
        foreach (var locator in locators)
        {
            if (TryClick(locator, timeoutSeconds)) return true;
        }

        return false;
    }

    public void ClickWithScroll(By locator, int timeoutSeconds)
    {
        var element = _waits.WaitForVisible(locator, timeoutSeconds);
        ScrollIntoView(element);
        element = _waits.WaitForClickable(locator, timeoutSeconds);
        element.Click();
    }

    public void ScrollPageDown(int pixels = 600)
    {
        var js = (IJavaScriptExecutor)_driver;
        js.ExecuteScript("window.scrollBy(0, arguments[0]);", pixels);
    }

    public void ScrollIntoView(IWebElement element)
    {
        var js = (IJavaScriptExecutor)_driver;
        js.ExecuteScript("arguments[0].scrollIntoView({block: 'center', inline: 'nearest'});", element);
    }

    public void SwitchToNewTab(int timeoutSeconds = 10)
    {
        var currentWindow = _driver.CurrentWindowHandle;

        var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(timeoutSeconds));
        wait.Until(d => d.WindowHandles.Count > 1);

        var newWindow = _driver.WindowHandles.First(h => h != currentWindow);
        _driver.SwitchTo().Window(newWindow);
    }

    public IReadOnlyCollection<IWebElement> FindAllFirstExisting(By[] locators)
    {
        foreach (var by in locators)
        {
            var els = _driver.FindElements(by);
            if (els.Count > 0)
                return els;
        }

        return Array.Empty<IWebElement>();
    }

    public void FocusBodyWithJs()
    {
        var js = (IJavaScriptExecutor)_driver;
        js.ExecuteScript("document.body.focus();");
    }

    public void PressTab()
    {
        new Actions(_driver)
            .SendKeys(Keys.Tab)
            .Perform();
    }

    public void PressShiftTab()
    {
        new Actions(_driver)
            .KeyDown(Keys.Shift)
            .SendKeys(Keys.Tab)
            .KeyUp(Keys.Shift)
            .Perform();
    }

    public void PressEnter()
    {
        new Actions(_driver)
            .SendKeys(Keys.Enter)
            .Perform();
    }

    public void PressArrowDown()
    {
        new Actions(_driver)
            .SendKeys(Keys.ArrowDown)
            .Perform();
    }

    public void PressArrowRight()
    {
        new Actions(_driver)
            .SendKeys(Keys.ArrowRight)
            .Perform();
    }

    public void PressArrowUp()
    {
        new Actions(_driver)
            .SendKeys(Keys.ArrowUp)
            .Perform();
    }

    public IWebElement GetActiveElement()
    {
        var js = (IJavaScriptExecutor)_driver;
        return (IWebElement)js.ExecuteScript("return document.activeElement;");
    }

    public bool IsElementFocused(params By[] locators)
    {
        try
        {
            var activeElement = GetActiveElement();

            foreach (var locator in locators)
            {
                var found = _driver.FindElements(locator);

                foreach (var element in found)
                {
                    if (activeElement.Equals(element))
                        return true;
                }
            }

            return false;
        }
        catch
        {
            return false;
        }
    }

    public string GetActiveElementText()
    {
        try
        {
            return GetActiveElement().Text?.Trim() ?? string.Empty;
        }
        catch
        {
            return string.Empty;
        }
    }

    public void TypeIntoActiveElement(string text)
    {
        var activeEl = GetActiveElement();
        new Actions(_driver)
            .Click(activeEl)
            .SendKeys(text)
            .Perform();
    }

    public void TypeIntoActiveElementSlowly(string text, int delayMs = 100)
    {
        var activeEl = GetActiveElement();
        foreach (char c in text)
        {
            new Actions(_driver)
                .Click(activeEl)
                .SendKeys(c.ToString())
                .Perform();

            Thread.Sleep(delayMs);
        }
    }

    public int CountFirstExisting(params By[] locators)
    {
        foreach (var locator in locators)
        {
            var els = _driver.FindElements(locator);
            if (els.Count > 0)
                return els.Count;
        }

        return 0;
    }

    public void FocusElementWithJs(By locator)
    {
        var element = _driver.FindElement(locator);
        var js = (IJavaScriptExecutor)_driver;
        js.ExecuteScript(
            "arguments[0].scrollIntoView({block: 'center'}); arguments[0].focus();",
            element);
    }

    public void ClickElementWithJs(By locator)
    {
        var element = _driver.FindElement(locator);
        var js = (IJavaScriptExecutor)_driver;
        js.ExecuteScript(
            "arguments[0].scrollIntoView({block:'center'}); arguments[0].click();",
            element);
    }

    public void ScrollAndClickFirstWithJs(By locator)
    {
        var element = _driver.FindElement(locator);
        var js = (IJavaScriptExecutor)_driver;
        js.ExecuteScript(
            "arguments[0].scrollIntoView({block:'center'}); arguments[0].focus(); arguments[0].click();",
            element);
    }

    public void BlurActiveElementWithJs()
    {
        var js = (IJavaScriptExecutor)_driver;
        js.ExecuteScript("document.activeElement.blur();");
    }
}