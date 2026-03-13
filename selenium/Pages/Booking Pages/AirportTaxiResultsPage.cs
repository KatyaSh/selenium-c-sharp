using OpenQA.Selenium;

public class AirportTaxiResultsPage : BasePage
{
    private readonly By _pageAnchor = By.CssSelector("[data-test*='taxi-card-wrapper']");
    private readonly By _taxiCards = By.CssSelector("[data-test*='taxi-card-wrapper__car-card'][role='button']");
    private readonly By _priceElements = By.CssSelector("[data-test='price-taxi-price']");
    private readonly By _cookiesAccept = By.Id("onetrust-accept-btn-handler");
    private readonly By _continueButton = By.CssSelector("[data-test='continue-action-bar__continue-button']");

    public AirportTaxiResultsPage(IWebDriver driver, Waits waits, ElementActions elements)
        : base(driver, waits, elements)
    {
    }

    public void AcceptCookiesIfPresent()
    {
        _elements.Click(_cookiesAccept, 5);
    }

    public void WaitUntilLoaded()
    {
        _waits.WaitForExists(_pageAnchor, 30);
    }

    public void ClickMostExpensiveTaxi()
    {
        var taxiCards = _driver.FindElements(_taxiCards);

        IWebElement mostExpensiveCard = null;
        double maxPrice = 0;

        foreach (var card in taxiCards)
        {
            var priceEl = card.FindElement(_priceElements);
            double price = ParsePrice(priceEl.Text);

            if (price > maxPrice)
            {
                maxPrice = price;
                mostExpensiveCard = card;
            }
        }

        ((IJavaScriptExecutor)_driver)
            .ExecuteScript("arguments[0].scrollIntoView({block:'center'});", mostExpensiveCard);
        mostExpensiveCard.Click();
    }

    public AirportTaxiDetailsPage ClickContinue()
    {
        _elements.ClickWithScroll(_continueButton, 10);
        return new AirportTaxiDetailsPage(_driver, _waits, _elements);
    }

    private double ParsePrice(string rawText)
    {
        string digitsOnly = "";
        foreach (char c in rawText)
        {
            if (char.IsDigit(c) || c == '.')
            {
                digitsOnly += c;
            }
        }

        bool parsed = double.TryParse(
            digitsOnly,
            System.Globalization.NumberStyles.Any,
            System.Globalization.CultureInfo.InvariantCulture,
            out double price
        );

        if (parsed)
        {
            return price;
        }

        return 0;
    }
}