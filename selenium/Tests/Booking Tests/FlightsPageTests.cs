using Booking;
using NUnit.Framework;

[TestFixture]
public class FlightsPageTests : Booking_BaseTest
{ 
    [Test]
    public void MultiCityFlightSearch()
    {
        var baseDate = DateTime.Today.AddDays(15);
        var homePage = new HomePage(driver, waits, elements);

        homePage.AcceptCookiesIfPresent();
        homePage.CloseBaner();

        var flightsPage = homePage.ClickFlightsMenu();
        flightsPage.WaitUntilLoaded();

        flightsPage.SelectMultiCityMode();

        flightsPage.EnsureFlightCountAtLeast(2);
        Assert.That(flightsPage.GetFlightCount(), Is.GreaterThanOrEqualTo(2));

        flightsPage.FillFlight(0, "Warsaw", "Vienna", baseDate.AddDays(2));
        flightsPage.FillFlight(1, "Vienna", "Rome", baseDate.AddDays(4));
       
        flightsPage.AddFlight();
        flightsPage.EnsureFlightCountAtLeast(3);
        Assert.That(flightsPage.GetFlightCount(), Is.GreaterThanOrEqualTo(3));

        flightsPage.FillFlight(2, "Barcelona", "Warsaw", baseDate.AddDays(6));

        var beforeRemove = flightsPage.GetFlightCount();
        flightsPage.RemoveFlight(3);

        Assert.That(
            flightsPage.GetFlightCount(),
            Is.EqualTo(beforeRemove - 1)
        );

        var resultsPage = flightsPage.ClickSearch();
        resultsPage.WaitUntilLoaded();

        Assert.That(
            resultsPage.HasResults() || resultsPage.HasNoResultsMessage(),
            Is.True
        );
    }
}


