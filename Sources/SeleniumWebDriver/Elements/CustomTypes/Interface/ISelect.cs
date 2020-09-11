using SeleniumWebDriver.Elements.BaseTypes.Interfaces;

namespace SeleniumWebDriver.Elements.CustomTypes.Interface
{
    public interface ISelect : IElement
    {
        bool Selected { get; }
        void Select(string value);
        string GetSelected();
    }
}