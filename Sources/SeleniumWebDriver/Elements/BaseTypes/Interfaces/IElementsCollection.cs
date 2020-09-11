using System.Collections.Generic;

namespace SeleniumWebDriver.Elements.BaseTypes.Interfaces
{
    public interface IElementsCollection<out T> : IEnumerable<T> where T : IElement, new() { }
}