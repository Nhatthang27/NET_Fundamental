using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebServer.SDK;

namespace WebServer.Server
{
    public static class HeaderParser
    {
        public static bool TryParse(string line, out WHeader? header)
        {
            header = null;
            ArgumentNullException.ThrowIfNull(line);

            //Host: localhost:8080
            //Connection: keep-alive

            var parts = line.Split(": ");
            if (parts.Length != 2)
            {
                return false;
            }

            header = new WHeader
            {
                Name = parts[0],
                Values = parts[1]
            };

            return true;
        }
    }
}