using Booking;
using NUnit.Framework;

[TestFixture]
public class SearchHotelsPageTests : Booking_BaseTest
{
    private const string City = "Warsaw";
    private const int Adults = 2;
    private const int Children = 1;
    private const int Rooms = 1;
    private const int ChildAge = 7;
    private static readonly DateTime CheckIn = DateTime.Today.AddDays(3);
    private static readonly DateTime CheckOut = DateTime.Today.AddDays(7);

    [Test]
    public void SearchHotels()
    {
        var homePage = new HomePage(driver, waits, elements);

        homePage.AcceptCookiesIfPresent();
        homePage.CloseBaner();
        homePage.SetDestination("Warsaw");
        homePage.SelectDates(
         DateTime.Today.AddDays(1),
         DateTime.Today.AddDays(3)
         );

        var resultsPage = homePage.ClickSearch();
        resultsPage.WaitUntilLoaded();

        var hotelsCount = resultsPage.GetHotelsCount();
        Assert.That(hotelsCount, Is.GreaterThan(0));
    }

    [Test]
    public void SearchHotelsInCityWithFilters()
    {
        var homePage = new HomePage(driver, waits, elements);

        homePage.AcceptCookiesIfPresent();
        homePage.CloseBaner();
        homePage.SetDestination(City);
        homePage.SelectDates(CheckIn, CheckOut);
        homePage.SetOccupancy(Adults, Children, Rooms, ChildAge);

        var resultsPage = homePage.ClickSearch();
        resultsPage.WaitUntilLoaded();

        Assert.That(
            resultsPage.HasAnyResults(),
            Is.True,
            $"Expected hotel results for '{City}', but the list is empty."
        );

        resultsPage.Apply5StarsFilter();

        Assert.That(
            resultsPage.HasAnyResults(),
            Is.True,
            "After applying 5-star filter the results list is empty."
        );

        resultsPage.SortByLowestPrice();

        Assert.That(
            resultsPage.HasAnyResults(),
            Is.True,
            "After sorting by lowest price the results list is empty."
        );

        var prices = resultsPage.GetDisplayedPrices();
        if (prices.Count >= 2)
        {
            Assert.That(
                prices,
                Is.Ordered.Ascending,
                "Hotels are not sorted by price from lowest to highest."
            );
        }

        resultsPage.ApplyFitnessCentreFilter();

        Assert.That(
            resultsPage.HasAnyResults(),
            Is.True,
            "After applying 'Fitness centre' filter the results list is empty."
        );
    }
}

