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

        if (IsSessionIdFormatValid(sessionId))
        {
            var session = context.RequestServices.GetRequiredService<IMySessionStorage>().Get(sessionId!);
            context.Response.Cookies.Append(SESSION_ID_COOKIE_NAME, session.Id);

            return session;
        }
        else
        {
            var session = context.RequestServices.GetRequiredService<IMySessionStorage>().Create();
            context.Response.Cookies.Append(SESSION_ID_COOKIE_NAME, session.Id);

            return session;
        }
    }

    private static bool IsSessionIdFormatValid(string? sessionId)
    {
        return !string.IsNullOrWhiteSpace(sessionId) && Guid.TryParse(sessionId, out _);
    }
}