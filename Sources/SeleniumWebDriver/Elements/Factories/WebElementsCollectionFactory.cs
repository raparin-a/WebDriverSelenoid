using System;
using SeleniumWebDriver.Elements.BaseTypes;
using SeleniumWebDriver.Elements.BaseTypes.Interfaces;
using SeleniumWebDriver.Elements.Locators;

namespace SeleniumWebDriver.Elements.Factories
{
    public static class WebElementsCollectionFactory
    {
        internal static IElementsCollection<T> Create<T>(INative parent, Locator locator) where T : IElement, new() => new WebElementsCollection<T>(parent, locator);
        
        internal static object Create(Type type, INative parent, Locator locator)
        {
            var listType = typeof(WebElementsCollection<>);
            var genericArgs = type.GetGenericArguments();
            var concreteType = listType.MakeGenericType(genericArgs);
            var newList = Activator.CreateInstance(concreteType, parent, locator);
            return newList;
        }
    }
}