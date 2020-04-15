﻿using Common;
using Newtonsoft.Json;
using System;
using System.Drawing;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace server
{
    public class Server
    {
        private UdpClient udpServer;
        private int port;
        private IPEndPoint remoteEP;

        public Server(int port = 55555)
        {
            udpServer = new UdpClient(port);
            this.port = port;
            remoteEP = new IPEndPoint(IPAddress.Any, port);
        }

        public string receive()
        {
            var data = udpServer.Receive(ref remoteEP);
            ImageConverter convertData = new ImageConverter();
            Image image = (Image)convertData.ConvertFrom(data);
            image.Save("C:\\Users\\ncaccamo\\Music\\myBitmap.bmp");
            //string msg = Encoding.ASCII.GetString(data);
            //Console.WriteLine("Received: " + msg);
            return "Done";
        }

        public string jsRecieve()
        {
            var data = udpServer.Receive(ref remoteEP);
            string p = Encoding.ASCII.GetString(data);
            Packet deserializedPacket = JsonConvert.DeserializeObject<Packet>(p);


            ImageConverter convertData = new ImageConverter();
            Image image = (Image)convertData.ConvertFrom(deserializedPacket.Painting);
            image.Save("C:\\Users\\ncaccamo\\Music\\myBitmap.bmp");

            return "Done";
        }

        public void send(string msg)
        {
            byte[] msgBytes = Encoding.ASCII.GetBytes(msg);
            udpServer.Send(msgBytes, msgBytes.Length, remoteEP);
        }

        static void Main(string[] args)
        {
            var server = new Server();

            string msg = null;

            while (msg != "exit")
            {
                msg = server.jsRecieve();
                server.send("Echo msg: " + msg);
            }
        }
    }
}
