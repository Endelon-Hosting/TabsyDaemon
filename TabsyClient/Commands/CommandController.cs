using System;
using System.Collections.Generic;

namespace TabsyClient.Commands
{
    public class CommandController
    {
        private static Dictionary<string, ICommand> Commands { get; set; } = new Dictionary<string, ICommand>();

        public static void RegisterCommand(string name, ICommand command)
        {
            lock(Commands)
            {
                Commands.Add(name, command);
            }
        }

        public static void Process(string input)
        {
            string[] parts = input.Split(" ");

            if(!Commands.ContainsKey(parts[0]))
            {
                Console.WriteLine($"Command {parts[0]} not found");
                return;
            }

            lock(Commands)
            {
                Commands[parts[0]].Call(parts);
            }
        }
    }
}
