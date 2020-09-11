using OpenQA.Selenium;
using OpenQA.Selenium.Support.Extensions;
using SeleniumWebDriver.DriverManager;
using SeleniumWebDriver.Elements.BaseTypes.Interfaces;
using SeleniumWebDriver.Elements.Extensions;
using SeleniumWebDriver.Elements.Factories;
using SeleniumWebDriver.Elements.Locators;
using System;
using System.Collections.Generic;
using static SeleniumWebDriver.Helpers.WaitHelper;

namespace SeleniumWebDriver.Elements.BaseTypes
{
    public abstract class WebPage : IWebPage
    {
        public string Title { get; set; }
        public string Address { get; set; }
        public bool Opened(int retryCount = 4) => DoRetryWithReturn(() => DriverFactory.GetDriver.Url.StartsWith(((IWebPage)this).Address), retryCount);
        /// <summary>
        /// Open page with address from your appsettings config + address from your url page attribute + query
        /// </summary>
        /// <param name="query">For example '?filter=isactive'</param>
        public void Open(string query = null) => DriverFactory.GetDriver.Navigate().GoToUrl(Address + query);

        public virtual void WaitForLoaded(int retryCount = 4)
            => DoRetryWithReturn(() => (DriverFactory.GetDriver as IJavaScriptExecutor)?
                .ExecuteScript("if (document.readyState) return document.readyState;").ToString().ToLower() == "complete", retryCount);

        [Obsolete("Finding an element using 'How' Find<T>(Locator locator) is deprecated. Please use 'By' Find<T>(By by, string locator)")]
        public T Find<T>(Locator locator) where T : IElement, new() => WebElementFactory.Create<T>(this, locator);
        public T Find<T>(Locators.By by, string locator) where T : IElement, new() => WebElementFactory.Create<T>(this, new Locator(by, locator));

        [Obsolete("Finding an element using 'How' FindAll<T>(Locator locator) is deprecated. Please use 'By' FindAll<T>(By by, string locator)")]
        public IEnumerable<T> FindAll<T>(Locator locator) where T : IElement, new() => WebElementsCollectionFactory.Create<T>(this, locator);
        public IEnumerable<T> FindAll<T>(Locators.By by, string locator) where T : IElement, new() => WebElementsCollectionFactory.Create<T>(this, new Locator(by, locator));

        [Obsolete("Finding an element using 'How' FindElement(Locator locator) is deprecated. Please use 'By' FindElement(By by, string locator)")]
        IWebElement INative.FindElement(Locator locator, int index) => DriverFactory.GetDriver.FindElement(locator.ToSeleniumLocator());
        IWebElement INative.FindElement(Locators.By by, string locator, int index) => DriverFactory.GetDriver.FindElement(new Locator(by, locator).ToSeleniumLocator());

        [Obsolete("Finding an element using 'How' FindElements(Locator locator) is deprecated. Please use 'By' FindElements(By by, string locator)")]
        IReadOnlyCollection<IWebElement> INative.FindElements(Locator locator) => DriverFactory.GetDriver.FindElements(locator.ToSeleniumLocator());
        IReadOnlyCollection<IWebElement> INative.FindElements(Locators.By by, string locator) => DriverFactory.GetDriver.FindElements(new Locator(by, locator).ToSeleniumLocator());

        public virtual void ScrollDown(string number) => DriverFactory.GetDriver.ExecuteJavaScript("scroll(0," + number + ")");
    }
}