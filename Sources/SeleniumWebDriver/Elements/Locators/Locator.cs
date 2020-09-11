namespace SeleniumWebDriver.Elements.Locators
{
    public class Locator
    {
        internal Locator(By by, string @using)
        {
            By = by;
            Using = @using;
        }

        public By By { get; }
        public string Using { get;}

        public override string ToString()
        {
            return $"Find element by {By} - {Using}";
        }
    }
}