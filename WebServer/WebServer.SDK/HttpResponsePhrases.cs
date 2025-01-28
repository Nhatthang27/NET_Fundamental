using System;

namespace WebServer.SDK;

public static class HttpResponsePhrases
{
    public static string GetPhrase(HttpResponseCodes code)
    {
        return code switch
        {
            HttpResponseCodes.OK => "OK",
            HttpResponseCodes.BadRequest => "Bad Request",
            HttpResponseCodes.NotFound => "Not Found",
            HttpResponseCodes.InternalServerError => "Internal Server Error",
            _ => throw new NotImplementedException()
        };
    }
}
