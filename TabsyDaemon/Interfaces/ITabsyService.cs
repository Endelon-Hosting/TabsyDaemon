using System.Threading.Tasks;

namespace TabsyDaemon.Interfaces
{
    public interface ITabsyService
    {
        public void Start();
        public void Stop();
        public bool IsRunning { get; }
    }
}
