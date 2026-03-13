using OpenQA.Selenium;

public class AirportTaxiPage : BasePage
{
    private readonly By _pageAnchor = By.CssSelector("[data-testid='taxi-date-time-picker-form-element-oneway']");
    private readonly By _pickupInput = By.CssSelector("[data-testid='taxi-autocomplete-search-box__input-pickup']");
    private readonly By _pickupFirst = By.CssSelector("[data-testid='taxi-autocomplete-search-drop-down__item-pickup-0']");
    private readonly By _destInput = By.CssSelector("[data-testid='taxi-autocomplete-search-box__input-dropoff']");
    private readonly By _destFirst = By.CssSelector("[data-testid='taxi-autocomplete-search-drop-down__item-dropoff-0']");
    private readonly By _dateTimeButton = By.CssSelector("[data-testid='taxi-date-time-picker-form-element__button-oneway']");
    private readonly By _searchButton = By.CssSelector("[data-testid='submit-button']");

    private By DateCell(string date) => By.CssSelector($"[data-date='{date}']");

    public AirportTaxiPage(IWebDriver driver, Waits waits, ElementActions elements)
        : base(driver, waits, elements)
    {
    }

    public void WaitUntilLoaded()
    {
        _waits.WaitForExists(_pageAnchor, 20);
    }

    public void SetPickupLocation(string location)
    {
        _elements.Type(_pickupInput, location, timeoutSeconds: 10, clear: true);
        _waits.WaitForExists(_pickupFirst, 10);
        _elements.Click(_pickupFirst, 10);
    }

    public void SetDestination(string destination)
    {
        _elements.Type(_destInput, destination, timeoutSeconds: 10, clear: true);
        _waits.WaitForExists(_destFirst, 10);
        _elements.Click(_destFirst, 10);
    }

    public void SelectDate(DateTime date)
    {
        _elements.Click(_dateTimeButton, 10);
        string dateStr = date.ToString("yyyy-MM-dd");
        _elements.ClickWithScroll(DateCell(dateStr), 10);
    }

    public AirportTaxiResultsPage ClickSearch()
    {
        _elements.Click(_searchButton, 10);
        return new AirportTaxiResultsPage(_driver, _waits, _elements);
    }
}