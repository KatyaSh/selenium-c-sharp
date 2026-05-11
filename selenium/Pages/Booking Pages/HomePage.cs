using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

namespace Booking
{
    public class HomePage : BasePage
    {
        private readonly By _cookiesAccept = By.XPath("//button[@id='onetrust-accept-btn-handler']");
        private readonly By _banner = By.XPath("//button[@aria-label='Dismiss sign-in info.']");

        private readonly By[] _destinationInput =
        {
           By.CssSelector("input[name='ss']"),
           By.CssSelector("input[data-testid='searchbox-destination-input']"),
           By.CssSelector("[data-testid='searchbox-destination'] input"),
        };

        private readonly By[] _autocompleteFirstItem =
        {
            By.CssSelector("[data-testid='autocomplete-result']"),
            By.CssSelector("[data-testid='autocomplete-results'] [role='option']"),
            By.CssSelector("[role='listbox'] [role='option']")
        };

        private readonly By _openCalendar = By.XPath("//button[@data-testid='searchbox-dates-container']");

        private readonly By[] _searchButton =
        {
            By.CssSelector("button[type='submit']"),
            By.CssSelector("button[data-testid='searchbox-submit-button']"),
            By.CssSelector("[data-testid='searchbox-submit-button']")
                  };

        private readonly By _attractionsMenu = By.CssSelector("#attractions");
        private readonly By _carRentalMenu = By.CssSelector("#cars");
        private readonly By _flightsMenu = By.CssSelector("#flights");
        private readonly By _airportTaxisMenu = By.CssSelector("#airport_taxis");

        private readonly By _openOccupancy = By.CssSelector("button[data-testid='occupancy-config']");

        private readonly By[] _adultsCount =
        {
            By.CssSelector("input#group_adults"),
            By.CssSelector("[data-testid='occupancy-adults-count']"),
            By.CssSelector("input[name='group_adults']")
        };

        private readonly By[] _childrenCount =
        {
            By.CssSelector("input#group_children"),
            By.CssSelector("[data-testid='occupancy-children-count']"),
            By.CssSelector("input[name='group_children']")
        };

        private readonly By[] _roomsCount =
        {
            By.CssSelector("input#no_rooms"),
            By.CssSelector("[data-testid='occupancy-rooms-count']"),
            By.CssSelector("input[name='no_rooms']")
        };

        private readonly By _incAdults = By.XPath("//label[@for='group_adults']/../..//button[2]");
        private readonly By _decAdults = By.XPath("//label[@for='group_adults']/../..//button[1]");
        private readonly By _incChildren = By.XPath("//label[@for='group_children']/../..//button[2]");
        private readonly By _decChildren = By.XPath("//label[@for='group_children']/../..//button[1]");
        private readonly By _incRooms = By.XPath("//label[@for='no_rooms']/../..//button[2]");
        private readonly By _decRooms = By.XPath("//label[@for='no_rooms']/../..//button[1]");

        private readonly By[] _kidsAgeSelect =
        {
            By.CssSelector("select[data-testid='kids-age-select']"),
            By.CssSelector("select[name^='age']"),
            By.CssSelector("select[id^='age']")
        };

        private readonly By _languagePickerTrigger = By.CssSelector("[data-testid='header-language-picker-trigger']");
        private readonly By _heroBannerTitle = By.CssSelector("[data-testid='herobanner-title1']");
        private readonly By _signInButton = By.CssSelector("[data-testid='header-sign-in-button'][aria-label='Sign in']");
        private readonly By _registerButton = By.CssSelector("[data-testid='header-sign-up-button']");
        private readonly By _currencyTrigger = By.CssSelector("button[data-testid='header-currency-picker-trigger']");
        private readonly By _currencyOptions = By.CssSelector("button[data-testid='selection-item']");
        private readonly By _calendarDay = By.CssSelector("span[data-date]");
        private readonly By[] _checkInDateField =
        {
            By.CssSelector("[data-testid='date-display-field-start']"),
            By.CssSelector("[data-testid='searchbox-dates-container']"),
            By.CssSelector("button[data-testid='searchbox-dates-container']")
        };
        private readonly By _checkOutDateField = By.CssSelector("[data-testid='date-display-field-end']");
        private readonly By _currentCalendarDate = By.CssSelector("span[data-date][aria-current='date']");

        private readonly By[] _skipToMainContent =
        {
            By.CssSelector("a[href='#main']"),
            By.XPath("//a[.//span[text()='Skip to main content']]")
        };

        private readonly By[] _navigationMenu =
        {
            By.CssSelector("nav[data-testid='header-xpb']"),
            By.CssSelector("[data-testid='header-xpb']"),
            By.CssSelector("nav[role='navigation']")
        };

