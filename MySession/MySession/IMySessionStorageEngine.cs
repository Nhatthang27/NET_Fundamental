using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MySession.MySession
{
    public interface IMySessionStorageEngine
    {
        Task CommitAsync(string id, Dictionary<string, byte[]> store, CancellationToken cancellationToken);
        Task<Dictionary<string, byte[]>> LoadAsync(string id, CancellationToken cancellationToken);
    }
}