using System;

using TabsyClient.Commands;
using TabsyClient.Logging;
using TabsyClient.Rpc;

namespace TabsyClient
{
    public class Program
    {
        private static RpcClient RpcClient { get; set; }
        public static void Main(string[] args)
        {
            Logger.Info($"Starting TabsyClient v0.1");
            Logger.Info("© 2021 Endelon Hosting");
            Logger.Info("www.endelon-hosting.de");

            #region RPC

            if (args.Length < 1)
            {
                Logger.Error("No target specified. Usage 'tabsyclient.exe <ip>:<port>'");
                return;
            }

            string host;
            int port;

            try
            {
                host = args[0].Split(":")[0];
                port = int.Parse(args[0].Split(":")[1]);
            }
            catch (Exception e)
            {
                Logger.Error($"Error parsing connection target: {e.Message}");
                return;
            }

            RpcClient = new RpcClient();
            RpcClient.Connect(host, port);

            #endregion RPC

            #region Commands

            CommandController.RegisterCommand("ping", new PingCommand());
            CommandController.RegisterCommand("docker", new DockerCommand());
            CommandController.RegisterCommand("debug", new DebugCommand());

            #endregion

            while (true)
            {
                string input = Console.ReadLine();

                if (input == "exit")
                    break;

                CommandController.Process(input);
            }
        }
    }
}
