using System.Linq;
using SeleniumWebDriver.Elements.Attributes;
using SeleniumWebDriver.Elements.BaseTypes;
using SeleniumWebDriver.Elements.Locators;

namespace SeleniumWebDriver.Elements.CustomTypes
{
    public class HTMLTable : WebElement
    {
        private class BodyCell  : WebElement
        {
            [FindBy(By.CssSelector, "td")] internal WebElementsCollection<TextInput> BodyCells { get; set; }
        }
        
        private class HeaderCell : WebElement
        {
            [FindBy(By.CssSelector, "th")] internal WebElementsCollection<TextInput> HeaderCells { get; set; }
        }

        [FindBy(By.CssSelector, "tbody > tr")] private WebElementsCollection<BodyCell> BodyRows { get; set; }

        [FindBy(By.CssSelector, "thead > tr")] private WebElementsCollection<HeaderCell> HeaderRows { get; set; }
        
        
        public WebElement GetWebElementInBodyCellAtIndex(short bodyRowIndex, short bodyCellIndex)
            => BodyRows?.ElementAtOrDefault(bodyRowIndex)?.BodyCells?.ElementAtOrDefault(bodyCellIndex);

        public string GetTextInBodyCellAtIndex(short bodyRowIndex, short bodyCellIndex)
            => BodyRows?.ElementAtOrDefault(bodyRowIndex)?.BodyCells?.ElementAtOrDefault(bodyCellIndex)?.Text;

        public void SendTextToBodyCellAtIndex(short bodyRowIndex, short bodyCellIndex, string text)
            => BodyRows.ElementAtOrDefault(bodyRowIndex).BodyCells.ElementAtOrDefault(bodyCellIndex).SendKeys(text);

        public WebElement GetWebElementInHeaderCellAtIndex(short bodyRowIndex, short bodyCellIndex)
            => HeaderRows?.ElementAtOrDefault(bodyRowIndex)?.HeaderCells?.ElementAtOrDefault(bodyCellIndex);

        public string GetTextInHeaderCellAtIndex(short bodyRowIndex, short bodyCellIndex) 
            => HeaderRows?.ElementAtOrDefault(bodyRowIndex)?.HeaderCells?.ElementAtOrDefault(bodyCellIndex)?.Text;

        public void SendTextToHeaderCellAtIndex(short bodyRowIndex, short bodyCellIndex, string text)
            => HeaderRows.ElementAtOrDefault(bodyRowIndex).HeaderCells.ElementAtOrDefault(bodyCellIndex).SendKeys(text);

        public string[] GetHeaders()
            => HeaderRows.FirstOrDefault().HeaderCells.Where(cell => !string.IsNullOrEmpty(cell.Text)).Select(c => c.Text).ToArray();

    }
}

    