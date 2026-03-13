using OpenQA.Selenium;

public class FlightsPage : BasePage
{
    private readonly By _multiCityRadio = By.CssSelector("[for='search_type_option_MULTISTOP']");
    private readonly By _searchFormRoot = By.CssSelector("[data-ui-name='search_type_option_MULTISTOP']");
    private readonly By _flightItems = By.CssSelector("[data-ui-name='segments_list_item']");
    private readonly By _searchButton = By.CssSelector("button[data-ui-name='button_search_submit']");
    private readonly By _addFlightButton = By.XPath("//button[@data-ui-name='button_segment_add']");
    private readonly By _firstOption = By.CssSelector("[data-ui-name='locations_list_item']");

    public FlightsPage(IWebDriver driver, Waits waits, ElementActions elements)
        : base(driver, waits, elements)
    {
    }

    public void WaitUntilLoaded()
    {
        _waits.WaitForClickable(_searchButton, 20);
    }

    public void SelectMultiCityMode()
    {
        _elements.Click(_multiCityRadio, 10);
    }

    public int GetFlightCount()
    {
        return _driver.FindElements(_flightItems).Count;
    }

    public void EnsureFlightCountAtLeast(int expected)
    {
        while (GetFlightCount() < expected)
        {
            AddFlight();
        }
    }

    public void AddFlight()
    {
        _elements.Click(_addFlightButton, 10);
    }

    public void FillFlight(int flightIndexBased, string from, string to, DateTime date)
    {
        var fromButton = By.CssSelector($"button[data-ui-name='input_location_from_segment_{flightIndexBased}']");
        var toButton = By.CssSelector($"button[data-ui-name='input_location_to_segment_{flightIndexBased}']");
        var dateButton = By.CssSelector($"button[data-ui-name='input_date_segment_{flightIndexBased}'], button[data-ui-name='button_date_segment_{flightIndexBased}']");

        SetLocation(fromButton, from);
        SetLocation(toButton, to);
        SetDate(dateButton, date);
    }

    private By RemoveFlightButton(int indexBased)
    {
        return By.CssSelector($"button[data-ui-name='button_remove_segment_{indexBased}']");
    }

    public void RemoveFlight(int flightIndexBased)
    {
        var index0 = flightIndexBased - 1;

        var removeButton = RemoveFlightButton(index0);
        _elements.Click(removeButton, 10);
    }

    public FlightResultsPage ClickSearch()
    {
        _elements.Click(_searchButton, 10);
        return new FlightResultsPage(_driver, _waits, _elements);
    }

    private void SetLocation(By locationButton, string value)
    {
        _elements.Click(locationButton, 10);

        var input = By.CssSelector("input[type='text']");
        _elements.Type(input, value, 10, clear: true);

        _waits.WaitForExists(_firstOption, 10);
        _elements.Click(_firstOption, 10);
    }

    private void SetDate(By dateButton, DateTime date)
    {
        _elements.Click(dateButton, 10);

        var dateStr = date.ToString("yyyy-MM-dd");
        var cell = By.CssSelector($"[data-date='{dateStr}'], button[data-date='{dateStr}']");

        _elements.ClickWithScroll(cell, 15);
    }
}
