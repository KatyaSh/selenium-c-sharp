using Booking;
using NUnit.Framework;
using OpenQA.Selenium;

[TestFixture]
public class InvalidLoginTests : Booking_BaseTest
{
    private const string UnregisteredEmail = "unregistered.test123@email.com";

    [Test]
    public void EnterUnregisteredEmail_ShouldShowVerifyEmailPage()
    {
        var homePage = new HomePage(driver, waits, elements);
        homePage.AcceptCookiesIfPresent();
        homePage.CloseBaner();

        var loginPage = homePage.ClickSignInButton();
        loginPage.WaitUntilLoaded();

        loginPage.EnterEmail(UnregisteredEmail);
        
        var verifyPage = loginPage.ClickContinue();       
        verifyPage.WaitUntilLoaded();

        Assert.That(
            verifyPage.IsVerifyEmailPageDisplayed(),
            Is.True,
            "Verify email page was not displayed after entering unregistered email."
        );

        Assert.That(
            verifyPage.GetHeaderText(),
            Does.Contain("Verify your email"),
            $"Header text is unexpected. Actual: '{verifyPage.GetHeaderText()}'"
        );
    }
}