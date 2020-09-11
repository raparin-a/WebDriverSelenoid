namespace SeleniumWebDriver.Elements.BaseTypes.Interfaces
{
    public interface IWebPage : ISearchable, INative
    {
        string Address { get; set; }
        void Open(string query = null);
        string Title { get; set; }
        void WaitForLoaded(int retryCount);
        void ScrollDown(string number);
    }
}