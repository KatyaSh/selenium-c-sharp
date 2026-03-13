using OpenQA.Selenium;

public class BookingDetailsPage : BasePage
{
    private readonly By _pageAnchor = By.CssSelector("[data-testid='user-details-firstname']");

    private readonly By _firstName = By.CssSelector("[data-testid='user-details-firstname']");
    private readonly By _lastName = By.CssSelector("[data-testid='user-details-lastname']");
    private readonly By _email = By.CssSelector("[data-testid='user-details-email']");
    private readonly By _phone = By.CssSelector("[data-testid='phone-number-input']");

    private readonly By[] _nextButton =
{
    By.CssSelector("button[name='book'][type='submit']"),
    By.XPath("//button[contains(.,'Next: Final details')]")
};

    public BookingDetailsPage(IWebDriver driver, Waits waits, ElementActions elements)
        : base(driver, waits, elements)
    {
    }

    public void WaitUntilLoaded()
    {
        _waits.WaitForExists(_pageAnchor, 30);
    }

    public void EnterFirstName(string firstName)
    {
        _elements.Type(_firstName, firstName, timeoutSeconds: 10, clear: true);
    }

    public void EnterLastName(string lastName)
    {
        _elements.Type(_lastName, lastName, timeoutSeconds: 10, clear: true);
    }

    public void EnterEmail(string email)
    {
        _elements.Type(_email, email, timeoutSeconds: 10, clear: true);
    }

    public void EnterPhone(string phone)
    {
        _elements.Type(_phone, phone, timeoutSeconds: 10, clear: true);
    }

    public FinalDetailsPage ClickNext()
    {
        _elements.ClickFirstExisting(10, _nextButton);
        return new FinalDetailsPage(_driver, _waits, _elements);
    }
}