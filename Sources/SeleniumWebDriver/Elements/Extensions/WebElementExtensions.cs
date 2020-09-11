using System;
using OpenQA.Selenium;
using SeleniumWebDriver.Elements.BaseTypes;
using static SeleniumWebDriver.Helpers.WaitHelper;
using static SeleniumWebDriver.DriverManager.DriverFactory;

namespace SeleniumWebDriver.Elements.Extensions
{
    public static class WebElementExtensions
    {
        internal static bool Exists(this IWebElement element)
        {
            try
            {
                // Poke element.
                // ReSharper disable once UnusedVariable
                var text = element.Text;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// This method will wait until attribute value is not null.
        /// If you don't want to wait set default param retries value to 1. 
        /// </summary>
        /// <param name="element"></param>
        /// <param name="attributeName">Name of attribute you are looking for.</param>
        /// <param name="retries">Default value is 0, that means it will use your application settings values for retries.</param>
        /// <returns></returns>
        public static string WaitForAttribute(this WebElement element, string attributeName, int retries = 0) 
            => WaitForCondition(() => element.GetAttribute(attributeName), e => e.Length == 0, retries);
        
        /// <summary>
        /// This method will wait until attribute value is not null.
        /// If you don't want to wait set default param retries value to 1. 
        /// </summary>
        /// <param name="element"></param>
        /// <param name="attributeName">Name of attribute you are looking for.</param>
        /// <param name="retries">Default value is 0, that means it will use your application settings values for retries.</param>
        /// <param name="repeatCondition">Condition for attribute value</param>
        /// <returns></returns>
        public static string WaitForAttribute(this WebElement element, string attributeName, Func<string, bool> repeatCondition, int retries = 0) 
            => WaitForCondition(() => element.GetAttribute(attributeName), repeatCondition, retries);

        /// <summary>
        /// This method will wait until property value is not null.
        /// If you don't want to wait set default param retries value to 1. 
        /// </summary>
        /// <param name="element"></param>
        /// <param name="propertyName">Name of property you are looking for.</param>
        /// <param name="retries">Default value is 0, that means it will use your application settings values for retries.</param>
        /// <returns></returns>
        public static string WaitForProperty(this WebElement element, string propertyName, int retries = 0)
            => WaitForCondition(() => element.GetProperty(propertyName), e => e.Length == 0, retries);

        /// <summary>
        /// This method will wait until css class value is not null.
        /// If you don't want to wait set default param retries value to 1. 
        /// </summary>
        /// <param name="element"></param>
        /// <param name="cssName">Name of css class you are looking for.</param>
        /// <param name="retries">Default value is 0, that means it will use your application settings values for retries.</param>
        /// <returns></returns>

        public static string WaitForCssValue(this WebElement element, string cssName, int retries = 0)
            => WaitForCondition(() => element.GetCssValue(cssName), e => e.Length == 0, retries);

        /// <summary>
        /// This method will wait for text value is not null.
        /// If you don't want to wait set default param retries value to 1. 
        /// </summary>
        /// <param name="element"></param>
        /// <param name="retries">Default value is 0, that means it will use your application settings values for retries.</param>
        /// <returns></returns>
        public static string WaitForText(this WebElement element, int retries = 0)
            => WaitForCondition(() => element.Text, e => e.Length == 0, retries);

        /// <summary>
        /// This method will wait for text value is same as in bool expression you entered in param condition.
        /// If you don't want to wait set default param retries value to 1. 
        /// </summary>
        /// <param name="element"></param>
        /// <param name="repeatCondition">Condition for text.</param>
        /// <param name="retries">Default value is 0, that means it will use your application settings values for retries.</param>
        /// <returns></returns>
        public static string WaitForText(this WebElement element, Func<string,bool> repeatCondition, int retries = 0)
            => WaitForCondition(() => element.Text, repeatCondition, retries);

        /// <summary>
        /// This method will wait for any condition on WebElement.
        /// If you want to specify different count of retries from default you can set number of retries in parameter
        /// after condition. 
        /// </summary>
        /// <param name="element"></param>
        /// <param name="repeatCondition"></param>
        /// <param name="retries">Default value is 0, that means it will use your application settings values for retries.</param>
        public static void WaitFor(this WebElement element, Func<WebElement, bool> repeatCondition, int retries = 0)
            => WaitForCondition(() => element, repeatCondition, retries);

        public static void WaitForDisplayed(this WebElement element, int seconds = 15)
            => WaitForCondition(() => element, x => x.Displayed, seconds);

        public static void WaitForNotDisplayed(this WebElement element, int seconds = 15)
            => WaitForCondition(() => element, x => !x.Displayed, seconds);
        
        public static void ScrollToElement(this WebElement element) => (GetDriver as IJavaScriptExecutor)?
            .ExecuteScript("arguments[0].scrollIntoView({block: 'end'})", element.GetNative());
    }
}