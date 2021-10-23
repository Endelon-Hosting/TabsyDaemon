using System;

using AustinHarris.JsonRpc;

using TabsyClient.Rpc;

namespace TabsyClient.Commands
{
    public class PingCommand : ICommand
    {
        public void Call(string[] args)
        {
            Console.WriteLine("Ping");

            RpcClient.TriggerRpc("Ping", DateTime.Now);
        }
    }
}