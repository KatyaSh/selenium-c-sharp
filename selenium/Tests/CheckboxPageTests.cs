using NUnit.Framework;
using NUnit.Framework.Internal;
public class CheckboxPageTests : BrowserActions_BaseTest
{
    [Test]
    public void Toggle1Checkbox()
    {
        var availableExamplesPage = new AvailableExamplesPage(driver);
        var checkboxesPage = new CheckboxesPage(driver);

        availableExamplesPage.OpenExamplePage("Checkboxes");
        Assert.That(checkboxesPage.IsCheckboxesPageOpened(), Is.True, "Checkboxes page is not opened");

        Assert.That(checkboxesPage.IsCheckbox1Displayed, Is.True, "Checkbox 1 is not displayed");
        Assert.That(checkboxesPage.IsCheckbox2Displayed, Is.True, "Checkbox 2 is not displayed");

        if (!checkboxesPage.IsCheckbox1Selected())
            checkboxesPage.ClickCheckbox1();
        Assert.That(checkboxesPage.IsCheckbox1Selected(), Is.True, "Checkbox 1 is not checked after clicking");

        if (checkboxesPage.IsCheckbox1Selected())
            checkboxesPage.ClickCheckbox1();
        Assert.That(checkboxesPage.IsCheckbox1Selected(), Is.False, "Checkbox 1 is not unchecked after clicking again");
    }

    [Test]
    public void ToggleBothCheckbox()
    {
        var availableExamplesPage = new AvailableExamplesPage(driver);
        var checkboxesPage = new CheckboxesPage(driver);

        availableExamplesPage.OpenExamplePage("Checkboxes");
        Assert.That(checkboxesPage.IsCheckboxesPageOpened(), Is.True, "Checkboxes page is not opened");

        Assert.That(checkboxesPage.IsCheckbox1Displayed, Is.True, "Checkbox 1 is not displayed");
        Assert.That(checkboxesPage.IsCheckbox2Displayed, Is.True, "Checkbox 2 is not displayed");

        if (!checkboxesPage.IsCheckbox1Selected())
            checkboxesPage.ClickCheckbox1();
        Assert.That(checkboxesPage.IsCheckbox1Selected(), Is.True, "Checkbox 1 is not checked after clicking");

        if (!checkboxesPage.IsCheckbox2Selected())
            checkboxesPage.ClickCheckbox2();
        Assert.That(checkboxesPage.IsCheckbox2Selected(), Is.True, "Checkbox 2 is not checked after clicking");
    }

    [Test]
    public void UncheckBothCheckboxes() 
    {
        var availableExamplesPage = new AvailableExamplesPage(driver);
        var checkboxesPage = new CheckboxesPage(driver);
        availableExamplesPage.OpenExamplePage("Checkboxes");
        Assert.That(checkboxesPage.IsCheckboxesPageOpened(), Is.True, "Checkboxes page is not opened");
        Assert.That(checkboxesPage.IsCheckbox1Displayed, Is.True, "Checkbox 1 is not displayed");
        Assert.That(checkboxesPage.IsCheckbox2Displayed, Is.True, "Checkbox 2 is not displayed");

        if (!checkboxesPage.IsCheckbox1Selected())
            checkboxesPage.ClickCheckbox1();
        Assert.That(checkboxesPage.IsCheckbox1Selected(), Is.True, "Checkbox 1 is not checked after clicking");

        if (checkboxesPage.IsCheckbox1Selected())
            checkboxesPage.ClickCheckbox1();
        Assert.That(checkboxesPage.IsCheckbox1Selected(), Is.False, "Checkbox 1 is not unchecked after clicking");

        if (!checkboxesPage.IsCheckbox2Selected())
            checkboxesPage.ClickCheckbox2();
        Assert.That(checkboxesPage.IsCheckbox2Selected(), Is.True, "Checkbox 2 is not checked after clicking");       

        if (checkboxesPage.IsCheckbox2Selected())
            checkboxesPage.ClickCheckbox2();
        Assert.That(checkboxesPage.IsCheckbox2Selected(), Is.False, "Checkbox 2 is not unchecked after clicking");
    }

    [Test]
    public void ToggleCheckboxesRepeatedly()
    {
        var availableExamplesPage = new AvailableExamplesPage(driver);
        var checkboxesPage = new CheckboxesPage(driver);
        availableExamplesPage.OpenExamplePage("Checkboxes");
        Assert.That(checkboxesPage.IsCheckboxesPageOpened(), Is.True, "Checkboxes page is not opened");
        Assert.That(checkboxesPage.IsCheckbox1Displayed, Is.True, "Checkbox 1 is not displayed");
        Assert.That(checkboxesPage.IsCheckbox2Displayed, Is.True, "Checkbox 2 is not displayed");

        if (!checkboxesPage.IsCheckbox1Selected())
            checkboxesPage.ClickCheckbox1();
        Assert.That(checkboxesPage.IsCheckbox1Selected(), Is.True, "Checkbox 1 is not checked after clicking");

        if (checkboxesPage.IsCheckbox1Selected())
            checkboxesPage.ClickCheckbox1();
        Assert.That(checkboxesPage.IsCheckbox1Selected(), Is.False, "Checkbox 1 is not unchecked after clicking");

        if (!checkboxesPage.IsCheckbox1Selected())
            checkboxesPage.ClickCheckbox1();
        Assert.That(checkboxesPage.IsCheckbox1Selected(), Is.True, "Checkbox 1 is not checked after clicking");

        if (checkboxesPage.IsCheckbox1Selected())
            checkboxesPage.ClickCheckbox1();
        Assert.That(checkboxesPage.IsCheckbox1Selected(), Is.False, "Checkbox 1 is not unchecked after clicking");
    }

}

