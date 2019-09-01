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

namespace _1.SuperSocketEcho
{
    class Program
    {
        static void Main(string[] args)
        {
            MyAppServer server = new MyAppServer();

            server.Setup(new RootConfig(),new ServerConfig()
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

    public class MyReceiveFilter : FixedHeaderReceiveFilter<BinaryRequestInfo>
    {
        public MyReceiveFilter()
            : base(6)
        {

        }

        protected override int GetBodyLengthFromHeader(byte[] header, int offset, int length)
        {
            return (int)header[offset + 4] + (int)header[offset + 5] * 256 - 6;
        }

        protected override BinaryRequestInfo ResolveRequestInfo(ArraySegment<byte> header, byte[] bodyBuffer, int offset, int length)
        {
            var byteTmp = bodyBuffer.CloneRange(offset, length);

            return new BinaryRequestInfo(BitConverter.ToUInt32(header.Array, 0).ToString(), MyReceiveFilter.Combine(header.Array, byteTmp));
        }

        public static byte[] Combine(byte[] first, byte[] second)
        {
            byte[] ret = new byte[first.Length + second.Length];
            Buffer.BlockCopy(first, 0, ret, 0, first.Length);
            Buffer.BlockCopy(second, 0, ret, first.Length, second.Length);
            return ret;
        }
    }

    public class MyAppServer : AppServer<MyAppSession, BinaryRequestInfo>
    {
        public MyAppServer() : base(new DefaultReceiveFilterFactory<MyReceiveFilter, BinaryRequestInfo>())
        {
            this.NewSessionConnected += new SessionHandler<MyAppSession>(MyServer_NewSessionConnected);
            this.SessionClosed += new SessionHandler<MyAppSession, CloseReason>(MyServer_SessionClosed);
            this.NewRequestReceived += new RequestHandler<MyAppSession, BinaryRequestInfo>(MyServer_NewRequestReceived);
        }

        private void MyServer_NewRequestReceived(MyAppSession session, BinaryRequestInfo requestinfo)
        {
            Console.WriteLine("MyServer_NewRequestReceived");

            session.Send(requestinfo.Body,0, requestinfo.Body.Length);
        }

        private void MyServer_SessionClosed(MyAppSession session, CloseReason value)
        {
            Console.WriteLine("MyServer_UserClosed");
        }

        private void MyServer_NewSessionConnected(MyAppSession session)
        {
            Console.WriteLine("MyServer_NewUserConnected");
        }
    }

    public class MyAppSession : AppSession<MyAppSession, BinaryRequestInfo>
    {

    }
}
