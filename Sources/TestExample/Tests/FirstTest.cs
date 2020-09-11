using NUnit.Framework;
using SeleniumWebDriver.DriverManager;
using SeleniumWebDriver.Elements.Factories;
using TestExample.Pages;

namespace TestExample.Tests
{
    [TestFixture]
    public class FirstTest : TestsBase
    {
        [Test]
        public void LetsStart()
        {
            var google = PageFactory.Get<Google>();
            google.Open();
            google.Search.SendKeys("Test");
            google.SearchInGoogle.Click();
            Browser.TakeScreenshots("SomeName");
        }
        
        [Test]
        public void LetsStart1()
        {
            var google = PageFactory.Get<Google>();
            google.Open();
            google.Search.SendKeys("Test");
            google.SearchInGoogle.Click();
            Browser.SwitchToTabWithUrl("https://www.google.com");
            Browser.TakeScreenshots("SomeName");
        }
        
        [Test]
        public void LetsStart2()
        {
            var tablePage = PageFactory.Get<PageWithHTMLTable>();
            tablePage.Open("https://ng-bootstrap.github.io/#/components/table/examples");
            var table = tablePage.Table;
            var textInBodyRow = tablePage.Table.GetTextInBodyCellAtIndex(2, 2);
            Assert.AreEqual("324,459,463", textInBodyRow);
        }
    }
}