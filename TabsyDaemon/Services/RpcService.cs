using System.IO;
using System;
using System.Net;

using TabsyDaemon.Interfaces;
using System.Net.Sockets;
using TabsyDaemon.Logging;

namespace TabsyDaemon.Services
{
    public class RpcService : ITabsyService
    {
        public bool IsRunning  { get; }
        public Action<StreamWriter, string> HandleRequest { get; set; }
        private TcpListener Listener { get; set; }

        public async void Start()
        {
            IPAddress ip;

            try
            {
                ip = IPAddress.Parse(Environment.RpcBindIp);
            }
            catch(ArgumentNullException)
            {
                Logger.Error("The rpc bind host is not defined");
                return;
            }
            catch(FormatException)
            {
                Logger.Error("Invalid format for rpc host");
                return;
            }
            catch(Exception e)
            {
                Logger.Error($"Unhandled exception: {e.Message}");
                return;
            }

            int port;

            try
            {
                port = int.Parse(Environment.RpcBindPort);
            }
            catch(ArgumentNullException)
            {
                Logger.Error("The rpc port is not defined");
                return;
            }
            catch(FormatException)
            {
                Logger.Error("Invalid format for rpc port");
                return;
            }
            catch(Exception e)
            {
                Logger.Error($"Unhandled exception: {e.Message}");
                return;
            }

            Logger.Info($"Starting rpc server on {ip.ToString()}:{port}");

            Listener = new TcpListener(ip, port);
        }

        public async void Stop()
        {
            
        }
    }
}
