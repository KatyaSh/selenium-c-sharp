using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

public class DropdownPage
{
    private IWebDriver driver;
    private readonly By dropdownPageHeader = By.XPath("//div[@class='example']");
    private readonly By dropdownSelect = By.Id("dropdown");

    public DropdownPage(IWebDriver driver)
    {
        this.driver = driver;
    }

    public SelectElement SelectDropdown()
    {
        return ElementUtils.GetDropdown(driver, dropdownSelect, 20);
    }

    public bool IsDropdownPageOpened()
    {
        return WaitUtils.WaitForElementIsVisible(driver, dropdownPageHeader, 20).Displayed;
    }

    public void SelectByText(string text)
    {
        SelectDropdown().SelectByText(text);
    }

    public string GetSelectedOptionText()
    {
        return SelectDropdown().SelectedOption.Text;
    }

    public List<string> GetSelectDropdownOptions()
    {
        var select = SelectDropdown();
        return select.Options.Select(o => o.Text).ToList();
    }
}