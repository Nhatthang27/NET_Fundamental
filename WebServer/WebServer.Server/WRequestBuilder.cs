using Microsoft.Extensions.Primitives;
using WebServer.SDK;

namespace WebServer.Server
{
    internal class WRequestBuilder
    {
        public WMethods Method { get; set; } = WMethods.Get;
        public string Url { get; set; } = string.Empty;
        public string HttpVersion { get; set; } = string.Empty;
        public string Host { get; set; } = string.Empty;
        public bool IsKeepAlive { get; set; } = false;
        public IDictionary<string, string> Headers { get; set; } = new Dictionary<string, string>();

        public WRequest Build()
        {
            Validate();
            var request = new WRequest()
            {
                Method = Method,
                Url = Url,
                HttpVersion = HttpVersion,
                Host = Host,
                IsKeepAlive = IsKeepAlive,
                Headers = Headers
            };
            return request;
        }

        private void Validate()
        {
            if (string.IsNullOrEmpty(Url))
            {
                throw new ArgumentException("Url is required");
            }
            if (string.IsNullOrEmpty(HttpVersion))
            {
                throw new ArgumentException("HttpVersion is required");
            }
            if (string.IsNullOrEmpty(Host))
            {
                throw new ArgumentException("Host is required");
            }
        }

        internal void AddHeader(WHeader header)
        {
            if ("Host".Equals(header.Name, StringComparison.OrdinalIgnoreCase))
            {
                Host = header.Values.First() ?? string.Empty;
            }
            else if ("Connection".Equals(header.Name, StringComparison.OrdinalIgnoreCase))
            {
                IsKeepAlive = "keep-alive".Equals(header.Values.First(), StringComparison.OrdinalIgnoreCase);
            }

            if (!Headers.TryGetValue(header.Name, out var value))
            {
                Headers.Add(header.Name, header.Values!);
            }
            else
            {
                Headers[header.Name] = StringValues.Concat(value, header.Values)!;
            }
        }
    }
}
