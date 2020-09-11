using SeleniumWebDriver.Elements.BaseTypes.Interfaces;

namespace SeleniumWebDriver.Elements.CustomTypes.Interface
{
    public interface ICheckBox : IElement
    {
        void Check();
        void Uncheck();
        bool Checked { get; }
    }
}