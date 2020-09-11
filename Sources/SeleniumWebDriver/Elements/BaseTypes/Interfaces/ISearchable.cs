using SeleniumWebDriver.Elements.Locators;
using System;
using System.Collections.Generic;

namespace SeleniumWebDriver.Elements.BaseTypes.Interfaces
{
    public interface ISearchable
    {
        [Obsolete("Finding an element using 'How' Find<T>(Locator locator) is deprecated. Please use 'By' Find(By by, string locator)")]
        T Find<T>(Locator locator) where T : IElement, new();
        T Find<T>(By by, string locator) where T : IElement, new();
        [Obsolete("Finding an element using 'How' FindAll<T>(Locator locator) is deprecated. Please use 'By' FindAll(By by, string locator)")]
        IEnumerable<T> FindAll<T>(Locator locator) where T : IElement, new();
        IEnumerable<T> FindAll<T>(By by, string locator) where T : IElement, new();
    }
}