using System;

namespace SeleniumWebDriver.Elements.Attributes
{
    public class UrlAttribute : Attribute
    {
        public string Url { get; private set; }
        public UrlAttribute(string url) =>  Url = url;
    }
}