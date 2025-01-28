using WebServer.SDK;

namespace WebServer.Server
{
    internal class RequestLine
    {
        public required WMethods Method { get; set; }
        public string Url { get; set; } = string.Empty;
        public string HttpVersion { get; set; } = string.Empty;
    }
}
