using System;

using TabsyClient.Logging;

namespace TabsyClient
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Logger.Info($"Starting TabsyClient v0.1");
            Logger.Info("© 2021 Endelon Hosting");
            Logger.Info("www.endelon-hosting.de");

            if (args.Length < 1)
            {
                Logger.Error("No target specified. Usage 'tabsyclient.exe <ip>:<port>'");
            }
        }
    }
}
