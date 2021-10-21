using SimpleTcp;

using System;

using TabsyClient.Josy;
using TabsyClient.Logging;

namespace TabsyClient
{
    public class Program
    {
        public static SimpleTcpClient TcpClient { get; set; }
        public static void Main(string[] args)
        {
            Logger.Info($"Starting TabsyClient v0.1");
            Logger.Info("© 2021 Endelon Hosting");
            Logger.Info("www.endelon-hosting.de");

            if (args.Length < 1)
            {
                Logger.Error("No target specified. Usage 'tabsyclient.exe <ip>:<port>'");
            }

            Logger.Info($"Connecting to {args[0]}");
            TcpClient = new SimpleTcpClient(args[0]);

            TcpClient.Events.Connected += OnConnected;
            TcpClient.Events.Disconnected += OnDisconnected;
            TcpClient.Events.DataReceived += OnDataReceived;

            TcpClient.Connect();

            Console.ReadLine();
        }

        private static void OnConnected(object sender, ClientConnectedEventArgs e)
        {
            Logger.Info("Connected to TabsyDaemon");

            Logger.Info("Starting Josy");
            JosyCallbacks.Josy.RemoteProvider = new TcpJosyRemoteProvider(); // Set the remote provider
        }

        private static void OnDisconnected(object sender, ClientDisconnectedEventArgs e)
        {
            Logger.Info("Disconnected to TabsyDaemon");
        }

        private static void OnDataReceived(object sender, DataReceivedEventArgs e)
        {
            string data = System.Text.Encoding.UTF8.GetString(e.Data);

            Josy.TcpJosyRemoteProvider.ReceiveData(data);
        }
    }
}
