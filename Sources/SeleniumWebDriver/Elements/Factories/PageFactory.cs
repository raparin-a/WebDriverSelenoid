using SeleniumWebDriver.Configuration;
using SeleniumWebDriver.Elements.BaseTypes.Interfaces;
using SeleniumWebDriver.Elements.Extensions;

namespace SeleniumWebDriver.Elements.Factories
{
    public static class PageFactory
    {
        private static readonly string BaseUrl = Settings.WebDriverOptions.Value.PageOptions.BaseUrl;
        public static T Get<T>() where T : IWebPage, new()
        {
            IWebPage page = new T();
            InitPage(page);
            return (T) page;
        }
        private static void InitPage(IWebPage page)
        {
            if (page.GetType().HasUrlAttribute()) page.Address = BaseUrl + page.GetType().GetUrlAttribute().Url;
            WebElementFactory.InitProperties(page);
        }
    }
}