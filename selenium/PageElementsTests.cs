using NUnit.Framework;
using NUnit.Framework.Internal;
using OpenQA.Selenium;
using SeleniumExtras.WaitHelpers;
using OpenQA.Selenium.Support.UI;

public class PageElementsTests : BrowserActions_BaseTest
{
    [Test]
    public void HandlingSimpleAlert()
    {
        ClickLinkByText("JavaScript Alerts");

        ClickElementByXPath("//button[@onclick = 'jsAlert()']");

        IAlert alert = webDriverWait.Until(ExpectedConditions.AlertIsPresent());

        Assert.That(alert, Is.Not.Null, "Alert is not displayed");

        alert.Accept();

        var result = WaitForElement(By.XPath("//p[text()='You successfully clicked an alert']"));
        Assert.That(result.Text, Is.EqualTo("You successfully clicked an alert"), "wrong result text");
    }

    [Test]
    public void HandlingConfirmationAlert()
    {
        ClickLinkByText("JavaScript Alerts");

        ClickElementByXPath("//button[@onclick = 'jsConfirm()']");

        IAlert alert = webDriverWait.Until(ExpectedConditions.AlertIsPresent());

        Assert.That(alert, Is.Not.Null, "Alert is not displayed");

        alert.Dismiss();

        var result = WaitForElement(By.XPath("//p[text()='You clicked: Cancel']"));
        Assert.That(result.Text, Is.EqualTo("You clicked: Cancel"), "wrong result text");
    }

    [Test]
    public void HandlingPromptAlert()
    {
        ClickLinkByText("JavaScript Alerts");

        ClickElementByXPath("//button[@onclick = 'jsPrompt()']");

        IAlert alert = webDriverWait.Until(ExpectedConditions.AlertIsPresent());

        Assert.That(alert, Is.Not.Null, "Alert is not displayed");

        IAlert promptAlert = driver.SwitchTo().Alert();
        var myInput = "my input";
        promptAlert.SendKeys(myInput);
        promptAlert.Accept();

        var result = WaitForElement(By.XPath($"//p[text()='You entered: {myInput}']"));
        Assert.That(result.Text, Is.EqualTo($"You entered: {myInput}"), "wrong result text");
    }

    [Test]
    public void SwitchingToIframe()
    {
        ClickLinkByText("Frames");

        ClickElementByXPath("//a[text()='iFrame']");

        WaitForElement(By.XPath($"//div/h3[contains(text(), 'An iFrame containing the TinyMCE WYSIWYG Editor')]"));

        var closeButton = WaitForElement(By.XPath("//div[@aria-label='Close']"));
        closeButton.Click();

        IWebElement iframeElement = driver.FindElement(By.CssSelector("iframe#mce_0_ifr"));
        driver.SwitchTo().Frame(iframeElement);

        var paragraph = driver.FindElement(By.XPath("//p[text()='Your content goes here.']"));
        Assert.That(paragraph.Text, Is.EqualTo("Your content goes here."));
    }

    [Test]
    public void InteractingWithSelectElement()
    {
        ClickLinkByText("Dropdown");

        WaitForElement(By.XPath($"//div/h3[contains(text(), 'Dropdown List')]"));

        var selectElement = WaitForElement(By.Id("dropdown"));
        var select = new SelectElement(selectElement);

        select.SelectByText("Option 2");
        Assert.That(select.SelectedOption.Text, Is.EqualTo("Option 2"));

        var selectedOption = select.SelectedOption;
        Assert.That(selectedOption.Displayed, Is.True, "Selected option is not displayed in the dropdown");
    }

    [Test]
    public void InteractingWithCheckboxElement()
    {
        ClickLinkByText("Checkboxes");

        WaitForElement(By.XPath($"//div/h3[contains(text(), 'Checkboxes')]"));

        IWebElement firstCheckbox = driver.FindElement(By.XPath("//form[@id='checkboxes']/input[1]"));
        firstCheckbox.Click();
        Assert.That(firstCheckbox.Selected, Is.True, "Checkbox 1 is not checked after clicking.");
    }

    [Test]
    public void InteractingWithRangeElement()
    {
        ClickLinkByText("Horizontal Slider");

        WaitForElement(By.XPath("//div/h3[contains(text(), 'Horizontal Slider')]"));

        var desiredValue = "3.5";

        IWebElement rangeInput = driver.FindElement(By.XPath("//input[@type='range']"));
        ((IJavaScriptExecutor)driver).ExecuteScript($"arguments[0].value = {desiredValue}; arguments[0].dispatchEvent(new Event('change'));", rangeInput);

        IWebElement displayedValue = driver.FindElement(By.Id("range"));

        Assert.That(displayedValue.Text, Is.EqualTo(desiredValue), "Displayed value does not match entered value.");
    }

    [Test]
    public void InteractingWithTextInputElement()
    {
        ClickLinkByText("Inputs");

        WaitForElement(By.XPath("//div/h3[contains(text(), 'Inputs')]"));

        IWebElement inputField = driver.FindElement(By.XPath("//input[@type='number']"));
        var inputText = "15";
        inputField.Clear();
        inputField.SendKeys(inputText);

        var displayedValue = inputField.GetAttribute("value");

        Assert.That(displayedValue, Is.EqualTo(inputText), "Entered text does not match displayed text in the input field.");
    }

    [Test]
    public void InteractingWithBasicAuth()
    {
        driver.Navigate().GoToUrl("http://admin:admin@the-internet.herokuapp.com/basic_auth");

        WaitForElement(By.XPath("//div/h3[contains(text(), 'Basic Auth')]"));

        var successMessage = WaitForElement(By.XPath("//p[contains(text(), 'Congratulations! You must have the proper credentials.')]"));
        Assert.That(successMessage.Displayed, Is.True, "Success message is not displayed. Basic Auth failed.");
    }

    [Test]
    public void DownloadingFile()
    {
        ClickLinkByText("File Download");

        WaitForElement(By.XPath("//div/h3[contains(text(), 'File Downloader')]"));

        var expectedFileName = "testing.jpeg";
        var downloadDirectory = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
            "Downloads"
        );
        var filePath = Path.Combine(downloadDirectory, expectedFileName);

        ClickElementByXPath("//a[@href='download/testing.jpeg']");

        var fileExists = false;
        var timeoutSeconds = 10;
        for (int i = 0; i < timeoutSeconds; i++)
        {
            if (File.Exists(filePath))
            {
                fileExists = true;
                break;
            }
            Thread.Sleep(1000);
        }

        Assert.That(fileExists, Is.True, $"Downloaded file '{expectedFileName}' was not found in '{downloadDirectory}'.");
    }

    [Test]
    public void ValidateDataSortedByFirstName()
    {
        ClickLinkByText("Sortable Data Tables");

        WaitForElement(By.XPath("//div/h3[contains(text(), 'File Downloader')]"));
    }
}


