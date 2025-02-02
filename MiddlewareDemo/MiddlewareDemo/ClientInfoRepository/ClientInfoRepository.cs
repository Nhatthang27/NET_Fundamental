using MiddlewareDemo.Models;

namespace MiddlewareDemo.ClientInfoRepository
{
    public class ClientInfoRepository : IClientInfoRepository
    {
        private readonly Dictionary<string, ClientInfo> _clientInfo = new Dictionary<string, ClientInfo>
        {
            { "123", new ClientInfo { Id = 1, UserName = "User1" } },
            { "456", new ClientInfo { Id = 2, UserName = "User2" } },
            { "789", new ClientInfo { Id = 3, UserName = "User3" } }
        };
        public ClientInfo? GetClientInfo(string apiKey)
        {
            if (_clientInfo.ContainsKey(apiKey))
            {
                return _clientInfo[apiKey];
            }
            return null;
        }
    }
}
