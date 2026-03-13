using OpenQA.Selenium;

public class AttractionsSearchPage : BasePage
{
    private readonly By[] _cityInput =
    {
            By.CssSelector("input[data-testid='search-input-field']"),
            By.CssSelector("input[placeholder='Where are you going?']"),            
        };

    private readonly By _autocompleteFirstItem = By.CssSelector("[data-testid='search-bar-result']]");

    private readonly By[] _searchButton =
        {
            By.CssSelector("button[data-testid='search-button']"),
            By.CssSelector("button[type='submit']")            
        };

    private readonly By _firstCityOption = By.CssSelector("button[role='option']");

    public AttractionsSearchPage(IWebDriver driver, Waits waits, ElementActions elements)
        : base(driver, waits, elements)
    {
    }

    public void SetCity(string city)
    {
        _elements.TypeIntoFirstExisting(city, clear: true, timeoutSeconds: 10, _cityInput);
        _elements.ClickWithScroll(_firstCityOption, 10);
    }

    public void SelectDates(DateTime from, DateTime to)
    {
        var fromStr = from.ToString("yyyy-MM-dd");
        var toStr = to.ToString("yyyy-MM-dd");

        var fromCell = By.CssSelector($"span[role='button'][data-date='{fromStr}'], [data-date='{fromStr}']");
        var toCell = By.CssSelector($"span[role='button'][data-date='{toStr}'], [data-date='{toStr}']");

        _elements.ClickWithScroll(fromCell, 10);
        _elements.ClickWithScroll(toCell, 10);
    }

    public AttractionsResultsPage ClickSearch()
    {
        _elements.ClickFirstExisting(10, _searchButton);
        return new AttractionsResultsPage(_driver, _waits, _elements);
    }
}

