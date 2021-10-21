using SimpleTcp;

using System.Threading.Tasks;

using TabsyDaemon.Interfaces;
using TabsyDaemon.Logging;

namespace TabsyDaemon.Services
{
    public class JosyService : ITabsyService
    {
        public bool IsRunning { get { return TcpServer.IsListening; } }
        private SimpleTcpServer TcpServer { get; set; }

        public async void Start()
        {
            TcpServer = new SimpleTcpServer(Environment.JosyBind);

            TcpServer.Events.ClientConnected += OnClientConnected;
            TcpServer.Events.ClientDisconnected += OnClientDisconnected;
            TcpServer.Events.DataReceived += OnDataReceived;

            TcpServer.Logger = OnLog;

            Logger.Info($"Binding Josy on {Environment.JosyBind}");

            await TcpServer.StartAsync();
        }

        // Logger Callback for TcpServer
        private void OnLog(string obj)
        {
            Logger.Info("Network: " + obj);
        }

        #region Network Callbacks

        private void OnDataReceived(object sender, DataReceivedEventArgs e)
        {
            
        }

        private void OnClientDisconnected(object sender, ClientDisconnectedEventArgs e)
        {
            Logger.Debug($"TabsyClient disconnected. Remote endpoint: {e.IpPort}");
        }

        private void OnClientConnected(object sender, ClientConnectedEventArgs e)
        {
            Logger.Debug($"TabsyClient connected. Remote endpoint: {e.IpPort}");
        }

        #endregion

        public async void Stop()
        {
            TcpServer.Stop();
        }
    }
}
