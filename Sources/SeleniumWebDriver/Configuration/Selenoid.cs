using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml.Serialization;
using static SeleniumWebDriver.DriverManager.CapabilitiesFactory;

namespace SeleniumWebDriver.Configuration
{
    public class Selenoid
    {
        private static readonly HttpClient HttpClient = new HttpClient {BaseAddress = new Uri(Settings.WebDriverOptions.Value.SelenoidOptions.HubUrl)};
        public static async Task DeleteVideo(string fileName) => await HttpClient.DeleteAsync("/video/" + fileName + ".mp4");
        public static async Task DeleteLog(string fileName) => await HttpClient.DeleteAsync("/log/" + fileName + ".log");

        public static void SetBrowserName(string value) => AddSelenoidCapabilities("name", value);
        public static void SetVideoName(string value) => AddSelenoidCapabilities("videoName", value + ".mp4");
        public static void SetLogName(string value) => AddSelenoidCapabilities("logName", value + ".log");

        public static IEnumerable<string> GetFilesList(string sessionId)
        {
            var hubUrl = Settings.WebDriverOptions.Value.SelenoidOptions.HubUrl;
            var url = $"{hubUrl}/download/{sessionId}";
            var request = WebRequest.Create(url) as HttpWebRequest;
            var html = string.Empty;

            using var response = (HttpWebResponse)request?.GetResponse();
            using var stream = response?.GetResponseStream();
            using var reader = new StreamReader(stream);
            html = reader.ReadToEnd();
            var data = new XmlSerializer(typeof(DownloadData)).Deserialize(new StringReader(html)) as DownloadData;

            return data?.Items;
        }

        public static string GetFileContent(string sessionId, string fileName)
        {
            var hubUrl = Settings.WebDriverOptions.Value.SelenoidOptions.HubUrl;
            var url = $"{hubUrl}/download/{sessionId}/{fileName}";
            var request = WebRequest.Create(url) as HttpWebRequest;
            var content = string.Empty;

            using var response = (HttpWebResponse)request?.GetResponse();
            using var stream = response?.GetResponseStream();
            using var reader = new StreamReader(stream);
            content = reader.ReadToEnd();

            return content;
        }
    }
}