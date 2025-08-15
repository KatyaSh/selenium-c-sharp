using NUnit.Framework;
using OpenQA.Selenium;

public class LogoutTests : BaseTestSuccesfullLogin
{
    [Test]
    public void Logout()
    {
        var burgerMenuButton = driver.FindElement(By.Id("react-burger-menu-btn"));
        burgerMenuButton.Click();       
        var logoutButton = webDriverWait.Until(
            SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("logout_sidebar_link"))
        );
        logoutButton.Click();
        var actualUrl = driver.Url;
        var expectedUrl = BaseUrl;

        Assert.That(actualUrl, Is.EqualTo(expectedUrl), "Wrong URL for Login page");
    }
}

