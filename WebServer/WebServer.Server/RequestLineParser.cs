using WebServer.SDK;

namespace WebServer.Server
{
    internal static class RequestLineParser
    {
        public static bool TryParse(string line, out RequestLine? requestLine)
        {
            requestLine = null;
            ArgumentNullException.ThrowIfNull(line);

            //GET / HTTP/1.1
            //POST / HTTP/1.1

            var parts = line.Split(' ');
            if (parts.Length != 3)
            {
                return false;
            }

            if (!Enum.TryParse(parts[0], true, out WMethods method))
            {
                return false;
            }

            requestLine = new RequestLine
            {
                Method = method,
                Url = parts[1],
                HttpVersion = parts[2]
            };

            return true;
        }
    }
}
