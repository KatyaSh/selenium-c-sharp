using Booking;
using NUnit.Framework;

[TestFixture]
public class AirportTaxiTests : Booking_BaseTest
{
    private static readonly DateTime TaxiDate = DateTime.Today.AddDays(3);

    [Test]
    public void BookMostExpensiveAirportTaxi()
    {
        var homePage = new HomePage(driver, waits, elements);
        homePage.AcceptCookiesIfPresent();
        homePage.CloseBaner();
        
        var taxiPage = homePage.ClickAirportTaxisMenu();
        taxiPage.WaitUntilLoaded();
        taxiPage.SetPickupLocation("Warsaw Frederic Chopin Airport");
        taxiPage.SetDestination("Courtyard by Marriott Warsaw Airport");
        taxiPage.SelectDate(TaxiDate);

        var resultsPage = taxiPage.ClickSearch();        
        resultsPage.WaitUntilLoaded();
        resultsPage.ClickMostExpensiveTaxi();

        var detailsPage = resultsPage.ClickContinue();
        detailsPage.WaitUntilLoaded();

        Assert.That(
            detailsPage.HasCorrectJourneyData(
            expectedPickup: "Warsaw Chopin Airport",
            expectedDate: TaxiDate
            ),
         Is.True,
         $"Journey data is incorrect. Expected pickup: 'Warsaw Chopin Airport', date: {TaxiDate:ddd, d MMM}"
        );
    }
}