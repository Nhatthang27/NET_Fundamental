namespace MiddlewareDemo.ClientInfoRepository
{
    public class ClientInfoRepositoryFactory : IClientInfoRepositoryFactory
    {
        public IClientInfoRepository Create()
        {
            return new ClientInfoRepository();
        }
    }
}