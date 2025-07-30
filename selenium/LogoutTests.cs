using NUnit.Framework;
using OpenQA.Selenium;

public class LogoutTests : BaseTest
{
    [Test]
    public void Logout()
    {
        SuccesfullLogin("standard_user", "secret_sauce");
        var burgerMenuButton = driver.FindElement(By.Id("react-burger-menu-btn"));
        burgerMenuButton.Click();
        //var logoutButton = WaitForElement(By.Id("logout_sidebar_link"));
        var logoutButton = webDriverWait.Until(
            SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("logout_sidebar_link"))
        );
        logoutButton.Click();
        var actualUrl = driver.Url;
        var expectedUrl = "https://www.saucedemo.com/";

        Assert.That(actualUrl, Is.EqualTo(expectedUrl), "Wrong URL for Login page");
    }
}

