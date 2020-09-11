using OpenQA.Selenium;
using SeleniumWebDriver.Elements.Locators;
using System;
using System.Collections.Generic;

namespace SeleniumWebDriver.Elements.BaseTypes.Interfaces
{
    public interface INative
    {
        [Obsolete("Finding an element using 'How' FindElement(Locator locator) is deprecated. Please use 'By' FindElement(By by, string locator)")]
        IWebElement FindElement(Locator locator, int index);
        IWebElement FindElement(Locators.By by, string locator, int index);
        [Obsolete("Finding an element using 'How' FindElements(Locator locator) is deprecated. Please use 'By' FindElements(By by, string locator)")]
        IReadOnlyCollection<IWebElement> FindElements(Locator locator);
        IReadOnlyCollection<IWebElement> FindElements(Locators.By by, string locator);
    }
}