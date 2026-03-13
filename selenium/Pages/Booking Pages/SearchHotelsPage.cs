using Booking;
using OpenQA.Selenium;

public class SearchHotelsPage : BasePage
{
    private readonly By _propertyCard = By.CssSelector("[data-testid='property-card']");
    
    private readonly By _resultsAnchor = By.CssSelector(
        "[data-testid='property-card'], [data-testid='srp-card'], #searchresultsTmpl"
    );
    
    private readonly By _srpCard = By.CssSelector("[data-testid='srp-card']");

    private readonly By[] _filter5Stars =
    {
        By.CssSelector("input[name='class=5']"),
        By.CssSelector("input[id*='class-5']"),
        By.XPath("//label[.//*[contains(.,'5 stars') or contains(.,'5 gwiazdek')]]//input[contains(@name,'class')]"),
        By.XPath("//label[contains(.,'5 stars') or contains(.,'5 gwiazdek')]")
    };

    private readonly By[] _sortDropdown =
    {
        By.CssSelector("button[data-testid='sorters-dropdown-trigger']"),
        By.CssSelector("[data-testid='sorters-dropdown-trigger']"),
        By.CssSelector("button[aria-label*='Sort']"),
        By.CssSelector("button[aria-label*='Sortowanie']")
    };

    private readonly By[] _sortLowestPrice =
    {
        By.CssSelector("button[data-id='price']"),
        By.CssSelector("[role='menuitem'][data-id='price']"),
        By.XPath("//button[contains(.,'Price (lowest first)') or contains(.,'Cena (od najniższej)') or contains(.,'Cena (od najtańszych)')]"),
        By.XPath("//*[@role='menuitem' or self::button][contains(.,'Price (lowest first)') or contains(.,'Cena (od najniższej)') or contains(.,'Cena (od najtańszych)')]")
    };

    private readonly By[] _filterFitnessCentre =
    {
        By.CssSelector("input[name*='fitness']"),
        By.XPath("//div[@data-testid='filters-group-label-content' and contains(.,'Fitness center')]//ancestor::label"),
        By.XPath("//div[@data-testid='filters-group-label-content' and contains(.,'Centrum fitness')]//ancestor::label")
    };

    private readonly By[] _priceElements =
    {
        By.CssSelector("[data-testid='price-and-discounted-price']"),
        By.CssSelector("[data-testid='property-card'] [data-testid='price-and-discounted-price']"),
        By.CssSelector("[data-testid='srp-card'] [data-testid='price-and-discounted-price']")
    };
    
    private readonly By[] _firstHotelLink =
    {
        By.CssSelector("[data-testid='property-card'] [data-testid='title-link']"),
        By.CssSelector("[data-testid='property-card'] a[href*='/hotel/']"),
        By.CssSelector("[data-testid='property-card'] h3 a")
    };

    public SearchHotelsPage(IWebDriver driver, Waits waits, ElementActions elements)
        : base(driver, waits, elements)
    {
    }

    public SearchHotelsPage WaitUntilLoaded(int timeoutSeconds = 30)
    {
        _waits.WaitForExists(_resultsAnchor, timeoutSeconds);
        return this;
    }

    public int GetHotelsCount(int timeoutSeconds = 30)
    {
        _waits.WaitForExists(_propertyCard, timeoutSeconds);
        return _driver.FindElements(_propertyCard).Count;
    }

    public bool HasAnyResults(int timeoutSeconds = 30)
    {
        return _waits.WaitForExists(_propertyCard, timeoutSeconds);
    }

    public SearchHotelsPage ApplyFitnessCentreFilter()
    {
        _elements.ClickFirstExisting(15, _filterFitnessCentre);
        WaitUntilLoaded();
        return this;
    }
    public List<int> GetDisplayedPrices()
    {
        var priceEls = _elements.FindAllFirstExisting(_priceElements);
        var prices = new List<int>();

        foreach (var el in priceEls)
        {
            var rawText = el.Text;

            var digitsOnly = "";
            foreach (char c in rawText)
            {
                if (char.IsDigit(c))
                {
                    digitsOnly += c;
                }
            }

            if (string.IsNullOrWhiteSpace(digitsOnly))
            {
                continue;
            }

            var parsed = int.TryParse(digitsOnly, out int price);
            if (parsed)
            {
                prices.Add(price);
            }
        }

        return prices;
    }
    public SearchHotelsPage Apply5StarsFilter()
    {
        _elements.ClickFirstExisting(15, _filter5Stars);
        WaitUntilLoaded();
        return this;
    }

    public SearchHotelsPage SortByLowestPrice()
    {
        _elements.ClickFirstExisting(10, _sortDropdown);
        _elements.ClickFirstExisting(10, _sortLowestPrice);
        WaitUntilLoaded();
        return this;
    }

    public HotelPage OpenFirstHotel()
    {
        var link = _elements.FindFirstExisting(_firstHotelLink);
        ((IJavaScriptExecutor)_driver)
            .ExecuteScript("arguments[0].scrollIntoView({block:'center'});", link);
        link.Click();
        _elements.SwitchToNewTab(10);
        return new HotelPage(_driver, _waits, _elements);
    }
}
