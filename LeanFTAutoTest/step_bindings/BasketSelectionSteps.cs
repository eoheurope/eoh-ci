using System;
using TechTalk.SpecFlow;
using System.Diagnostics;
using HP.LFT.SDK;
using HP.LFT.SDK.Web;
using NUnit.Framework;
using HP.LFT.Verifications;

namespace LeanFTAutoTest.step_bindings
{
    [Binding]
    public class BasketSelectionSteps : UnitTestClassBase
    {
        IBrowser browser;

        [Given(@"I am on the shop web site")]
        public void GivenIAmOnTheShopWebSite()
        {
            browser = BrowserFactory.Launch(BrowserType.Chrome);
            browser.Navigate("https://www.edgewordstraining.co.uk/demo-site");
        }
        
        [When(@"I add an item to the basket")]
        public void WhenIAddAnItemToTheBasket()
        {
            var sEditField = browser.Describe<IEditField>(new EditFieldDescription
            {
                AccessibilityName = string.Empty,
                Name = @"s",
                Placeholder = @"Search products…",
                TagName = @"INPUT",
                Type = @"search",
                Index = 0
            });
            sEditField.SetValue("cap");

            var searchButton = browser.Describe<IButton>(new ButtonDescription
            {
                AccessibilityName = string.Empty,
                ButtonType = @"submit",
                Name = @"Search",
                Role = string.Empty,
                TagName = @"BUTTON",
                Index = 0
            });
            searchButton.Click();

            var addToCartButton = browser.Describe<IButton>(new ButtonDescription
            {
                ButtonType = @"submit",
                Name = @"Add to cart",
                TagName = @"BUTTON"
            });
            addToCartButton.Click();

            Reporter.ReportEvent("Screenshot", "example screenshot", HP.LFT.Report.Status.Passed, browser.GetSnapshot());


            var homeLink = browser.Describe<ILink>(new LinkDescription
            {
                AccessibilityName = string.Empty,
                InnerText = @"Home",
                Role = string.Empty,
                TagName = @"A",
                Index = 0
            });
            homeLink.Click();

            var cartLink = browser.Describe<ILink>(new LinkDescription
            {
                AccessibilityName = string.Empty,
                InnerText = @"Cart",
                Role = string.Empty,
                TagName = @"A",
                Index = 0
            });
            cartLink.Click();
        }
        
        [Then(@"I can see the item in the basket")]
        public void ThenICanSeeTheItemInTheBasket()
        {
            // check cap exists in basket
            var capLink = browser.Describe<ILink>(new LinkDescription
            {
                InnerText = @"Cap",
                TagName = @"A"
            });
            Assert.That(capLink.IsVisible);
            Verify.IsTrue(capLink.IsVisible, "Verifying the cap is in the basket");

            Reporter.ReportEvent("Screenshot", "example screenshot", HP.LFT.Report.Status.Passed, browser.GetSnapshot());


            var removeThisItemLink = browser.Describe<ILink>(new LinkDescription
            {
                InnerText = @"×",
                TagName = @"A"
            });
            removeThisItemLink.Click();

            var returnToShopLink = browser.Describe<ILink>(new LinkDescription
            {
                InnerText = @"Return to shop ",
                TagName = @"A"
            });
            returnToShopLink.Click();

            browser.CloseAllTabs();
        }
    }
}
