using System;
using System.Collections.Generic;
using System.Threading;
using OpenQA.Selenium.Chrome;
using SeleniumWebDriver.Configuration;
using SeleniumWebDriver.Configuration.Options;

namespace SeleniumWebDriver.DriverManager
{
    public static class CapabilitiesFactory
    {
        private static readonly ThreadLocal<Dictionary<string, Dictionary<string, object>>> Capabilities = new ThreadLocal<Dictionary<string, Dictionary<string, object>>>();
        private static readonly CapabilitiesOptions DefaultCapabilities = Settings.WebDriverOptions.Value.SelenoidOptions.CapabilitiesOptions;
        private const string SelenoidCap = "selenoid:options";
        private const string ChromeCap = "ChromeCapabilities";
        
        private static Dictionary<string, Dictionary<string, object>> GetCapabilities
        {
            get
            {
                if (Capabilities.Value == null || !Capabilities.IsValueCreated)
                {
                    Capabilities.Value = new Dictionary<string, Dictionary<string, object>>
                    {
                        {SelenoidCap, new Dictionary<string, object>
                            {
                                {nameof(DefaultCapabilities.enableVNC), DefaultCapabilities.enableVNC},
                                {nameof(DefaultCapabilities.enableLogs), DefaultCapabilities.enableLogs},
                                {nameof(DefaultCapabilities.enableVideo), DefaultCapabilities.enableVideo},
                                {nameof(DefaultCapabilities.screenResolution), DefaultCapabilities.screenResolution},
                                {nameof(DefaultCapabilities.videoScreenSize), DefaultCapabilities.videoScreenSize},
                                {nameof(DefaultCapabilities.videoFrameRate), DefaultCapabilities.videoFrameRate},
                                {nameof(DefaultCapabilities.videoCodec), DefaultCapabilities.videoCodec}
                            }
                        },
                        {ChromeCap, new Dictionary<string, object>()}
                    };
                }

                return Capabilities.Value;
            }
        }

        internal static ChromeOptions GetCapabilitiesForDriver()
        {
            var options = new ChromeOptions { BrowserVersion = DefaultCapabilities.chromeVersion};
                options.AddAdditionalCapability(SelenoidCap, GetCapabilities[SelenoidCap], true);
            
            if (GetCapabilities[ChromeCap].Count == 0) return options;
            
            foreach (var capability in GetCapabilities[ChromeCap])
                options.AddAdditionalCapability(capability.Key, capability.Value, true);
            
            return options;
        }

        internal static void AddSelenoidCapabilities(string key, string value)
        {
            if (GetCapabilities[SelenoidCap].ContainsKey(key))
                GetCapabilities[SelenoidCap][key] = value;
            else
                GetCapabilities[SelenoidCap].Add(key, value);
        }

        internal static void AddChromeCapabilities(string key, string value)
        {
            if (GetCapabilities[ChromeCap].ContainsKey(key))
                GetCapabilities[ChromeCap][key] = value;
            else
                GetCapabilities[ChromeCap].Add(key, value);
        }
    }
}