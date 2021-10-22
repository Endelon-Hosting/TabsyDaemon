using System;

namespace TabsyClient.Commands
{
    public class PingCommand : ICommand
    {
        public void Call(string[] args)
        {
            DateTime time = DateTime.Now;
            Console.WriteLine("Pining");


        }
    }
}