using System;
using SeleniumWebDriver.Elements.Attributes;
using SeleniumWebDriver.Elements.Locators;

namespace SeleniumWebDriver.Elements.Extensions
{
    public static class LocatorConverterExtension
    {
        internal static Locator ToLocator(this FindByAttribute attribute) => new Locator(attribute.By, attribute.Locator);
        internal static OpenQA.Selenium.By ToSeleniumLocator(this Locator locator)
        {
            switch (locator.By)
            {
                case By.Id:
                    return OpenQA.Selenium.By.Id(locator.Using);
                case By.CssSelector:
                    return OpenQA.Selenium.By.CssSelector(locator.Using);
                case By.TagName:
                    return OpenQA.Selenium.By.TagName(locator.Using);
                case By.XPath:
                    return OpenQA.Selenium.By.XPath(locator.Using);
                case By.ClassName:
                    return OpenQA.Selenium.By.ClassName(locator.Using);
                case By.LinkText:
                    return OpenQA.Selenium.By.LinkText(locator.Using);
                case By.Name:
                    return OpenQA.Selenium.By.Name(locator.Using);
                case By.PartialLinkText:
                    return OpenQA.Selenium.By.PartialLinkText(locator.Using);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}