using System;

using TabsyClient.Rpc;

namespace TabsyClient.Commands
{
    public class DockerCommand : ICommand
    {
        public void Call(string[] args)
        {
            if(args.Length > 1)
            {
                switch(args[1])
                {
                    case "status":
                        RpcClient.TriggerRpc("docker.containers.count.request");
                        RpcClient.TriggerRpc("docker.images.count.request");
                        break;
                    default:
                        Console.WriteLine($"No option for '{args[0]}' found");
                        break;
                }
            }
        }
    }
}
