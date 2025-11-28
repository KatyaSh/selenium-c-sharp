using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

public class DropdownPage
{
    private IWebDriver driver;
    private readonly By dropdownPageHeader = By.XPath($"//div/h3[contains(text(), 'Dropdown List')]");
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
        return WaitUtils.WaitForElementVisible(driver, dropdownPageHeader, 20).Displayed;
    }
}

