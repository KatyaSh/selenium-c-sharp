using NUnit.Framework;
using OpenQA.Selenium;

public class LoginTests : BaseTest
{
    [Test]
    public void UnsuccessfulLoginEmptyCredentials()
    {
        WaitForElement(By.ClassName("login_logo"));
        var loginButton = driver.FindElement(By.Id("login-button"));
        loginButton.Click();
        var errorMessage = driver.FindElement(By.CssSelector("[data-test='error']"));

        Assert.That(errorMessage.Displayed, Is.True, "Error message is not displayed");

        var actualErrorText = errorMessage.Text;
        var expectedErrorText = "Epic sadface: Username is required";

        Assert.That(actualErrorText, Is.EqualTo(expectedErrorText), "Wrong error message text");
    }

    [Test]
    public void UnsuccessfulLoginInvalidCredentials()
    {
        Login("invalid_username", "invalid_password");
        var errorMessage = driver.FindElement(By.CssSelector("[data-test='error']"));

        Assert.That(errorMessage.Displayed, Is.True, "Error message is not displayed");

        var actualErrorText = errorMessage.Text;
        var expectedErrorText = "Epic sadface: Username and password do not match any user in this service";

        Assert.That(actualErrorText, Is.EqualTo(expectedErrorText), "Wrong error message text");
    }
}

