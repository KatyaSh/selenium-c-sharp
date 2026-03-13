using OpenQA.Selenium;

public class FinalDetailsPage : BasePage
{
    private readonly By _pageAnchor = By.CssSelector("[data-testid='bs3-payment-timing-section']");

    private readonly By _bookingSummary = By.CssSelector("[data-testid='booking-details-date-summary']");

    private readonly By[] _bookAndPayButton =
    {
        By.CssSelector("button.js-bp-submit-button--complete-booking"),
        By.XPath("//button[contains(.,'Book and pay')]")
    };

    public FinalDetailsPage(IWebDriver driver, Waits waits, ElementActions elements)
        : base(driver, waits, elements)
    {
    }

    public void WaitUntilLoaded()
    {
        _waits.WaitForExists(_pageAnchor, 30);
    }

    public bool HasCorrectDates(DateTime checkIn, DateTime checkOut)
    {
        IWebElement summaryEl = _elements.GetVisibleElement(_bookingSummary, 10);
        string summaryText = summaryEl.Text;

        string expectedCheckIn = checkIn.ToString("ddd, MMM d, yyyy",
            System.Globalization.CultureInfo.InvariantCulture);
        string expectedCheckOut = checkOut.ToString("ddd, MMM d, yyyy",
            System.Globalization.CultureInfo.InvariantCulture);

        bool hasCheckIn = summaryText.Contains(expectedCheckIn);
        bool hasCheckOut = summaryText.Contains(expectedCheckOut);

        return hasCheckIn && hasCheckOut;
    }

    public bool IsBookAndPayButtonVisible()
    {
        return _waits.WaitForExists(_bookAndPayButton[0], 10)
            || _waits.WaitForExists(_bookAndPayButton[1], 5);
    }
}