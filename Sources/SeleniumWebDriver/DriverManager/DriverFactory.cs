using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Remote;
using Protractor;
using SeleniumWebDriver.Configuration;
using System;
using System.Drawing;
using System.Threading;

namespace SeleniumWebDriver.DriverManager
{
    public static class DriverFactory
    {
        private static readonly ThreadLocal<IWebDriver> Driver = new ThreadLocal<IWebDriver>();
        internal static Actions Action => new Actions(GetDriver);

        public static IWebDriver GetDriver
        {
            get
            {
                if (Driver.Value == null || !Driver.IsValueCreated) Driver.Value = CreateSession();

                return Driver.Value;
            }
        }

        private static IWebDriver CreateSession()
        {
            var options = Settings.WebDriverOptions.Value;
            var session = new RemoteWebDriver(new Uri(options.SelenoidOptions.HubUrl + "/wd/hub"), CapabilitiesFactory.GetCapabilitiesForDriver());

            if (options.PageOptions.Width is null || options.PageOptions.Height is null)
                session.Manage().Window.Maximize();
            else
                session.Manage().Window.Size = new Size(options.PageOptions.Width.Value, options.PageOptions.Height.Value);
            if (options.AngularOptions?.UseNgWebDriver != null && options.AngularOptions.AsyncTimeoutMsec != null)
            {
                session.Manage().Timeouts().AsynchronousJavaScript = TimeSpan.FromMilliseconds(options.AngularOptions.AsyncTimeoutMsec.Value);
                return new NgWebDriver(session) as IWebDriver;
            }

            return session;
        }

        internal static void CloseSession() => Driver.Value = null;
    }
}