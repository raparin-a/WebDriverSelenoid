namespace SeleniumWebDriver.Elements.Locators
{
    public class SearchStrategy
    {
        public Locator Locator { get; }
        public int Index { get; } 
        
        public SearchStrategy() { }
        public SearchStrategy(Locator locator) =>  Locator = locator;
        
        public SearchStrategy(Locator locator, int index)
        {
            Locator = locator;
            Index = index;
        }
    }
}