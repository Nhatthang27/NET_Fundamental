namespace WebServer.SDK
{
    public class WRequest
    {
        public required WMethods Method { get; set; }
        public required string Url { get; set; }
        public required string HttpVersion { get; set; }
        public required string Host { get; set; }
        public bool IsKeepAlive { get; set; } = false;
        public required IDictionary<string, string> Headers { get; set; }
    }
}
