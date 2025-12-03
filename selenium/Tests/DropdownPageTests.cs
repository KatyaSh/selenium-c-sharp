using NUnit.Framework;
using NUnit.Framework.Internal;
public class DropdownPageTests : BrowserActions_BaseTest
{
    [Test]
    public void SelectOptionFromDropdown()
    {
        var availableExamplePage = new AvailableExamplesPage(driver);
        var dropdownPage = new DropdownPage(driver);

        availableExamplePage.OpenExamplePage("Dropdown");
        Assert.That(dropdownPage.IsDropdownPageOpened(), Is.True, "Dropdown Page is not opened");

        dropdownPage.SelectByText("Option 2");

        var  selected = dropdownPage.GetSelectedOptionText();
        Assert.That(selected, Is.EqualTo("Option 2"), "Selected option is not correct");
    }

    [Test]
    public void SelectTwoOptionsFromDropdown()
    {
        var availableExamplePage = new AvailableExamplesPage(driver);
        var dropdownPage = new DropdownPage(driver);

        availableExamplePage.OpenExamplePage("Dropdown");
        Assert.That(dropdownPage.IsDropdownPageOpened(), Is.True, "Dropdown Page is not opened");

        dropdownPage.SelectByText("Option 1");
        var selectedText1 = dropdownPage.GetSelectedOptionText(); 
        Assert.That(selectedText1, Is.EqualTo("Option 1"), "Selected option is not correct");

        dropdownPage.SelectByText("Option 2");
        var selectedText2 = dropdownPage.GetSelectedOptionText();
        Assert.That(selectedText2, Is.EqualTo("Option 2"), "Selected option is not correct");
    }

    [Test]
    public void GetListAvailableOptions()
    {
        var availableExamplePage = new AvailableExamplesPage(driver);
        var dropdownPage = new DropdownPage(driver);

        availableExamplePage.OpenExamplePage("Dropdown");
        Assert.That(dropdownPage.IsDropdownPageOpened(), Is.True, "Dropdown Page is not opened");

        var options = dropdownPage.GetSelectDropdownOptions();
        Assert.That(options.Count, Is.EqualTo(3), "Dropdown options count is incorrect");
    }
}

