using OpenQA.Selenium.Support.UI;
using SeleniumWebDriver.Elements.BaseTypes;
using SeleniumWebDriver.Elements.CustomTypes.Interface;
using static SeleniumWebDriver.Helpers.WaitHelper;

namespace SeleniumWebDriver.Elements.CustomTypes
{
    public class DropDown : WebElement, ISelect
    {
        public void Select(string value) => DoRetry(() => new SelectElement(GetNative()).SelectByText(value));
        public string GetSelected() => DoRetryWithReturn(() => new SelectElement(GetNative()).SelectedOption.Text);
    }
}