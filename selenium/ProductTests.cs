using NUnit.Framework;
using NUnit.Framework.Internal;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System.Diagnostics;

public class ProductTests : BaseTest
{
    [Test]
    public void FirstTest()
    {
        SuccesfullLogin("standard_user", "secret_sauce");

        var productDetails = driver.FindElements(By.CssSelector("[data-test='inventory-item-desc']"));
        var firtsProductText = productDetails[0].Text;
        var productElementsTitles = driver.FindElements(By.XPath("//div[@data-test='inventory-item-name']"));
        productElementsTitles[0].Click();
        WaitForElement(By.Id("back-to-products"));
        var description = driver.FindElement(By.XPath("//div[@data-test='inventory-item-desc']")).Text;

        Assert.That(description, Is.EqualTo(firtsProductText), "Wrong product details text is on product page");
    }

    [Test]
    public void SortingCheck()
    {
        SuccesfullLogin("standard_user", "secret_sauce");        
        var selectElement = new SelectElement(driver.FindElement(By.TagName("select")));
        selectElement.SelectByText("Price (low to high)");
        var productPrices = driver.FindElements(By.CssSelector("[data-test='inventory-item-price']"));
        List<decimal> actualPrices = new List<decimal>();

        foreach (var el in productPrices)
        {
            var raw = el.Text.Replace("$", "").Trim();
            var rawParts = raw.Split('\n');
            var price = rawParts.Last();
            if (decimal.TryParse(price, out var value))
                actualPrices.Add(value);
        }

        var expected = new List<decimal>(actualPrices);
        expected.Sort();

        Assert.That(actualPrices, Is.EqualTo(expected), "ASC sorting is not applied");
    }

    [Test]
    public void AddProductToBasket()
    {
        SuccesfullLogin("standard_user", "secret_sauce");
        var productElementsTitles = driver.FindElements(By.XPath("//div[@data-test='inventory-item-name']"));
        productElementsTitles[0].Click();
        var addToBCartButton = driver.FindElement(By.Name("add-to-cart"));
        addToBCartButton.Click();
        WaitForElement(By.Id("remove"));
        var shoppingCartBadge = driver.FindElement(By.CssSelector("[data-test='shopping-cart-badge']"));
        var actualProductNumber = int.Parse(shoppingCartBadge.Text);
        var expectedProductNumber = 1;
        Assert.That(actualProductNumber, Is.EqualTo(expectedProductNumber), "Product numder in the basket doesn't match");
    }

    [Test]
    public void RemoveProductFromBasket()
    {
        SuccesfullLogin("standard_user", "secret_sauce");
        ClearCartBeforeTest();
        driver.Navigate().GoToUrl("https://www.saucedemo.com/inventory.html");
        var productElementsTitles = driver.FindElements(By.XPath("//div[@data-test='inventory-item-name']"));
        productElementsTitles[0].Click();
        WaitForElement(By.Id("back-to-products"));       
        var addToBCartButton = driver.FindElement(By.XPath("//button[@data-test ='add-to-cart']"));
        addToBCartButton.Click();       
        WaitForElement(By.Id("remove"));
        var shoppingCartBadge = driver.FindElement(By.CssSelector("[data-test='shopping-cart-badge']"));
        var actualProductNumber = int.Parse(shoppingCartBadge.Text);
        var expectedProductNumber = 1;
        Assert.That(actualProductNumber, Is.EqualTo(expectedProductNumber), "Product numder in the basket doesn't match");
        var cartButton = driver.FindElement(By.CssSelector("[data-test='shopping-cart-link']"));
        cartButton.Click();
        var cartPageTitle = driver.FindElement(By.CssSelector("[data-test='title']"));
        Assert.That(cartPageTitle.Text, Is.EqualTo("Your Cart"), "Cart page is not opened");
        var removeButton = driver.FindElement(By.Id("remove-sauce-labs-backpack"));
        removeButton.Click();
        var items = driver.FindElements(By.CssSelector("[data-test='inventory-item']"));
        Assert.That(items.Count, Is.EqualTo(0), "The item is still in the cart");
    }    
}
