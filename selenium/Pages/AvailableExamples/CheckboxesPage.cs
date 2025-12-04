using OpenQA.Selenium;

public class CheckboxesPage
{
    private IWebDriver driver;
    private readonly By checkboxesContainer = By.Id("checkboxes");
    private readonly By checkboxInputs = By.CssSelector("input[type='checkbox']");
    private readonly By checkboxesPageHeader = By.XPath("//div[@class='example']");

    public CheckboxesPage(IWebDriver driver)
    {
        this.driver = driver;
    }

    public bool IsCheckboxesPageOpened()
    {
        return WaitUtils.WaitForElementIsVisible(driver, checkboxesPageHeader, 20).Displayed;
    }


    private IWebElement GetCheckbox(int index)
        {
            var container = WaitUtils.WaitForElementIsVisible(driver, checkboxesContainer, 5);
            return container.FindElements(checkboxInputs)[index];
        }
    public bool IsCheckboxDisplayed(int index) => GetCheckbox(index).Displayed;

    public bool IsCheckboxSelected(int index) => GetCheckbox(index).Selected;

    public void ClickCheckbox(int index) => GetCheckbox(index).Click();

    public void SetCheckboxState(int index, bool isSelected)
    {
        var checkbox = GetCheckbox(index);
        if (checkbox.Selected != isSelected)
            checkbox.Click();
    }

    public void Check(int index) => SetCheckboxState(index, true);
    public void Uncheck(int index) => SetCheckboxState(index, false);
}
