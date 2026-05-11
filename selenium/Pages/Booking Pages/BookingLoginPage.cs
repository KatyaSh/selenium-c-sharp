using OpenQA.Selenium;

public class BookingLoginPage : BasePage
{
    private readonly By _pageAnchor = By.CssSelector("form.nw-signin");
    private readonly By _emailInput = By.CssSelector("input[name='username']");
    private readonly By _continueButton = By.CssSelector("button[type='submit']");

    public BookingLoginPage(IWebDriver driver, Waits waits, ElementActions elements)
        : base(driver, waits, elements)
    {
    }

    public void WaitUntilLoaded()
    {
        _waits.WaitForExists(_pageAnchor, 20);
    }

    public void EnterEmail(string email)
    {
        _elements.Type(_emailInput, email, timeoutSeconds: 10, clear: true);
    }

    public VerifyEmailPage ClickContinue()
    {
        _elements.Click(_continueButton, 10);
        return new VerifyEmailPage(_driver, _waits, _elements);
    }
}