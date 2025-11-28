using NUnit.Framework;
using NUnit.Framework.Internal;
public class LoginPageTests : BrowserActions_BaseTest
{
    [Test]
    public void SuccessfulLoginEmptyCredentials()
    {
        var availableExamplePage = new AvailableExamplesPage(driver);
        var loginPage = new LoginPage(driver);
        var secureAreaPage = new SecureAreaPage(driver);

        availableExamplePage.OpenExamplePage("Form Authentication");
        Assert.That(loginPage.IsLoginPageOpened(), Is.True, "Login Page is not opened");
        loginPage.EnterUsername("tomsmith");
        loginPage.EnterPassword("SuperSecretPassword!");
        loginPage.ClickLoginButton();
        Assert.That(secureAreaPage.IsLoginSuccessfull(), Is.True, "Login was not successful");
    }

    [Test]
    public void UnsuccessfulLoginInvalidCredentials()
    {
        var availableExamplePage = new AvailableExamplesPage(driver);
        var loginPage = new LoginPage(driver);

        availableExamplePage.OpenExamplePage("Form Authentication");
        Assert.That(loginPage.IsLoginPageOpened(), Is.True, "Login Page is not opened");
        loginPage.EnterUsername("invalid");
        loginPage.EnterPassword("invalid!");
        loginPage.ClickLoginButton();
        Assert.That(loginPage.IsUnsuccessLogin(), Is.True, "Error message is not displayed");
        Assert.That(loginPage.GetInvalidLoginMessageText, Is.EqualTo("Your username is invalid!"));
    }

    [Test]
    public void UnsuccessfulLoginEmptyCredentials()
    {
        var availableExamplePage = new AvailableExamplesPage(driver);
        var loginPage = new LoginPage(driver);

        availableExamplePage.OpenExamplePage("Form Authentication");
        Assert.That(loginPage.IsLoginPageOpened(), Is.True, "Login Page is not opened");
        loginPage.EnterUsername("");
        loginPage.EnterPassword("");
        loginPage.ClickLoginButton();
        Assert.That(loginPage.IsUnsuccessLogin(), Is.True, "Error message is not displayed");
        Assert.That(loginPage.GetInvalidLoginMessageText, Is.EqualTo("Your username is invalid!"));
    }

    [Test]
    public void LogoutAfterSuccessfulLogin()
    {
        var availableExamplePage = new AvailableExamplesPage(driver);
        var loginPage = new LoginPage(driver);
        var secureAreaPage = new SecureAreaPage(driver);

        availableExamplePage.OpenExamplePage("Form Authentication");
        Assert.That(loginPage.IsLoginPageOpened(), Is.True, "Login Page is not opened");
        loginPage.EnterUsername("tomsmith");
        loginPage.EnterPassword("SuperSecretPassword!");
        loginPage.ClickLoginButton();
        Assert.That(secureAreaPage.IsLoginSuccessfull(), Is.True, "Login was not successful");
        secureAreaPage.ClickLogoutButton();
        Assert.That(loginPage.IsLoginPageOpened(), Is.True, "Login Page is not opened");
    }
}

