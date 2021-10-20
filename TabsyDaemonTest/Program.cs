using System;

using TabsyDaemon;

namespace TabsyDaemonTest
{
    public class Program
    {
        private static Tabsy Tabsy { get; set; }

        static void Main(string[] args)
        {
            Tabsy = new Tabsy();

            Tabsy.Start();

            Console.ReadLine();
        }
    }
}
