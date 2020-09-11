using OpenQA.Selenium;
using SeleniumWebDriver.DriverManager;
using SeleniumWebDriver.Elements.BaseTypes.Interfaces;
using SeleniumWebDriver.Elements.Extensions;
using SeleniumWebDriver.Elements.Factories;
using SeleniumWebDriver.Elements.Locators;
using System;
using System.Collections.Generic;
using System.Drawing;
using static SeleniumWebDriver.Helpers.WaitHelper;

namespace SeleniumWebDriver.Elements.BaseTypes
{
    public class WebElement : IElement
    {
        private IWebElement _nativeElement;
        internal IWebElement GetNative()
        {
            if (_nativeElement == null || !_nativeElement.Exists())
                _nativeElement = Parent.FindElement(SearchStrategy.Locator, SearchStrategy.Index);

            return _nativeElement;
        }
        public INative Parent { get; set; }
        public Locator Locator { get; set; }
        public SearchStrategy SearchStrategy { get; set; }
        public Size Size => DoRetryWithReturn(() => GetNative().Size);
        public Point Location => DoRetryWithReturn(() => GetNative().Location);
        public bool Displayed => DoRetryWithReturn(() => GetNative().Displayed);
        public bool Enabled => DoRetryWithReturn(() => GetNative().Enabled);
        public string TagName => DoRetryWithReturn(() => GetNative().TagName);
        public bool Selected => DoRetryWithReturn(() => GetNative().Selected);
        public string Text => DoRetryWithReturn(() => GetNative().Text);
        public string GetAttribute(string attributeName) => DoRetryWithReturn(() => GetNative().GetAttribute(attributeName));
        public string GetProperty(string property) => DoRetryWithReturn(() => GetNative().GetProperty(property));
        public string GetCssValue(string cssName) => DoRetryWithReturn(() => GetNative().GetCssValue(cssName));
        public bool Exists()
        {
            try
            {
                GetNative().Exists();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public WebElement() { }
        public WebElement(Locator locator) => Locator = locator;
        public WebElement(INative parent, Locator locator)
        {
            Parent = parent;
            Locator = locator;
        }

        public void MouseOver() => DoRetry(() => { DriverFactory.Action.MoveToElement(GetNative()).Build().Perform(); });

        public void Click() => DoRetry(() => { GetNative().Click(); });

        [Obsolete("Finding an element using 'How' Find<T>(Locator locator) is deprecated. Please use 'By' Find<T>(By by, string locator)")]
        public T Find<T>(Locator locator) where T : IElement, new() => WebElementFactory.Create<T>(this, locator);
        public T Find<T>(Locators.By by, string locator) where T : IElement, new() => WebElementFactory.Create<T>(this, new Locator(by, locator));

        [Obsolete("Finding an element using 'How' FindAll<T>(Locator locator) is deprecated. Please use 'By' FindAll<T>(By by, string locator)")]
        public IEnumerable<T> FindAll<T>(Locator locator) where T : IElement, new() => WebElementsCollectionFactory.Create<T>(this, locator);
        public IEnumerable<T> FindAll<T>(Locators.By by, string locator) where T : IElement, new() => WebElementsCollectionFactory.Create<T>(this, new Locator(by, locator));

        IWebElement IElement.GetNative() => GetNative();
        void IElement.SetNativeElement(IWebElement nativeElement) => _nativeElement = nativeElement;

        [Obsolete("Finding an element using 'How' FindElement(Locator locator) is deprecated. Please use 'By' FindElement(By by, string locator)")]
        IWebElement INative.FindElement(Locator locator, int index) => GetNative().FindElement(locator.ToSeleniumLocator());
        IWebElement INative.FindElement(Locators.By by, string locator, int index) => GetNative().FindElement(new Locator(by, locator).ToSeleniumLocator());

        [Obsolete("Finding an element using 'How' FindElements(Locator locator) is deprecated. Please use 'By' FindElements(By by, string locator)")]
        IReadOnlyCollection<IWebElement> INative.FindElements(Locator locator) => GetNative().FindElements(locator.ToSeleniumLocator());
        IReadOnlyCollection<IWebElement> INative.FindElements(Locators.By by, string locator) => GetNative().FindElements(new Locator(by, locator).ToSeleniumLocator());
    }
}