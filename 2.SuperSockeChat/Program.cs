using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuperSocket.Common;
using SuperSocket.Facility.Protocol;
using SuperSocket.SocketBase;
using SuperSocket.SocketBase.Config;
using SuperSocket.SocketBase.Protocol;

namespace _2.SuperSockeChat
{
    class Program
    {
        static void Main(string[] args)
        {
            MyAppServer server = new MyAppServer();

            server.Setup(new RootConfig(), new ServerConfig()
            {
                Port = 3000,
                Ip = "Any",
            });

            server.Start();

            while (Console.ReadLine() != "q")
            {

            }
        }
    }


    public class MyReceiveFilter : FixedHeaderReceiveFilter<MyRequestInfo>
    {
        public MyReceiveFilter() : base(6)
        {

        }

        protected override int GetBodyLengthFromHeader(byte[] header, int offset, int length)
        {
            return (int)header[offset] +
                   (int)header[offset + 1] * 256 +
                   (int)header[offset + 2] * 65535 +
                   (int)header[offset + 3] * 16777216 - 6; 
        }

        protected override MyRequestInfo ResolveRequestInfo(ArraySegment<byte> header, byte[] bodyBuffer, int offset, int length)
        {
            var byteTmp = bodyBuffer.CloneRange(offset, length);

            return new MyRequestInfo(MyReceiveFilter.Combine(header.Array, byteTmp), length);
        }

        public static byte[] Combine(byte[] first, byte[] second)
        {
            byte[] ret = new byte[first.Length + second.Length];
            Buffer.BlockCopy(first, 0, ret, 0, first.Length);
            Buffer.BlockCopy(second, 0, ret, first.Length, second.Length);
            return ret;
        }
    }

    public class MyAppServer : AppServer<MyAppSession, MyRequestInfo>
    {
        public MyAppServer() : base(new DefaultReceiveFilterFactory<MyReceiveFilter, MyRequestInfo>())
        {
            this.NewSessionConnected += MyServer_NewSessionConnected;
            this.SessionClosed += MyServer_SessionClosed;
            this.NewRequestReceived += MyServer_NewRequestReceived;
        }

        private void MyServer_NewRequestReceived(MyAppSession session, MyRequestInfo requestInfo)
        {
        }

        private void MyServer_SessionClosed(MyAppSession session, CloseReason value)
        {
        }

        private void MyServer_NewSessionConnected(MyAppSession session)
        {
        }
    }

    public class MyAppSession : AppSession<MyAppSession, MyRequestInfo>
    {

    }

    public class MyRequestInfo : RequestInfo<MyRequestInfo>
    {
        public new dynamic Key { get; private set; }

        public new CGD.NcsBuffer Body { get; private set; }

        public MyRequestInfo(byte[] body, int length)
        {
            Body = new CGD.NcsBuffer(body, 0, body.Length);
            Key = Body.get_front_ushort(2);
        }
    }
}
