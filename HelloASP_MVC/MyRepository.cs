using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelloASP_MVC
{
    public class MyRepository : IRepository
    {
        private readonly ILogger<MyRepository> _logger;
        public MyRepository(ILogger<MyRepository> logger)
        {
            _logger = logger;
            _logger.LogInformation("new my repository");
        }

        public string GetId(string name)
        {
            return "ID: " + name;
        }
    }
}