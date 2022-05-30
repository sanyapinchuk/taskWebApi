using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebServer.Data;

namespace Tests.Common
{
    public abstract class TestCommandBase:IDisposable
    {
        protected readonly DataContext dataContext;
        protected readonly RepositoryManager repository;
        public TestCommandBase()
        {
            dataContext = FridgeContextFactory.Create();
            repository = new RepositoryManager(dataContext);
        }
        public void Dispose()
        {
            FridgeContextFactory.Destroy(dataContext);
        }
    }
}
