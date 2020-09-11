using OpenQA.Selenium;
using SeleniumWebDriver.Elements.BaseTypes.Interfaces;
using SeleniumWebDriver.Elements.Extensions;
using SeleniumWebDriver.Elements.Factories;
using SeleniumWebDriver.Elements.Locators;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace SeleniumWebDriver.Elements.BaseTypes
{
    public class WebElementsCollection<T> : INative, IElementsCollection<T> where T : IElement, new()
    {
        private INative Parent { get; }
        private Locator Locator { get; }
        private IWebElement[] _nativeElements;

        public WebElementsCollection(INative parent, Locator locator)
        {
            Parent = parent;
            Locator = locator;
        }

        [Obsolete("Finding an element using 'How' FindElement(Locator locator) is deprecated. Please use 'By' FindElement(By by, string locator)")]
        public IWebElement FindElement(Locator locator, int index)
        {
            if (CollectionRequiresReInitialization(_nativeElements, index))
                InitCollection();
            if (IndexOutOfRange(_nativeElements, index))
                throw new IndexOutOfRangeException($"Collection does not contain element with index {index}");
            return _nativeElements[index];
        }

        public IWebElement FindElement(Locators.By by, string locator, int index)
        {
            if (CollectionRequiresReInitialization(_nativeElements, index))
                InitCollection();
            if (IndexOutOfRange(_nativeElements, index))
                throw new IndexOutOfRangeException($"Collection does not contain element with index {index}");
            return _nativeElements[index];
        }
        //TODO do we need this realization?
        [Obsolete("Finding an element using 'How' FindElements(Locator locator) is deprecated. Please use 'By' FindElements(By by, string locator)")]
        public IReadOnlyCollection<IWebElement> FindElements(Locator locator) => throw new NotImplementedException();

        public IReadOnlyCollection<IWebElement> FindElements(Locators.By by, string locator) => throw new NotImplementedException();

        public IEnumerator<T> GetEnumerator()
        {
            InitCollection();
            for (var i = 0; i < _nativeElements.Length; i++)
            {
                var webElement = WebElementFactory.Create<T>(this, Locator, i);
                webElement.SetNativeElement(_nativeElements[i]);
                yield return webElement;
            }
        }

        public T this[int index]
        {
            get
            {
                InitCollection();
                var webElement = WebElementFactory.Create<T>(this, Locator, index);
                webElement.SetNativeElement(_nativeElements[index]);
                return webElement;
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        private static bool IndexOutOfRange(IReadOnlyCollection<IWebElement> collection, int index) => collection.Count < index + 1;
        private void InitCollection() => _nativeElements = Parent.FindElements(Locator.By, Locator.Using).ToArray();
        private static bool CollectionRequiresReInitialization(IReadOnlyList<IWebElement> collection, int index) => IndexOutOfRange(collection, index) || !collection[index].Exists();


    }
}