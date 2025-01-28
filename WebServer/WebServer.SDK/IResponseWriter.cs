namespace WebServer.SDK
{
    public interface IResponseWriter
    {
        Task SendResponseAsync(WResponse response);
    }
}
