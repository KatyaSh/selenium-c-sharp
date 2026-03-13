
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System.Xml.Linq;

namespace Booking
{
    public class HomePage : BasePage
    {
        private readonly By _cookiesAccept = By.XPath("//button[@id='onetrust-accept-btn-handler']");
        private readonly By _banner = By.XPath("//button[@aria-label='Dismiss sign-in info.']");

        private readonly By[] _destinationInput =
        {
            By.CssSelector("input[data-testid='searchbox-destination-input']"),
            By.CssSelector("[data-testid='searchbox-destination'] input"),
            By.CssSelector("input[name='ss']")
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
            By.CssSelector("button[data-testid='searchbox-submit-button']"),
            By.CssSelector("[data-testid='searchbox-submit-button']"),
            By.CssSelector("button[type='submit']")
        };

        private readonly By _attractionsMenu = By.CssSelector("#attractions");

        private readonly By _carRentalMenu = By.CssSelector("#cars");

        private readonly By _flightsMenu = By.CssSelector("#flights");

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

        private readonly By _languagePickerTrigger =
        By.CssSelector("[data-testid='header-language-picker-trigger']");

        private By LanguageOption(string langCode)
        {
            return By.CssSelector($"[data-testid='selection-item'][lang='{langCode}']");
        }

        private readonly By _heroBannerTitle =
            By.CssSelector("[data-testid='herobanner-title1']");

        private readonly By _airportTaxisMenu = By.CssSelector("#airport_taxis");

        private readonly By _signInButton = By.CssSelector("[data-testid='header-sign-in-button'][aria-label='Sign in']");

        public HomePage(IWebDriver driver, Waits waits, ElementActions elements)
            : base(driver, waits, elements)
        {
        }

        public void CloseBaner()
        {
            _elements.Click(_banner, 10);
        }

        public void AcceptCookiesIfPresent()
        {
            _elements.Click(_cookiesAccept, 10);
        }

        public void SetDestination(string destination, bool selectFromAutocomplete = true)
        {
            _elements.TypeIntoFirstExisting(destination, clear: true, timeoutSeconds: 5, _destinationInput);

            if (selectFromAutocomplete)
            {
                _waits.WaitForExists(_autocompleteFirstItem[0], 10);

                var clicked = _elements.TryClickFirstExisting(5, _autocompleteFirstItem);

                if (!clicked)
                {
                    _elements.FindFirstExisting(_destinationInput).SendKeys(Keys.Enter);
                }
            }
        }

        public void OpenCalendar()
        {
            _elements.ClickWithScroll(_openCalendar, 5);
        }

        public void SelectDates(DateTime checkIn, DateTime checkOut)
        {
            var inStr = checkIn.ToString("yyyy-MM-dd");
            var outStr = checkOut.ToString("yyyy-MM-dd");

            var checkInCell = By.CssSelector($"span[role='button'][data-date='{inStr}']");
            var checkOutCell = By.CssSelector($"span[role='button'][data-date='{outStr}']");

            _elements.ClickWithScroll(checkInCell, 10);
            _elements.ClickWithScroll(checkOutCell, 10);
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

        public void SetOccupancy(int adults, int children, int rooms, int defaultChildAge = 5)
        {
            _elements.ClickFirstExisting(10, _openOccupancy);

            SetAdultsCount(adults);
            SetChildrenCount(children);
            SetRoomsCount(rooms);

            if (children > 0)
            {
                SetChildrenAge(defaultChildAge);
            }
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
            IWebElement el = _elements.FindFirstExisting(locators);

            var raw = "";

            if (el.TagName.Equals("input", StringComparison.OrdinalIgnoreCase))
            {
                // сначала пробуем aria-valuenow, потом value
                raw = el.GetAttribute("aria-valuenow");

                if (string.IsNullOrWhiteSpace(raw))
                {
                    raw = el.GetAttribute("value");
                }
            }
            else
            {
                raw = el.Text;
            }

            raw = (raw ?? "").Trim();

            if (int.TryParse(raw, out int value))
            {
                return value;
            }

            throw new FormatException($"Cannot parse counter value from '{raw}'.");
        }

        private void WaitCounterValue(By[] locators, int expected, int timeoutSeconds)
        {
            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(timeoutSeconds));

            wait.Until(driver =>
            {
                try
                {
                    var current = GetCounterValue(locators);
                    var isEqual = current == expected;
                    return isEqual;
                }
                catch
                {
                    return false;
                }
            });
        }

        private void SetChildrenAge(int defaultChildAge)
        {
            IReadOnlyCollection<IWebElement> selects = FindAnyElements(_kidsAgeSelect);

            foreach (var selectEl in selects)
            {
                try
                {
                    var select = new SelectElement(selectEl);
                    select.SelectByValue(defaultChildAge.ToString());
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

        public void ChangeLanguage(string langCode)
        {
            _elements.Click(_languagePickerTrigger, 10);
            _elements.ClickWithScroll(LanguageOption(langCode), 10);
        }

        public string GetHeroBannerTitle()
        {
            return _elements.GetVisibleElement(_heroBannerTitle, 10).Text;
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
    }
}
    

