using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using OpenQA.Selenium;
using SeleniumWebDriver.Configuration.Options;
using SeleniumWebDriver.DriverManager;
using static SeleniumWebDriver.DriverManager.CapabilitiesFactory;

namespace SeleniumWebDriver.Configuration
{
    public static class Settings
    {
        private static ServiceProvider ServiceProvider { get; set; }
        internal static IOptions<WebDriverOptions> WebDriverOptions { get; private set; }
        internal static string CurrentPath => AppContext.BaseDirectory;
        public static void ConfigureWebDriver(string settingFileName = "appsettings.json")
        {
            var configuration = new ConfigurationBuilder()
                                    .AddJsonFile(settingFileName)
                                    .AddJsonFile($"{settingFileName}.{Environment.MachineName}.json", true)
                                    .AddEnvironmentVariables()
                                    .Build();
            var services = new ServiceCollection();
            services.Configure<WebDriverOptions>(configuration.GetSection("WebDriverOptions"));
            ServiceProvider = services.BuildServiceProvider();
            WebDriverOptions = ServiceProvider.GetService<IOptions<WebDriverOptions>>();
        }
        
        public static void AddBrowserCapability(string name, string value) => AddChromeCapabilities(name, value);

        public static IWebDriver GetWebDriver() => DriverFactory.GetDriver;
    }
}