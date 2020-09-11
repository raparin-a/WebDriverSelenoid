using SeleniumWebDriver.Elements.Attributes;
using SeleniumWebDriver.Elements.BaseTypes;
using SeleniumWebDriver.Elements.CustomTypes;
using SeleniumWebDriver.Elements.Locators;

namespace TestExample.Pages
{
    public class PageWithHTMLTable : WebPage
    {
        [FindBy(By.CssSelector, "div.card-body table.table.table-striped")] public HTMLTable Table { get; set; }
    }
}