using JosyCallbacks;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using TabsyClient.Logging;

namespace TabsyClient.Josy
{
    public class TcpJosyRemoteProvider : IJosyRemoteProvider
    {
        private static int Counter = 0;

        private static Dictionary<int, string> Received = new Dictionary<int, string>();

        public string SendData(string data)
        {
            int i = Counter;
            Program.TcpClient.Send($"{Counter}+" + data);
            Counter++;

            Task t = new Task(() =>
            {
                while(Program.TcpClient.IsConnected)
                {
                    if (Received.ContainsKey(i))
                        break;
                    else
                        Task.Delay(10);
                }
            });

            t.Start(); // Start the task an wait for it to finish
            t.Wait();

            string result = "";

            lock(Received)
            {
                result = Received[i];
                Received.Remove(i);
            }

            return result;
        }

        public static void ReceiveData(string data)
        {
            //JosyCallbacks.Josy.

            try
            {
                string[] s = data.Split("+");

                int id = int.Parse(s[0]); // Getting id

                lock (Received)
                {
                    Received.Add(id, s[1]);
                }
            }
            catch(Exception e)
            {
                Logger.Error($"Error receiving data: {e.Message}");
            }
        }
    }
}
