using OpenQA.Selenium;

public class AirportTaxiDetailsPage : BasePage
{
    private readonly By _pageAnchor = By.CssSelector("[data-testid='route-summary-wrapper']");
    private readonly By _pickupDate = By.CssSelector("[data-test='pick-up-location__date']");  
    private readonly By _pickupTitle = By.CssSelector("[data-test='pick-up-location__title']");

    public AirportTaxiDetailsPage(IWebDriver driver, Waits waits, ElementActions elements)
        : base(driver, waits, elements)
    {
    }

    public void WaitUntilLoaded()
    {
        _waits.WaitForExists(_pageAnchor, 30);
    }

    public bool HasCorrectJourneyData(string expectedPickup, DateTime expectedDate)
    {
        IWebElement pickupTitleEl = _elements.GetVisibleElement(_pickupTitle, 10);
        IWebElement dateEl = _elements.GetVisibleElement(_pickupDate, 10);

        string actualPickup = pickupTitleEl.Text;
        string actualDate = dateEl.Text;

        string expectedDateStr = expectedDate.ToString("ddd, d MMM",
            System.Globalization.CultureInfo.InvariantCulture);

        bool pickupMatch = actualPickup.Contains(expectedPickup);
        bool dateMatch = actualDate.Contains(expectedDateStr);

        return pickupMatch && dateMatch;
    }
}