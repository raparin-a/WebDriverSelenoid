using System;

namespace SeleniumWebDriver.Exсeptions
{
    public class NotIntractableException : SeleniumWebDriverException
    {
        public NotIntractableException(string exception) : base(exception) { }
        public NotIntractableException(string message, Exception innerException) : base(message, innerException) { }
    }
}