        public HomePage(IWebDriver driver, Waits waits, ElementActions elements)
            : base(driver, waits, elements)
        {
        }

        public void AcceptCookiesIfPresent()
        {
            if (_elements.TryClick(_cookiesAccept, 3))
                _waits.WaitForDisappear(_cookiesAccept, 5);
        }

        public void CloseBaner()
        {
            if (_elements.TryClick(_banner, 3))
                _waits.WaitForDisappear(_banner, 5);
        }

        public bool IsLoaded()
        {
            return _driver.Url.Contains("booking.com", StringComparison.OrdinalIgnoreCase);
        }

        public void SetDestination(string destination, bool selectFromAutocomplete = true)
        {
            _elements.TypeIntoFirstExisting(destination, clear: true, timeoutSeconds: 5, _destinationInput);

            if (selectFromAutocomplete)
            {
                _waits.WaitForExists(_autocompleteFirstItem[0], 10);

                var clicked = _elements.TryClickFirstExisting(5, _autocompleteFirstItem);

                if (!clicked)
                    _elements.FindFirstExisting(_destinationInput).SendKeys(Keys.Enter);
            }
        }

        public void OpenCalendar()
        {
            _elements.ClickWithScroll(_openCalendar, 5);
        }

        public void SelectDates(DateTime checkIn, DateTime checkOut)
        {
            var checkInCell = By.CssSelector($"span[role='button'][data-date='{checkIn:yyyy-MM-dd}']");
            var checkOutCell = By.CssSelector($"span[role='button'][data-date='{checkOut:yyyy-MM-dd}']");

            _elements.ClickWithScroll(checkInCell, 10);
            _elements.ClickWithScroll(checkOutCell, 10);
        }

        public void SetOccupancy(int adults, int children, int rooms, int defaultChildAge = 5)
        {
            _elements.ClickFirstExisting(10, _openOccupancy);

            SetAdultsCount(adults);
            SetChildrenCount(children);
            SetRoomsCount(rooms);

            if (children > 0)
                SetChildrenAge(defaultChildAge);
        }

        public void ChangeLanguage(string langCode)
        {
            _elements.Click(_languagePickerTrigger, 10);
            _elements.ClickWithScroll(LanguageOption(langCode), 10);
        }

        public string GetHeroBannerTitle()
        {
            return _elements.GetVisibleElement(_heroBannerTitle, 10).Text;
        }

        public bool IsRegisterFocused()
        {
            return _elements.IsElementFocused(_registerButton);
        }

        public bool IsCurrencyFocused()
        {
            return _elements.IsElementFocused(_currencyTrigger);
        }

        public bool IsCurrencyApplied(string expectedCurrencyCode)
        {
            var trigger = _elements.FindFirstExisting(_currencyTrigger);
            var combined = $"{trigger.Text} {trigger.GetAttribute("aria-label")}".Trim();
            return combined.Contains(expectedCurrencyCode, StringComparison.OrdinalIgnoreCase);
        }

        public bool IsStaysSearchFocused()
        {
            return _elements.IsElementFocused(_destinationInput);
        }

        public bool IsCheckInDateFocused()
        {
            return _elements.IsElementFocused(_checkInDateField);
        }

        public BookingRegisterPage NavigateToRegisterPageUsingKeyboard(int maxTabs = 25)
        {
            for (int i = 0; i < maxTabs; i++)
            {
                if (IsRegisterFocused())
                {
                    _elements.PressEnter();
                    return new BookingRegisterPage(_driver, _waits, _elements);
                }

                _elements.PressTab();
            }

            throw new NoSuchElementException("Register element was not focused after pressing Tab multiple times.");
        }

        public void NavigateToCurrencyWithKeyboard(int maxTabs = 20)
        {
            for (int i = 0; i < maxTabs; i++)
            {
                if (IsCurrencyFocused())
                    return;

                _elements.PressTab();
            }

            throw new NoSuchElementException("Currency dropdown was not focused after pressing Tab multiple times.");
        }

        public void OpenCurrencyDropdownWithKeyboard(int maxTabs = 20)
        {
            NavigateToCurrencyWithKeyboard(maxTabs);
            _elements.PressEnter();

            if (!_waits.WaitForExists(_currencyOptions, 10))
                throw new NoSuchElementException("Currency dropdown options did not display.");
        }

