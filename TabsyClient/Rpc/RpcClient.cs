using System.IO;
using System.Net.Sockets;
using System.Threading.Tasks;
using System;
using AustinHarris.JsonRpc;
using TabsyClient.Logging;
using System.Text;
using Newtonsoft.Json;
using System.Runtime.InteropServices.ComTypes;

namespace TabsyClient.Rpc
{
    public class RpcClient
    {
        private TcpClient Client { get; set; }
        public Action<StreamWriter, string> HandleRequest { get; set; }
        private static object service; // From https://github.com/Astn/JSON-RPC.NET/wiki/Getting-Started-(Sockets)
        private static int Counter { get; set; } = 0;
        private StreamReader reader;
        private StreamWriter writer;
        private static RpcClient Instance { get; set; }

        public RpcClient()
        {
            Instance = this;
        }

        public void Connect(string host, int port)
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

            try
            {
                Client = new TcpClient(host, port);
            }
            catch (ArgumentNullException)
            {
                Logger.Error("Cannot create tcp client: an argument is null");
                return;
            }
            catch (ArgumentOutOfRangeException)
            {
                Logger.Error("Cannot create tcp client: an argument is out of range");
                return;
            }
            catch (SocketException e)
            {
                Logger.Error($"Error while creating tcp client: {e.Message}");
                return;
            }

            if (!Client.Connected)
            {
                Logger.Error($"Cannot connect to {host}:{port}");
                return;
            }

            Task t = new Task(() =>
            {
                while (Client.Connected)
                {
                    try
                    {
                        var stream = Client.GetStream();
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

        public static void TriggerRpc(string name, params object[] parameter)
        {
            RpcModel m = new RpcModel()
            {
                Method = name,
                Id = Counter,
                Params = parameter
            };

            Counter++;

            Instance.Send(JsonConvert.SerializeObject(m));
        }
        private void Send(string s)
        {
            writer.Flush();
            writer.WriteLine(s);
            writer.Flush();
        }
    }
}