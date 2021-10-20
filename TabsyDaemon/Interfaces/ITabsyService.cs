using System.Threading.Tasks;

namespace TabsyDaemon.Interfaces
{
    public interface ITabsyService
    {
        public Task Start();
        public Task Stop();
        public bool IsRunning { get; }
    }
}
