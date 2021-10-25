using System.IO;
using System;
using System.Net;

using TabsyDaemon.Interfaces;
using System.Net.Sockets;
using TabsyDaemon.Logging;
using System.Threading.Tasks;
using System.Text;
using AustinHarris.JsonRpc;
using TabsyDaemon.Rpc;
using Newtonsoft.Json;

namespace TabsyDaemon.Services
{
    public class RpcService : ITabsyService
    {
        public bool IsRunning { get; set; }
        public Action<StreamWriter, string> HandleRequest { get; set; }
        private TcpListener Listener { get; set; }
        private static object service; // From https://github.com/Astn/JSON-RPC.NET/wiki/Getting-Started-(Sockets)
        private StreamReader reader;
        private StreamWriter writer;
        private int Counter { get; set; } = 0;
        public static RpcService Instance { get; private set; }

        public RpcService()
        {
            Instance = this;
        }

        public async void Start()
        {
            service = new RpcHandler();

            var rpcResultHandler = new AsyncCallback(
                state =>
                {
                    var async = ((JsonRpcStateAsync)state);
                    var result = async.Result;
                    var writer = ((StreamWriter)async.AsyncState);

                    //writer.WriteLine(result);
                    //writer.FlushAsync();
                }
            );

            HandleRequest = (writer, line) =>
            {
                var async = new JsonRpcStateAsync(rpcResultHandler, writer) { JsonRpc = line };
                JsonRpcProcessor.Process(async, writer);
            };

            IsRunning = true;

            IPAddress ip;

            try
            {
                ip = IPAddress.Parse(Environment.RpcBindIp);
            }
            catch (ArgumentNullException)
            {
                Logger.Error("The rpc bind host is not defined");
                return;
            }
            catch (FormatException)
            {
                Logger.Error("Invalid format for rpc host");
                return;
            }
            catch (Exception e)
            {
                Logger.Error($"Unhandled exception: {e.Message}");
                return;
            }

            int port;

            try
            {
                port = int.Parse(Environment.RpcBindPort);
            }
            catch (ArgumentNullException)
            {
                Logger.Error("The rpc port is not defined");
                return;
            }
            catch (FormatException)
            {
                Logger.Error("Invalid format for rpc port");
                return;
            }
            catch (Exception e)
            {
                Logger.Error($"Unhandled exception: {e.Message}");
                return;
            }

            Logger.Info($"Starting rpc server on {ip.ToString()}:{port}");

            Listener = new TcpListener(ip, port);

            try
            {
                Listener.Start();
            }
            catch (SocketException e)
            {
                Logger.Error($"Socket exception while starting listener: {e.Message}");
                return;
            }

            Task t = new Task(() =>
            {
                while (IsRunning)
                {
                    try
                    {
                        var client = Listener.AcceptTcpClient();
                        var stream = client.GetStream();
                        Logger.Debug("Rpc Network: Client Connected..");
                        reader = new StreamReader(stream, Encoding.UTF8);
                        writer = new StreamWriter(stream, new UTF8Encoding(false));

                        while (!reader.EndOfStream)
                        {
                            var line = reader.ReadLine();

                            HandleRequest(writer, line);

                            //Logger.Debug($"Rpc Network: REQUEST: {line}");
                        }
                    }
                    catch (Exception e)
                    {
                        Logger.Error($"Rpc Network: Unhandeld exception {e}");
                    }
                }
            });
            t.Start();
        }

        public async void Stop()
        {
            IsRunning = false;

            Logger.Info("Closing rpc server");

            try
            {
                Listener.Stop();
            }
            catch (SocketException e)
            {
                Logger.Error($"Socket exception while closing listener: {e.Message}");
                return;
            }
        }

        public void TriggerRpc(RpcModel m)
        {
            m.Id = Counter;
            Counter++;

            writer.Flush();
            writer.WriteLine(JsonConvert.SerializeObject(m));
            writer.Flush();
        }
    }
}
