using SeleniumWebDriver.Elements.Attributes;
using SeleniumWebDriver.Elements.BaseTypes;
using SeleniumWebDriver.Elements.Locators;

namespace TestExample.Angular.Pages
{
    [Url("/")]
    public class Angular : WebPage
    {
        public WebElement GetStartedButton => this.Find<WebElement>(By.XPath,"//div[@class = 'homepage-container']//a");
        public WebElement TryItNow => this.Find<WebElement>(By.XPath,"//div[@class = 'card']");
        public WebElement IntroductionTitle => this.Find<WebElement>(By.Id,"introduction-to-the-angular-docs");
    }
}
