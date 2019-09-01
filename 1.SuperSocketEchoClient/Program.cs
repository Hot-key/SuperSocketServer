using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using SuperSocket.ClientEngine;

namespace _1.SuperSocketEchoClient
{
    class Program
    {
        private static AsyncTcpSession tcpSession;
        static void Main(string[] args)
        {
            tcpSession = new AsyncTcpSession();

            tcpSession.Connected += tcpSession_Connected;
            tcpSession.Closed += tcpSession_Closed;
            tcpSession.DataReceived += tcpSession_DataReceived;
            tcpSession.Error += tcpSession_Error;
            tcpSession.Connect(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 3000));
            var tmpData = "";
            while (tmpData != "q")
            {
                tmpData = Console.ReadLine();

                var buffer = new CGD.buffer(50);

                buffer.append<int>(123);
                buffer.append<short>(0);
                buffer.append<string>(tmpData);
                buffer.set_front<short>(buffer.Count, 4);

                tcpSession.Send(buffer);
            }
        }

        private static void tcpSession_Error(object sender, ErrorEventArgs e)
        {
            Console.WriteLine("tcpSession_Error");
        }

        private static void tcpSession_DataReceived(object sender, DataEventArgs e)
        {
            AsyncTcpSession session = sender as AsyncTcpSession;

            byte[] tmpBuffer = e.Data;
            var buffer = new CGD.buffer(e.Data, 0, e.Length);

            int bufferType = (int)buffer.extract_uint();
            ushort bufferLength  = (ushort)buffer.extract_short();
            string bufferData = buffer.extract_string();
                
            Console.WriteLine("tcpSession_DataReceived");
            Console.WriteLine("---------------------------------");
            Console.WriteLine("Type   : " + bufferType);
            Console.WriteLine("Length : " + bufferLength);
            Console.WriteLine("Data   : " + bufferData);
            Console.WriteLine("---------------------------------");
        }

        private static void tcpSession_Closed(object sender, EventArgs e)
        {
            Console.WriteLine("tcpSession_Closed");
        }

        private static void tcpSession_Connected(object sender, EventArgs e)
        {
            AsyncTcpSession session = sender as AsyncTcpSession;

            Console.WriteLine("tcpSession_Connected");

            var buffer = new CGD.buffer(50);

            buffer.append<int>(123);
            buffer.append<short>(0);
            buffer.append<string>("전송한 데이터");
            buffer.set_front<short>(buffer.Count, 4);

            session.Send(buffer);
        }
    }
}
