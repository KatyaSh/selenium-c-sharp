using NUnit.Framework;
using Booking;

[TestFixture]
public class CarRentalPageTests : Booking_BaseTest
{
    [Test]
    public void CarRentals_ShouldShowError_WhenPickupLocationIsEmptyOrInvalid()
    {
        var homePage = new HomePage(driver, waits, elements);

        homePage.AcceptCookiesIfPresent();
        homePage.CloseBaner();

        var carRentalsPage = homePage.ClickCarRentalsMenu();

        carRentalsPage.WaitUntilLoaded();
        carRentalsPage.ClickSearch();

        Assert.That(
            carRentalsPage.HasPickupLocationError(),
            Is.True,
            "Error message was not shown for empty pickup location."
        );
                
        carRentalsPage.ClearPickupLocation();
        carRentalsPage.SetPickupLocation("asdasdasd@@@");
        carRentalsPage.ClickSearch();

        Assert.That(
            carRentalsPage.HasPickupLocationError(),
            Is.True,
            "Error message was not shown for invalid pickup location."
        );
    }
}
