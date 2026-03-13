using OpenQA.Selenium;
using System.Globalization;

public class AttractionDetailsPage : BasePage
{
    private readonly By _ticketForm =
        By.CssSelector("form[data-testid='inline-ticket-config']");

    private readonly By _dateItems =
        By.CssSelector("button[data-testid='datepicker-item']");

    private readonly By _timeSlots =
        By.CssSelector("input[data-testid='timeslot-option']");

    public AttractionDetailsPage(IWebDriver driver, Waits waits, ElementActions elements)
        : base(driver, waits, elements)
    {
    }

    private By DateButtonByAria(string ariaLabel)
    {
        return By.XPath(
            $"//button[@data-testid='datepicker-item' and contains(@aria-label,'{ariaLabel}')]"
        );
    }

    public void WaitUntilLoaded()
    {
        _waits.WaitForExists(_ticketForm, 20);
        var ticketForm = _elements.GetVisibleElement(_ticketForm, 5);
        _elements.ScrollIntoView(ticketForm);

    }

    public bool HasDate(DateTime date)
    {
        _waits.WaitForExists(_dateItems, 10);

        var aria = date.ToString(
            "ddd, MMM d",
            CultureInfo.InvariantCulture
        );

        return _waits.WaitForExists(DateButtonByAria(aria), 3);
    }

    public bool HasAnyTimeSlot()
    {
        return _waits.WaitForExists(_timeSlots, 10);
    }
}

