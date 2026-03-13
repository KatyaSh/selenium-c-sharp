using Booking;
using NUnit.Framework;

[TestFixture]
public class AttractionsPageTests : Booking_BaseTest
{
    [Test]
    public void SearchAttractionsInSpecificCity()
    {
        var city = "Krakow";
        var startDate = DateTime.Today.AddDays(5);
        var endDate = startDate.AddDays(7);

        var homePage = new HomePage(driver, waits, elements);

        homePage.AcceptCookiesIfPresent();
        homePage.CloseBaner();

        var attractionsSearchPage = homePage.ClickAttractionsMenu();

        attractionsSearchPage.SetCity(city);
        attractionsSearchPage.SelectDates(startDate, endDate);

        var resultsPage = attractionsSearchPage.ClickSearch();

        resultsPage.WaitUntilLoaded();

        var detailsPage = resultsPage.OpenFirstAttraction();

        detailsPage.WaitUntilLoaded();

        Assert.That(detailsPage.HasDate(startDate), Is.True, $"Selected date {startDate:yyyy-MM-dd} not found in date picker.");
        Assert.That(detailsPage.HasAnyTimeSlot(), Is.True, "No timeslots found on details page.");
    }
}

