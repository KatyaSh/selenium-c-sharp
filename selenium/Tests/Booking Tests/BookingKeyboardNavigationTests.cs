using Booking;
using NUnit.Framework;

[TestFixture]
public class BookingKeyboardNavigationTests : Booking_BaseTest
{
    [Test]
    public void NavigateToRegisterPageUsingKeyboard()
    {
        var homePage = new HomePage(driver, waits, elements);
        homePage.AcceptCookiesIfPresent();
        homePage.CloseBaner();

        Assert.That(homePage.IsLoaded(), Is.True,
            "Booking.com homepage should display.");

        Assert.DoesNotThrow(() => homePage.NavigateToRegisterPageUsingKeyboard());

        var registerPage = new BookingRegisterPage(driver, waits, elements);
        registerPage.WaitUntilLoaded();

        Assert.That(registerPage.IsDisplayed(), Is.True,
            "User should be redirected to the registration page.");
    }

    [Test]
    public void ChangeCurrencyWithKeyboardNavigation()
    {
        var homePage = new HomePage(driver, waits, elements);
        homePage.AcceptCookiesIfPresent();
        homePage.CloseBaner();

        Assert.That(homePage.IsLoaded(), Is.True);

        homePage.NavigateToCurrencyWithKeyboard();
        Assert.That(homePage.IsCurrencyFocused(), Is.True);

        var selectedCurrencyCode = homePage.SelectLastCurrencyOptionWithKeyboard();
        Assert.That(homePage.IsCurrencyApplied(selectedCurrencyCode), Is.True);
    }

    [Test]
    public void SearchForStaysInSpecificLocationUsingKeyboard()
    {
        var homePage = new HomePage(driver, waits, elements);
        homePage.AcceptCookiesIfPresent();
        homePage.CloseBaner();

        Assert.That(homePage.IsLoaded(), Is.True,
            "Booking.com homepage should display.");

        homePage.NavigateToStaysSearchWithKeyboard();
        Assert.That(homePage.IsStaysSearchFocused(), Is.True,
            "'Stays' search bar is highlighted.");

        homePage.TypeDestinationAndTabWithKeyboard("New York");

        homePage.NavigateToCheckInDateWithKeyboard();
        Assert.That(homePage.IsCheckInDateFocused(), Is.True,
            "Check-In date field is focused.");

        homePage.SelectCheckInDateWithKeyboard(arrowRightPresses: 3);

        homePage.SelectGuestsWithKeyboard(arrowDownPresses: 0);

        var searchPage = homePage.StartSearchWithKeyboard();

        searchPage.WaitUntilLoaded();
        Assert.That(searchPage.HasAnyResults(), Is.True,
            "Search results for 'Stays' in 'New York' are displayed.");
    }

    [Test]
    public void SelectCityFromAutoCompleteUsingKeyboard()
    {
        var homePage = new HomePage(driver, waits, elements);
        homePage.AcceptCookiesIfPresent();
        homePage.CloseBaner();

        Assert.That(homePage.IsLoaded(), Is.True,
            "Booking.com homepage should display.");

        homePage.NavigateToStaysSearchWithKeyboard();
        Assert.That(homePage.IsStaysSearchFocused(), Is.True,
            "The search field is focused.");

        homePage.TypeDestinationWithKeyboard("New Yo");
        Assert.That(homePage.IsAutocompleteSuggestionsDisplayed(), Is.True,
            "Suggestions should display.");

        homePage.SelectAutocompleteWithKeyboard(arrowDownPresses: 1);
        Assert.That(homePage.GetDestinationValue(),
            Does.Contain("New York").IgnoreCase,
            "'New York' is selected in the dropdown field.");
    }

    [Test]
    public void SkipToMainContentUsingKeyboard()
    {
        var homePage = new HomePage(driver, waits, elements);
        homePage.AcceptCookiesIfPresent();
        homePage.CloseBaner();

        homePage.ReloadPage();

        Assert.That(homePage.IsLoaded(), Is.True,
            "Booking.com homepage should display.");

        elements.PressTab();
        Assert.That(homePage.IsSkipToMainContentDisplayed(), Is.True,
            "Skip to main content button is displayed.");

        homePage.ClickSkipToMainContentWithJs();
        Assert.That(homePage.IsNavigationMenuDisplayed(), Is.False,
            "Booking menu is not displayed.");

        homePage.PressShiftTab();
        Assert.That(homePage.IsNavigationMenuDisplayed(), Is.True,
            "Booking menu is displayed.");
    }
}