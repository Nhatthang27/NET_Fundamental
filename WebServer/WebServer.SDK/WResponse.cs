namespace WebServer.SDK
{
    public class WResponse
    {
        public string HttpVersion { get; set; } = "HTTP/1.1";
        public HttpResponseCodes HttpResponseCodes { get; set; } = HttpResponseCodes.NotFound;
        public string HttpResponsePhrases { get; set; } = string.Empty;
        public int ContentLength { get; set; } = 0;
        public string ContentType { get; set; } = "text/html";
        public IResponseBodyWriter BodyWriter { get; set; } = NullResponseBodyWriter.Instance;
    }
}
