using System;
using System.IO;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.Extensions;
using Protractor;
using SeleniumWebDriver.Elements.BaseTypes;
using SeleniumWebDriver.Elements.BaseTypes.Interfaces;
using SeleniumWebDriver.Exсeptions;
using static SeleniumWebDriver.DriverManager.DriverFactory;
using static SeleniumWebDriver.Helpers.DirectoryHelper;

namespace SeleniumWebDriver.DriverManager
{
    public static class Browser
    {
        public static string Url => GetDriver.Url;
        /// <summary>
        /// This method is closing browser with all tabs.
        /// </summary>
        public static void CloseBrowser()
        {
            GetDriver.Quit();
            GetDriver.Dispose();
            CloseSession();
        }
        
        /// <summary>
        /// This method will close current tab in browser, and will close browser if there is only one tab.
        /// </summary>
        public static void CloseTab() => GetDriver.Close();
        public static void GoTo(string url) => GetDriver.Navigate().GoToUrl(url);
        public static void Back() => GetDriver.Navigate().Back();
        public static void Forward() => GetDriver.Navigate().Forward();
        public static void Refresh() => GetDriver.Navigate().Refresh();

        public static void TakeScreenshots(string testName, string createFolderName = "Failed_tests")
        {
            var createFolder = CreateAndReturn(createFolderName, testName);
            var path = Path.Combine(createFolder,"[" + DateTime.Now.ToString(" HH-mm-ss ") + "] "+ testName + ".png");
            switch (GetDriver)
            {
                case RemoteWebDriver remoteWebDriver:
                    remoteWebDriver.TakeScreenshot().SaveAsFile(path, ScreenshotImageFormat.Png);
                    break;
                case NgWebDriver ngDriver:
                    ((ITakesScreenshot)ngDriver.WrappedDriver).GetScreenshot().SaveAsFile(path, ScreenshotImageFormat.Png);
                    break;
                default:
                    throw new SeleniumWebDriverException("Unsupported web driver type");
            }
        }

        public static byte[] TakeScreenshots()
        {
            return GetDriver switch
            {
                RemoteWebDriver remoteWebDriver => remoteWebDriver.TakeScreenshot().AsByteArray,
                NgWebDriver ngDriver => ((ITakesScreenshot) ngDriver.WrappedDriver).GetScreenshot().AsByteArray,
                _ => throw new SeleniumWebDriverException("Unsupported web driver type")
            };
        }

        public static object ExecuteScript(string script, params object[] args)
        {
            var arguments = args.Select(el => el is WebElement ? ((IElement) el).GetNative() : el);
            return (GetDriver as IJavaScriptExecutor)?.ExecuteScript(script, arguments);
        }

        public static void SwitchToTabWithUrl(string url)
        {
            var current = GetDriver.CurrentWindowHandle;

            foreach (var windowHandle in GetDriver.WindowHandles)
            {
                if (GetDriver.CurrentWindowHandle != windowHandle)
                {
                    if (GetDriver.SwitchTo().Window(windowHandle).Url.Equals(url)) return;
                }
            }

            GetDriver.SwitchTo().Window(current);
        }

        public static void SwitchToTabWhichContainsUrl(string url)
        {
            var current = GetDriver.CurrentWindowHandle;

            foreach (var windowHandle in GetDriver.WindowHandles)
            {
                if (GetDriver.CurrentWindowHandle != windowHandle)
                {
                    if (GetDriver.SwitchTo().Window(windowHandle).Url.Contains(url)) return;
                }
            }

            GetDriver.SwitchTo().Window(current);
        }
        
        public static void SwitchToNextTab()
        {
            var indexNext = GetDriver.WindowHandles.IndexOf(GetDriver.CurrentWindowHandle) + 1;
            if (GetDriver.WindowHandles.Count > indexNext)  GetDriver.SwitchTo().Window(GetDriver.WindowHandles[indexNext]);
        }
        
        public static void SwitchToPreviousTab()
        {
            var indexNext = GetDriver.WindowHandles.IndexOf(GetDriver.CurrentWindowHandle) - 1;
            if (indexNext >= 0) GetDriver.SwitchTo().Window(GetDriver.WindowHandles[indexNext]);
        }

        public static void SendKeys(string message)
        {
            Actions action = new Actions(GetDriver);
            action.SendKeys(message);
            action.Perform();
        }

        public static void SwitchToIFrame(string frameId) => GetDriver.SwitchTo().Frame(frameId);
        public static void SwitchBackFromIFrame() => GetDriver.SwitchTo().DefaultContent();
        public static void AcceptAlert() => GetDriver.SwitchTo().Alert().Accept();
        public static void DeclineAlert() => GetDriver.SwitchTo().Alert().Dismiss();
        public static string GetAlertText => GetDriver.SwitchTo().Alert().Text;
        public static string SessionId
        {
            get
            {
                if (GetDriver is RemoteWebDriver remoteDriver)
                    return remoteDriver.SessionId.ToString();
                if (GetDriver is NgWebDriver ngDriver)
                    return (ngDriver.WrappedDriver as RemoteWebDriver)?.SessionId.ToString();
                return "undefined";
            }
        }
    }
}