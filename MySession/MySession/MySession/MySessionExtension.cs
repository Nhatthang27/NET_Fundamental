using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MySession.MySession;
public static class MySessionExtension
{
    private const string SESSION_ID_COOKIE_NAME = "MySession";
    public static ISession GetSession(this HttpContext context)
    {
        string? sessionId = context.Request.Cookies[SESSION_ID_COOKIE_NAME];

        var session = IsSessionIdFormatValid(sessionId) ?
                        context.RequestServices.GetRequiredService<IMySessionStorage>().Get(sessionId!)
                        : context.RequestServices.GetRequiredService<IMySessionStorage>().Create();
        context.Response.Cookies.Append(SESSION_ID_COOKIE_NAME, session.Id);
        return session;
    }

    private static bool IsSessionIdFormatValid(string? sessionId)
    {
        return !string.IsNullOrWhiteSpace(sessionId) && Guid.TryParse(sessionId, out _);
    }
}