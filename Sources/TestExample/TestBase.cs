using NUnit.Framework;
using NUnit.Framework.Interfaces;
using SeleniumWebDriver.Configuration;
using SeleniumWebDriver.DriverManager;
using static SeleniumWebDriver.Configuration.Selenoid;

namespace TestExample
{
    [TestFixture]
    [Parallelizable(ParallelScope.All)]
    public class TestsBase
    {
        [SetUp]
        public void Init()
        {
            SetBrowserName(CurrentTestName);
            SetLogName(CurrentTestName);
            SetVideoName(CurrentTestName);
        }

        [TearDown]
        public void Cleanup()
        {
            switch (TestContext.CurrentContext.Result.Outcome.Status)
            {
                case TestStatus.Failed:
                    Browser.TakeScreenshots(CurrentTestName);
                    Browser.CloseBrowser();
                    break;
                case TestStatus.Passed:
                    Browser.CloseBrowser();
                    DeleteLog(CurrentTestName);
                    DeleteVideo(CurrentTestName);    
                    break;
            }
        }

        private static string CurrentTestName => TestContext.CurrentContext.Test.Name;
    }
    [SetUpFixture]
    public class AssemblySetUp
    {
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Settings.ConfigureWebDriver();
        }
        
        [OneTimeTearDown]
        public void BaseTearDown()
        {
        }
    }
}