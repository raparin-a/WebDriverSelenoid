using SeleniumWebDriver.Elements.BaseTypes;
using SeleniumWebDriver.Elements.CustomTypes.Interface;
using static SeleniumWebDriver.Helpers.WaitHelper;

namespace SeleniumWebDriver.Elements.CustomTypes
{
    public class TextInput : WebElement, ITextInput
    {
        public void SendKeys(string text) => DoRetry(() => { GetNative().SendKeys(text); });
        public void Clear() => DoRetry(() => { GetNative().Clear(); });
        public string GetText => GetAttribute("text") ?? string.Empty;
    }
}