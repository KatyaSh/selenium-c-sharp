using OpenQA.Selenium;

public abstract class BasePage
{
    protected readonly IWebDriver _driver;
    protected readonly Waits _waits;
    protected readonly ElementActions _elements;

    protected BasePage(IWebDriver driver, Waits waits, ElementActions elements)
    {
        this._driver = driver;
        _waits = waits;
        _elements = elements;
    }
}

