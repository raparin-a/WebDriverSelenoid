using System;

namespace SeleniumWebDriver.Exсeptions
{
    public class NotFoundException : SeleniumWebDriverException
    {
        public NotFoundException(string exception) : base(exception) { }
        public NotFoundException(string message, Exception innerException) : base(message, innerException) { }
    }
}