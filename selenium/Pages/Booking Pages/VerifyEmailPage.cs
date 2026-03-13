using OpenQA.Selenium;

public class VerifyEmailPage : BasePage
{
    // заголовок "Verify your email address to sign in"
    private readonly By _pageHeader = By.CssSelector(".nw-step-header");

    // поля ввода кода верификации
    private readonly By _otpForm = By.CssSelector("form.nw-email-otp");

    public VerifyEmailPage(IWebDriver driver, Waits waits, ElementActions elements)
        : base(driver, waits, elements)
    {
    }

    public void WaitUntilLoaded()
    {
        _waits.WaitForExists(_pageHeader, 20);
    }

    public bool IsVerifyEmailPageDisplayed()
    {
        bool hasHeader = _waits.WaitForExists(_pageHeader, 10);
        bool hasOtpForm = _waits.WaitForExists(_otpForm, 10);

        return hasHeader && hasOtpForm;
    }

    public string GetHeaderText()
    {
        IWebElement header = _elements.GetVisibleElement(_pageHeader, 10);
        return header.Text;
    }
}