using System;
using SeleniumWebDriver.Elements.Locators;

namespace SeleniumWebDriver.Elements.Attributes
{
    public class FindByAttribute : Attribute
    {
        public By By { get; }

        public string Locator { get; }

        public FindByAttribute(By by, string locator)
        {
            By = by;
            Locator = locator;
        }
    }
}