        public string SelectLastCurrencyOptionWithKeyboard(int maxTabs = 20)
        {
            OpenCurrencyDropdownWithKeyboard(maxTabs);

            var optionCount = _elements.CountFirstExisting(_currencyOptions);

            if (optionCount <= 0)
                throw new NoSuchElementException("Currency options were not found.");

            for (int i = 1; i < optionCount; i++)
                _elements.PressArrowDown();

            var selectedCurrencyCode = ExtractCurrencyCode(_elements.GetActiveElementText());

            _elements.PressEnter();

            _waits.WaitUntil(() => IsCurrencyApplied(selectedCurrencyCode), 10);

            return selectedCurrencyCode;
        }

        public void WaitUntilCurrencyApplied(string expectedCurrencyCode, int timeoutSeconds = 10)
        {
            _waits.WaitUntil(() => IsCurrencyApplied(expectedCurrencyCode), timeoutSeconds);
        }

        public SearchHotelsPage StartSearchWithKeyboard()
        {
            _elements.PressEnter();
            return new SearchHotelsPage(_driver, _waits, _elements);
        }

        public void NavigateToStaysSearchWithKeyboard(int maxTabs = 15)
        {
            for (int i = 0; i < maxTabs; i++)
            {
                if (_elements.IsElementFocused(_destinationInput))
                    return;

                _elements.PressTab();
            }

            throw new NoSuchElementException(
                "Stays search bar was not focused after pressing Tab multiple times.");
        }

        public void ClearDestinationWithKeyboard()
        {
            new Actions(_driver)
                .KeyDown(Keys.Control)
                .SendKeys("a")
                .KeyUp(Keys.Control)
                .SendKeys(Keys.Backspace)
                .Perform();
        }

        public void TypeDestinationAndTabWithKeyboard(string location)
        {
            ClearDestinationWithKeyboard();
            _elements.TypeIntoActiveElementSlowly(location);
            _waits.WaitForExists(_autocompleteFirstItem[0], 10);
            _elements.PressTab();
        }

        public void TypeDestinationWithKeyboard(string location)
        {
            ClearDestinationWithKeyboard();
            _elements.TypeIntoActiveElementSlowly(location);
            _waits.WaitForExists(_autocompleteFirstItem[0], 10);
        }

        public void SelectAutocompleteWithKeyboard(int arrowDownPresses = 1)
        {
            for (int i = 0; i < arrowDownPresses; i++)
                _elements.PressArrowDown();

            _elements.PressEnter();
        }

        public bool IsAutocompleteSuggestionsDisplayed()
        {
            return _waits.WaitForExists(_autocompleteFirstItem[0], 5);
        }

        public string GetDestinationValue()
        {
            var el = _elements.FindFirstExisting(_destinationInput);
            return el.GetAttribute("value") ?? el.Text;
        }

        public void NavigateToCheckInDateWithKeyboard()
        {
            _elements.PressTab();
            _elements.FocusElementWithJs(_checkInDateField[2]);
        }

        public void SelectCheckInDateWithKeyboard(int arrowRightPresses = 3)
        {
            _elements.PressEnter();
            _waits.WaitForExists(_calendarDay, 10);
            _elements.ScrollAndClickFirstWithJs(_currentCalendarDate);
            _waits.WaitUntil(() => _elements.IsElementFocused(_currentCalendarDate), 5);

            for (int i = 0; i < arrowRightPresses; i++)
                _elements.PressArrowRight();

            _elements.PressEnter();
        }

        public void SelectGuestsWithKeyboard(int arrowDownPresses = 0)
        {
            for (int i = 0; i < arrowDownPresses; i++)
                _elements.PressArrowDown();

            _elements.FocusElementWithJs(_searchButton[0]);
        }

        public void ReloadPage()
        {
            _driver.Navigate().Refresh();
            _waits.WaitUntil(() => IsLoaded(), 10);
        }

        public bool IsSkipToMainContentDisplayed()
        {
            try
            {
                var el = _elements.FindFirstExisting(_skipToMainContent);
                return el.Displayed;
            }
            catch
            {
                return false;
            }
        }

        public void ClickSkipToMainContentWithJs()
        {
            var el = _elements.FindFirstExisting(_skipToMainContent);
            var js = (IJavaScriptExecutor)_driver;
            js.ExecuteScript("arguments[0].click();", el);
        }

        public bool IsNavigationMenuDisplayed()
        {
            try
            {
                var el = _elements.FindFirstExisting(_navigationMenu);
                var js = (IJavaScriptExecutor)_driver;
                return (bool)js.ExecuteScript(
                    "var rect = arguments[0].getBoundingClientRect();" +
                    "return rect.top < window.innerHeight && rect.bottom > 0;",
                    el);
            }
            catch
            {
                return false;
            }
        }

