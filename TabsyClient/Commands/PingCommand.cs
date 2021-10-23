using System;

using AustinHarris.JsonRpc;

using TabsyClient.Rpc;

namespace TabsyClient.Commands
{
    public class PingCommand : ICommand
    {
        public void Call(string[] args)
        {
            DateTime time = DateTime.Now;
            Console.WriteLine("Pining");

            RpcClient.TriggerRpc("Ping");
        }
    }
}