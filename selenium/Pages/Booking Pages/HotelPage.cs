using OpenQA.Selenium;

public class HotelPage : BasePage
{
    private readonly By _roomsTable = By.CssSelector("table#hprt-table");

    private readonly By _roomCountSelect = By.CssSelector("select[data-testid='select-room-trigger']");

    private readonly By[] _reserveButton =
    {
        By.CssSelector("button.js-reservation-button"),
        By.CssSelector("button[type='submit'].js-reservation-button"),
        By.XPath("//button[contains(@class,'js-reservation-button')]")
    };

    public HotelPage(IWebDriver driver, Waits waits, ElementActions elements)
        : base(driver, waits, elements)
    {
    }

    public void WaitUntilLoaded()
    {
        _waits.WaitForExists(_roomsTable, 30);
    }

    public void SelectOneRoom()
    {
        var selectEl = _elements.GetVisibleElement(_roomCountSelect, 10);
        var select = new OpenQA.Selenium.Support.UI.SelectElement(selectEl);
        select.SelectByValue("1");
    }

    public BookingDetailsPage ClickReserve()
    {
        _elements.ClickFirstExisting(10, _reserveButton);
        return new BookingDetailsPage(_driver, _waits, _elements);
    }
}