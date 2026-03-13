using Booking;
using NUnit.Framework;

[TestFixture]
public class BookHotelTests : Booking_BaseTest
{
    private const string City = "Krakow";
    private const int Adults = 2;
    private const int Children = 0;
    private const int Rooms = 1;
    private const int ChildAge = 7;

    private const string FirstName = "John";
    private const string LastName = "Doe";
    private const string Email = "john.doe@email.com";
    private const string Phone = "555-555-5555";

    private static readonly DateTime CheckIn = DateTime.Today.AddDays(3);
    private static readonly DateTime CheckOut = DateTime.Today.AddDays(7);

    [Test]
    public void SearchSelectAndAttemptToBookHotel()
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

        var hotelPage = resultsPage.OpenFirstHotel();

        hotelPage.WaitUntilLoaded();
        hotelPage.SelectOneRoom();

        var bookingDetailsPage = hotelPage.ClickReserve();

        bookingDetailsPage.WaitUntilLoaded();
        bookingDetailsPage.EnterFirstName(FirstName);
        bookingDetailsPage.EnterLastName(LastName);
        bookingDetailsPage.EnterEmail(Email);
        bookingDetailsPage.EnterPhone(Phone);

        var finalDetailsPage = bookingDetailsPage.ClickNext();

        finalDetailsPage.WaitUntilLoaded();

        Assert.That(
            finalDetailsPage.HasCorrectDates(CheckIn, CheckOut),
            Is.True,
            $"Booking summary dates don't match. Expected {CheckIn:ddd, MMM d, yyyy} and {CheckOut:ddd, MMM d, yyyy}."
        );

        Assert.That(
            finalDetailsPage.IsBookAndPayButtonVisible(),
            Is.True,
            "Expected 'Book and pay' button to be visible."
        );
    }
}