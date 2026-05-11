using OpenQA.Selenium;
public class CarRentalsPage : BasePage
{
    private readonly By _pickupLocationInput =
        By.CssSelector("input[placeholder='Airport, city, or station']");

    private readonly By _firstAutocompleteOption =
        By.CssSelector("button[role='option']");

    private readonly By _searchButton =
        By.CssSelector("button[type='submit']");

    private readonly By _pickupLocationError =By.CssSelector("[role='alert']");

    public CarRentalsPage(IWebDriver driver, Waits waits, ElementActions elements)
        : base(driver, waits, elements)
    {
    }

    public void WaitUntilLoaded()
    {
        _waits.WaitForExists(_pickupLocationInput, 20);
    }

    public void SetPickupLocation(string location)
    {
        _elements.Type(_pickupLocationInput,location, timeoutSeconds: 10,clear: true);        
    }

    public void SelectFirstPickupLocationFromAutocomplete()
    {
        _elements.Click(_firstAutocompleteOption, 10);
    }

    public void ClearPickupLocation()
    {
        _elements.Clear(_pickupLocationInput, 5);
    }

    public void ClickSearch()
    {
        _elements.Click(_searchButton, 10);
    }

    public bool HasPickupLocationError()
    {
        return _elements.Exists(_pickupLocationError, 5);
    }
}

