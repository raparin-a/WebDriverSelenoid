using SeleniumWebDriver.Elements.BaseTypes.Interfaces;

namespace SeleniumWebDriver.Elements.CustomTypes.Interface
{
    public interface ITextInput : IElement
    {
        void Clear();
        void SendKeys(string text);
    }
}