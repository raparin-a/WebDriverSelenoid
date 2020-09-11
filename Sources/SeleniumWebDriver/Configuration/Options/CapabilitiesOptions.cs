namespace SeleniumWebDriver.Configuration.Options
{
    public class CapabilitiesOptions
    {
        public bool enableVNC { get; set; }
        public bool enableLogs { get; set; }
        public bool enableVideo { get; set; }
        public string screenResolution { get; set; }
        public string videoScreenSize { get; set; }
        public int videoFrameRate { get; set; }
        public string videoCodec { get; set; }
        public string chromeVersion { get; set; }
    }
}