using Booking;
using NUnit.Framework;

[TestFixture]
public class LanguageChangeTests : Booking_BaseTest
{
    [Test]
    public void ChangeLanguageToGerman()
    {
        var homePage = new HomePage(driver, waits, elements);
        homePage.AcceptCookiesIfPresent();
        homePage.CloseBaner();

        homePage.ChangeLanguage("de");

        string title = homePage.GetHeroBannerTitle();

        Assert.That(
            title.Length, Is.GreaterThan(0),
            "Hero banner title is empty after language change."
        );

        Assert.That(
            title, Does.Contain("nächste").Or.Contain("Unterkunft").Or.Contain("Reise"),
            $"Page title doesn't look like German. Actual: '{title}'"
        );
    }
}