        public void PressShiftTab()
        {
            _elements.PressShiftTab();
        }

        public SearchHotelsPage ClickSearch()
        {
            _elements.ClickFirstExisting(10, _searchButton);
            return new SearchHotelsPage(_driver, _waits, _elements);
        }

        public AttractionsSearchPage ClickAttractionsMenu()
        {
            _elements.ClickFirstExisting(10, _attractionsMenu);
            return new AttractionsSearchPage(_driver, _waits, _elements);
        }

        public CarRentalsPage ClickCarRentalsMenu()
        {
            _elements.ClickFirstExisting(10, _carRentalMenu);
            return new CarRentalsPage(_driver, _waits, _elements);
        }

        public FlightsPage ClickFlightsMenu()
        {
            _elements.ClickFirstExisting(10, _flightsMenu);
            return new FlightsPage(_driver, _waits, _elements);
        }

        public AirportTaxiPage ClickAirportTaxisMenu()
        {
            _elements.ClickFirstExisting(10, _airportTaxisMenu);
            return new AirportTaxiPage(_driver, _waits, _elements);
        }

        public BookingLoginPage ClickSignInButton()
        {
            _elements.ClickFirstExisting(10, _signInButton);
            return new BookingLoginPage(_driver, _waits, _elements);
        }

        private By LanguageOption(string langCode)
        {
            return By.CssSelector($"[data-testid='selection-item'][lang='{langCode}']");
        }

        private void SetAdultsCount(int adults)
        {
            var current = GetCounterValue(_adultsCount);

            while (current != adults)
            {
                if (current < adults)
                    _elements.ClickFirstExisting(10, _incAdults);
                else
                    _elements.ClickFirstExisting(10, _decAdults);

                WaitCounterValue(_adultsCount, adults, 10);
                current = GetCounterValue(_adultsCount);
            }
        }

        private void SetChildrenCount(int children)
        {
            var current = GetCounterValue(_childrenCount);

            while (current != children)
            {
                if (current < children)
                    _elements.ClickFirstExisting(10, _incChildren);
                else
                    _elements.ClickFirstExisting(10, _decChildren);

                WaitCounterValue(_childrenCount, children, 10);
                current = GetCounterValue(_childrenCount);
            }
        }

        private void SetRoomsCount(int rooms)
        {
            var current = GetCounterValue(_roomsCount);

            while (current != rooms)
            {
                if (current < rooms)
                    _elements.ClickFirstExisting(10, _incRooms);
                else
                    _elements.ClickFirstExisting(10, _decRooms);

                WaitCounterValue(_roomsCount, rooms, 10);
                current = GetCounterValue(_roomsCount);
            }
        }

        private int GetCounterValue(By[] locators)
        {
            var el = _elements.FindFirstExisting(locators);

            string raw;

            if (el.TagName.Equals("input", StringComparison.OrdinalIgnoreCase))
            {
                raw = el.GetAttribute("aria-valuenow");
                if (string.IsNullOrWhiteSpace(raw))
                    raw = el.GetAttribute("value");
            }
            else
            {
                raw = el.Text;
            }

            raw = (raw ?? "").Trim();

            if (int.TryParse(raw, out int value))
                return value;

            throw new FormatException($"Cannot parse counter value from '{raw}'.");
        }

        private void WaitCounterValue(By[] locators, int expected, int timeoutSeconds)
        {
            _waits.WaitUntil(() =>
            {
                try { return GetCounterValue(locators) == expected; }
                catch { return false; }
            }, timeoutSeconds);
        }

        private void SetChildrenAge(int defaultChildAge)
        {
            var selects = FindAnyElements(_kidsAgeSelect);

            foreach (var selectEl in selects)
            {
                try
                {
                    new SelectElement(selectEl).SelectByValue(defaultChildAge.ToString());
                }
                catch { }
            }
        }

        private IReadOnlyCollection<IWebElement> FindAnyElements(By[] locators)
        {
            foreach (var by in locators)
            {
                var els = _driver.FindElements(by);
                if (els.Count > 0) return els;
            }

            return Array.Empty<IWebElement>();
        }

        private string ExtractCurrencyCode(string rawText)
        {
            if (string.IsNullOrWhiteSpace(rawText))
                throw new InvalidOperationException("Text is empty.");

            var parts = rawText.Split((char[])null, StringSplitOptions.RemoveEmptyEntries);

            foreach (var part in parts)
            {
                if (part.Length == 3 && part.All(char.IsLetter) && part.All(char.IsUpper))
                    return part;
            }

            throw new InvalidOperationException($"Cannot extract currency code from text: '{rawText}'.");
        }
    }
}