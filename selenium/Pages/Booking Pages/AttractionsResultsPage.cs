using OpenQA.Selenium;

public class AttractionsResultsPage : BasePage
{
    private readonly By[] _resultsAnchor =
        {
            By.CssSelector("[data-testid='product-card']"),
            By.CssSelector("[data-testid='card']"),
            By.CssSelector("[data-testid='sr-list']"),
            By.CssSelector("a[href*='/attractions/']")
        };

    private readonly By[] _attractionCards =
    {
            By.CssSelector("[data-testid='product-card'] a"),
            By.CssSelector("[data-testid='card'] a"),
            By.CssSelector("a[href*='/attractions/']")
        };

    public AttractionsResultsPage(IWebDriver driver, Waits waits, ElementActions elements)
        : base(driver, waits, elements)
    {
    }

    public void WaitUntilLoaded()
    {
        _waits.WaitForExists(_resultsAnchor[0], 20);
    }

    public AttractionDetailsPage OpenFirstAttraction()
    {
        var firstAttractionLink = _elements.FindFirstExisting(_attractionCards);

        ((IJavaScriptExecutor)_driver)
            .ExecuteScript("arguments[0].scrollIntoView({block:'center'});", firstAttractionLink);
        firstAttractionLink.Click();

        _elements.SwitchToNewTab(10);

        return new AttractionDetailsPage(_driver, _waits, _elements);
    }
}

