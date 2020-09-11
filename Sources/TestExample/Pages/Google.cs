using SeleniumWebDriver.Elements.Attributes;
using SeleniumWebDriver.Elements.BaseTypes;
using SeleniumWebDriver.Elements.CustomTypes;
using SeleniumWebDriver.Elements.Locators;

namespace TestExample.Pages
{
    [Url("/")]
    public class Google : WebPage
    {
        [FindBy(By.Id, "hplogo")] public WebElement Logo { get; set; }
        [FindBy(By.Name, "q")] public TextInput Search { get; set; }
        [FindBy(By.Name, "btnK")] public Button SearchInGoogle { get; set; }
        [FindBy(By.Name, "нету_элемента")] public Button NoExistingButton { get; set; }
        [FindBy(By.Name, "нету_такого_родительского_элемента")] public NotExistingParentElement NoExistingParentElement { get; set; }
    }

    public class NotExistingParentElement : WebElement
    {
        [FindBy(By.Name, "q")] public TextInput Search { get; set; }
    }
}