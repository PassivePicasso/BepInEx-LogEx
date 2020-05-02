using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WebSocketSharp;

namespace WebSocketLogClient
{
    public class LogClient
    {
        static void Main(string[] args)
        {
            var socketThread = new Thread(StartSocketClient);
            socketThread.Start();
            Console.ReadKey();
        }

        private static void StartSocketClient()
        {
            var ws = new WebSocket("ws://localhost:5892/Log");
            ws.OnMessage += Ws_OnMessage;
            ws.OnClose += Ws_OnClose;
            ws.OnError += Ws_OnError;
            ws.OnOpen += Ws_OnOpen;
            ws.Connect();
        }

        private static void Ws_OnMessage(object sender, MessageEventArgs e)
        {
            Console.WriteLine(e.Data);
        }
        private static void Ws_OnOpen(object sender, EventArgs e)
        {
            Console.WriteLine("Connection established");
        }

        private static void Ws_OnError(object sender, ErrorEventArgs e)
        {
            Console.WriteLine($"Connection error {e.Message}");
        }

        private static void Ws_OnClose(object sender, CloseEventArgs e)
        {
            Console.WriteLine($"Connection closed: {e.Code}:{e.Reason}");
        }
    }
}
