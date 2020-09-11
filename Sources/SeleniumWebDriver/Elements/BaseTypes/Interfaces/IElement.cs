using System.Drawing;
using OpenQA.Selenium;
using SeleniumWebDriver.Elements.Locators;

namespace SeleniumWebDriver.Elements.BaseTypes.Interfaces
{
    public interface IElement : ISearchable, INative
    {
        INative Parent { get; set; }
        Locator Locator { get; set; }
        SearchStrategy SearchStrategy { get; set; }
        Size Size { get; }
        Point Location { get; }

        IWebElement GetNative();
        void SetNativeElement(IWebElement nativeElement);
        void Click();
        bool Displayed { get; }
        bool Exists();
        bool Enabled { get; }
        
        string TagName { get; }
        string GetAttribute(string attributeName);
        string GetProperty(string property);
        string Text { get; }
        string GetCssValue(string cssName);
    }
}