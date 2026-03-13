using NUnit.Framework.Internal;
using OpenQA.Selenium;

public class FlightResultsPage : BasePage
{
    private readonly By _resultsCard =
        By.CssSelector("[data-testid='searchresults_card']");

    private readonly By _noResultsText =
        By.XPath("//*[contains(.,'We don't have any flights matching your search')]");

    public FlightResultsPage(IWebDriver driver, Waits waits, ElementActions elements)
        : base(driver, waits, elements)
    {
    }

    public void WaitUntilLoaded()
    {
        var loaded =
            _waits.WaitForExists(_resultsCard, 20) ||
            _waits.WaitForExists(_noResultsText, 20);

        if (!loaded)
            throw new WebDriverTimeoutException("Flights results page not loaded (no results card / no empty state).");
    }

    public bool HasResults()
    {
        return _elements.Exists(_resultsCard, 3);
    }

    public bool HasNoResultsMessage()
    {
        return _elements.Exists(_noResultsText, 3);
    }
}


