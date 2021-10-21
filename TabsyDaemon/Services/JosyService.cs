using System.Threading.Tasks;

using TabsyDaemon.Interfaces;

namespace TabsyDaemon.Services
{
    public class JosyService : ITabsyService
    {
        public bool IsRunning => throw new System.NotImplementedException();

        public Task Start()
        {
            throw new System.NotImplementedException();
        }

        public Task Stop()
        {
            throw new System.NotImplementedException();
        }
    }
}
