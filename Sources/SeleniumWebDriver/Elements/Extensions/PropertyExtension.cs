using System;
using System.Collections;
using System.Linq;
using System.Reflection;
using SeleniumWebDriver.Elements.Attributes;
using SeleniumWebDriver.Elements.BaseTypes.Interfaces;
using SeleniumWebDriver.Elements.Locators;
using UrlAttribute = SeleniumWebDriver.Elements.Attributes.UrlAttribute;

namespace SeleniumWebDriver.Elements.Extensions
{
    public static class PropertyExtension
    {
        internal static bool IsWebElement(this PropertyInfo propertyInfo) => propertyInfo.PropertyType.IsWebElement();
        internal static bool IsWebElement(this Type type) => type.GetInterfaces().Any(i => i == (typeof(IElement)));
        internal static bool HasLocatorAttribute(this PropertyInfo propertyInfo) => propertyInfo.CustomAttributes.Any(attribute => attribute.AttributeType == typeof(FindByAttribute));
        internal static bool HasUrlAttribute(this Type pageType) => pageType.CustomAttributes.Any(attribute => attribute.AttributeType == typeof(UrlAttribute));

        internal static Locator GetLocatorAttribute(this PropertyInfo propertyInfo)
        {
            var findByAttribute = (FindByAttribute) Attribute.GetCustomAttribute(propertyInfo, typeof (FindByAttribute));
            return findByAttribute.ToLocator();
        }
        
        internal static UrlAttribute GetUrlAttribute(this Type pageType)
        {
            var urlAttribute = (UrlAttribute) Attribute.GetCustomAttribute(pageType, typeof (UrlAttribute));
            return urlAttribute;
        }

        internal static bool IsWebElementsCollection(this PropertyInfo propertyInfo)
        {
            if (!propertyInfo.PropertyType.IsGenericType) return false;
            var interfaces = propertyInfo.PropertyType.GetInterfaces();
            if (interfaces.All(i => i != typeof(IEnumerable))) return false;
            if (propertyInfo.PropertyType.GenericTypeArguments.Length != 1) return false;
            return propertyInfo.PropertyType.GenericTypeArguments.Single().IsWebElement();
        }
    }
}