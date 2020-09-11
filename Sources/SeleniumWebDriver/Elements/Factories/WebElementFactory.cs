using System;
using System.Reflection;
using SeleniumWebDriver.Elements.BaseTypes.Interfaces;
using SeleniumWebDriver.Elements.Extensions;
using SeleniumWebDriver.Elements.Locators;

namespace SeleniumWebDriver.Elements.Factories
{
    public static class WebElementFactory
    {
        public static T Create<T>(INative parent, Locator locator) where T : IElement, new () => (T) Create(typeof(T), parent, locator);
        public static T Create<T>(INative parent, Locator locator, int index) where T : IElement, new() => (T) Create(typeof(T), parent, locator, index);
        
         internal static void InitProperties(INative native)
        {
            var properties = native.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            foreach (var property in properties)
            {
                if (property.HasLocatorAttribute() && property.IsWebElement())
                {
                    var element = Create(property.PropertyType, native, property.GetLocatorAttribute());
                    property.SetValue(native, element);
                }
                if (property.HasLocatorAttribute() && property.IsWebElementsCollection())
                {
                    var collection = WebElementsCollectionFactory.Create(property.PropertyType, native, property.GetLocatorAttribute());
                    property.SetValue(native, collection);
                }
                if (property.HasLocatorAttribute() && !(property.IsWebElement() || property.IsWebElementsCollection()))
                    throw new InvalidOperationException($"Property {property.Name} has FindBy attribute but is not collection or element");
            }
        }
         
        private static IElement Create(Type elementType, INative parent, Locator locator, int index = 0)
        {
            var element = (IElement) Activator.CreateInstance(elementType);
            element.Parent = parent;
            element.SearchStrategy = new SearchStrategy(locator, index);
            InitProperties(element);
            return element;
        }
    }
}