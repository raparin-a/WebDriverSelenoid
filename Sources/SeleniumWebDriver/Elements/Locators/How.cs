namespace SeleniumWebDriver.Elements.Locators
{
    public static class How
    {
        public static Locator Id(string id) => new Locator(By.Id, id);
        public static Locator XPath(string xpath) => new Locator(By.XPath, xpath);
        public static Locator CssSelector(string cssSelector) => new Locator(By.CssSelector, cssSelector);
        public static Locator TagName(string tagName) => new Locator(By.TagName, tagName);
        public static Locator ClassName(string className) => new Locator(By.ClassName, className);
        public static Locator LinkText(string linkText) => new Locator(By.LinkText, linkText);
        public static Locator Name(string name) => new Locator(By.Name, name);
        public static Locator PartialLinkText(string partialLink) => new Locator(By.PartialLinkText, partialLink);
    }
}