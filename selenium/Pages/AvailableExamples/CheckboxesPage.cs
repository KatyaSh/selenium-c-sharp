using OpenQA.Selenium;

public class CheckboxesPage
{
    private IWebDriver driver;
    private readonly By checkbox1 = By.XPath("//form/input[1]");
    private readonly By checkbox2 = By.XPath("//form/input[2]");
    private readonly By checkboxesPageHeader = By.XPath($"//div/h3[contains(text(), 'Checkboxes')]");

    public CheckboxesPage(IWebDriver driver)
    {
        this.driver = driver;
    }

    public void ClickCheckbox(By checkboxLocator)
    {
        driver.FindElement(checkboxLocator).Click();
    }

    public void ClickCheckbox1()
    {
        ElementUtils.ClickElementByXPath(driver, checkbox1, 20);
    }

    public void ClickCheckbox2()
    {
        ElementUtils.ClickElementByXPath(driver, checkbox2, 20);
    }

    public bool IsCheckbox1Selected()
    {
        return ElementUtils.IsElementSelected(driver, checkbox1);
    }

    public bool IsCheckbox2Selected()
    {
        return ElementUtils.IsElementSelected(driver, checkbox2);
    }

    public bool IsCheckboxesPageOpened()
    {
        return WaitUtils.WaitForElementVisible(driver, checkboxesPageHeader, 20).Displayed;
    }

    public bool IsCheckbox1Displayed()
    {
        return WaitUtils.WaitForElementVisible(driver, checkbox1, 20).Displayed;
    }

    public bool IsCheckbox2Displayed()
    {
        return WaitUtils.WaitForElementVisible(driver, checkbox2, 20).Displayed;
    }
}
