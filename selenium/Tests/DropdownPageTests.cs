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

        var select = dropdownPage.SelectDropdown();
        select.SelectByText("Option 2");

        string selectedText = select.SelectedOption.Text;
        Assert.That(selectedText, Is.EqualTo("Option 2"), "Selected option is not correct");
    }

    [Test]
    public void SelectTwoOptionsFromDropdown()
    {
        var availableExamplePage = new AvailableExamplesPage(driver);
        var dropdownPage = new DropdownPage(driver);

        availableExamplePage.OpenExamplePage("Dropdown");
        Assert.That(dropdownPage.IsDropdownPageOpened(), Is.True, "Dropdown Page is not opened");

        var select = dropdownPage.SelectDropdown();
        select.SelectByText("Option 1");
        string selectedText1 = select.SelectedOption.Text;
        Assert.That(selectedText1, Is.EqualTo("Option 1"), "Selected option is not correct");

        select.SelectByText("Option 2");
        string selectedText2 = select.SelectedOption.Text;
        Assert.That(selectedText2, Is.EqualTo("Option 2"), "Selected option is not correct");
    }

    [Test]
    public void GetListAvailableOptions()
    {
        var availableExamplePage = new AvailableExamplesPage(driver);
        var dropdownPage = new DropdownPage(driver);

        availableExamplePage.OpenExamplePage("Dropdown");
        Assert.That(dropdownPage.IsDropdownPageOpened(), Is.True, "Dropdown Page is not opened");

        var select = dropdownPage.SelectDropdown();
        var options = select.Options;
        Assert.That(options.Count, Is.EqualTo(3), "Number of available options is not correct");
    }



    //        var select = new SelectElement(selectElement);

    //        select.SelectByText("Option 2");
    //        Assert.That(select.SelectedOption.Text, Is.EqualTo("Option 2"));
}

