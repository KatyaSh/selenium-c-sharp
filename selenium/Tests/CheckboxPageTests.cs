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

        Assert.That(checkboxesPage.IsCheckboxDisplayed(0), Is.True, "Checkbox should be displayed");
        Assert.That(checkboxesPage.IsCheckboxDisplayed(1), Is.True, "Checkbox should be displayed");

        checkboxesPage.Check(0);
        Assert.That(checkboxesPage.IsCheckboxSelected(0), Is.True, "Checkbox should be checked");

        checkboxesPage.Uncheck(0);
        Assert.That(checkboxesPage.IsCheckboxSelected(0), Is.False, "Checkbox should be unchecked");
    }

    [Test]
    public void ToggleBothCheckbox()
    {
        var availableExamplesPage = new AvailableExamplesPage(driver);
        var checkboxesPage = new CheckboxesPage(driver);

        availableExamplesPage.OpenExamplePage("Checkboxes");
        Assert.That(checkboxesPage.IsCheckboxesPageOpened(), Is.True, "Checkboxes page is not opened");

        Assert.That(checkboxesPage.IsCheckboxDisplayed(0), Is.True, "Checkbox should be displayed");
        Assert.That(checkboxesPage.IsCheckboxDisplayed(1), Is.True, "Checkbox should be displayed");

        checkboxesPage.Check(0);
        Assert.That(checkboxesPage.IsCheckboxSelected(0), Is.True, "Checkbox should be checked");

        checkboxesPage.Check(1);
        Assert.That(checkboxesPage.IsCheckboxSelected(1), Is.True, "Checkbox should be checked");
    }

    [Test]
    public void UncheckBothCheckboxes() 
    {
        var availableExamplesPage = new AvailableExamplesPage(driver);
        var checkboxesPage = new CheckboxesPage(driver);
        availableExamplesPage.OpenExamplePage("Checkboxes");
        Assert.That(checkboxesPage.IsCheckboxesPageOpened(), Is.True, "Checkboxes page is not opened");

        Assert.That(checkboxesPage.IsCheckboxDisplayed(0), Is.True, "Checkbox should be displayed");
        Assert.That(checkboxesPage.IsCheckboxDisplayed(1), Is.True, "Checkbox should be displayed");

        checkboxesPage.Check(0);
        Assert.That(checkboxesPage.IsCheckboxSelected(0), Is.True, "Checkbox should be checked");

        checkboxesPage.Uncheck(0);
        Assert.That(checkboxesPage.IsCheckboxSelected(0), Is.False, "Checkbox should be unchecked"); 
        
        checkboxesPage.Check(1);
        Assert.That(checkboxesPage.IsCheckboxSelected(1), Is.True, "Checkbox should be checked");

        checkboxesPage.Uncheck(1);
        Assert.That(checkboxesPage.IsCheckboxSelected(1), Is.False, "Checkbox should be unchecked");
    }

    [Test]
    public void ToggleCheckboxesRepeatedly()
    {
        var availableExamplesPage = new AvailableExamplesPage(driver);
        var checkboxesPage = new CheckboxesPage(driver);
        availableExamplesPage.OpenExamplePage("Checkboxes");
        Assert.That(checkboxesPage.IsCheckboxesPageOpened(), Is.True, "Checkboxes page is not opened");

        Assert.That(checkboxesPage.IsCheckboxDisplayed(0), Is.True, "Checkbox should be displayed");
        Assert.That(checkboxesPage.IsCheckboxDisplayed(1), Is.True, "Checkbox should be displayed");

        checkboxesPage.Check(0);
        Assert.That(checkboxesPage.IsCheckboxSelected(0), Is.True, "Checkbox 1 is not checked after clicking");

        checkboxesPage.Uncheck(0);
        Assert.That(checkboxesPage.IsCheckboxSelected(0), Is.False, "Checkbox 1 is not unchecked after clicking");

        checkboxesPage.Check(0);
        Assert.That(checkboxesPage.IsCheckboxSelected(0), Is.True, "Checkbox 1 is not checked after clicking");

        checkboxesPage.Uncheck(0);
        Assert.That(checkboxesPage.IsCheckboxSelected(0), Is.False, "Checkbox 1 is not unchecked after clicking");
    }
}

