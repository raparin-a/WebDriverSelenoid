using System.IO;
using SeleniumWebDriver.Configuration;

namespace SeleniumWebDriver.Helpers
{
    internal static class DirectoryHelper
    {
        internal static string CreateAndReturn(string directoryName, string testName) => Directory.CreateDirectory(Path.Combine(Settings.CurrentPath, directoryName, testName)).ToString();
    }
}