using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Windows;

namespace Client
{
    public partial class Form1 : Form
    {
        public static int port = 3245;
        public static TcpListener listener = new TcpListener(IPAddress.Any, port);
        public static Thread conn = new Thread(awaitConnection);
        public static TcpClient client;
        public static NetworkStream dataStream;

        
        static void sendMessage(string msg)
        {
            char[] data = msg.ToCharArray();
            byte[] buffer = new byte[data.Length];
            for (int i = 0; i < data.Length; i++)
            {
                buffer[i] = (byte)data[i];
            }
            dataStream.Write(buffer, 0, buffer.Length);
            dataStream.Flush();
        }
        static void awaitConnection()
        {
            listener.Start();
            client = listener.AcceptTcpClient();
            dataStream = client.GetStream();


        }




        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            conn.Start();
        }

        private void exit(object senter, FormClosedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void MessageBoxToolStripMenuItem_Click(object sender, EventArgs e)
        {
            sendMessage("hello world");
        }
    }
}
