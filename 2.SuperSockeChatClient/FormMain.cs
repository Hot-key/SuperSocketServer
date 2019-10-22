using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SuperSocket.ClientEngine;

namespace _2.SuperSockeChatClient
{
    public partial class FormMain : Form
    {
        private static AsyncTcpSession tcpSession;

        public FormMain()
        {
            InitializeComponent();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            tcpSession = new AsyncTcpSession();
            tcpSession.Connected += tcpSession_Connected;
            tcpSession.Closed += tcpSession_Closed;
            tcpSession.DataReceived += tcpSession_DataReceived;
            tcpSession.Error += tcpSession_Error;
        }

        private void tcpSession_Connected(object sender, EventArgs e)
        {

        }

        private void tcpSession_Closed(object sender, EventArgs e)
        {

        }

        private void tcpSession_Error(object sender, ErrorEventArgs e)
        {

        }

        private void tcpSession_DataReceived(object sender, DataEventArgs e)
        {

        }

        private void buttonConnectServer_Click(object sender, EventArgs e)
        {
            tcpSession.Connect(new IPEndPoint(IPAddress.Parse(textBoxIpAddress.Text), Convert.ToInt32(textBoxPort.Text)));
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {

        }

        private void buttonRegister_Click(object sender, EventArgs e)
        {

        }

        private void timerRefreshRoom_Tick(object sender, EventArgs e)
        {

        }
    }
}
