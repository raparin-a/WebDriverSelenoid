using System;
using System.Collections.Generic;
using System.Linq;
using SeleniumWebDriver.Elements.BaseTypes;
using static SeleniumWebDriver.Helpers.WaitHelper;

namespace SeleniumWebDriver.Elements.Extensions
{
    public static class WebElementsCollectionExtensions
    {
        /// <summary>
        /// This method will wait for any condition on collection of WebElements.
        /// If you want to specify different count of retries from default you can set number of retries in parameter
        /// after condition. 
        /// </summary>
        /// <param name="elements"></param>
        /// <param name="repeatCondition"></param>
        /// <param name="retries">Default value is 0, that means it will use your application settings values for retries.</param>
        public static void WaitFor(this IEnumerable<WebElement> elements, Func<IEnumerable<WebElement>, bool> repeatCondition, int retries = 0)
            => WaitForCondition(() => elements, repeatCondition, retries);

        /// <summary>
        /// This method will wait until WebElements collections contains expected items count.
        /// If you don't want to wait set default param retries value to 1. 
        /// </summary>
        /// <param name="elements"></param>
        /// <param name="itemsCount"></param>
        /// <param name="retries">Default value is 0, that means it will use your application settings values for retries.</param>
        public static void WaitForItemsCount(this IEnumerable<WebElement> elements, int itemsCount, int retries = 0)
            => WaitForCondition(() => elements, e => e.Count() != itemsCount, retries);
    }
}
