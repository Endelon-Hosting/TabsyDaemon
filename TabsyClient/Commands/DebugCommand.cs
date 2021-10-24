using TabsyClient.Rpc;

namespace TabsyClient.Commands
{
    public class DebugCommand : ICommand
    {
        public void Call(string[] args)
        {
            RpcClient.TriggerRpc("debug");
        }
    }
}
