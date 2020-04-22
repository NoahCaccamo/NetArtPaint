using Common;
using Newtonsoft.Json;
using System;
using System.Drawing;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Common
{
    public class Client
    {
        private UdpClient client;
        private IPEndPoint ep;

        /// <param name="ip"> The ip of the server to connect to </param>
        /// <param name="port"> Port the server is running on </param>
        public Client(string ip = "127.0.0.1", int port = 55555)
        {
            client = new UdpClient();
            // endpoint where server is listening
            ep = new IPEndPoint(IPAddress.Parse(ip), port);
            client.Connect(ep);
        }

        public void send(Packet packet)
        {
            // send data

            var ms = new MemoryStream();
            //Bitmap bmp = new Bitmap(path, true);
           // packet.Painting.Save(ms, ImageFormat.Jpeg);

            string json = JsonConvert.SerializeObject(packet);
            byte[] msgBytes = Encoding.ASCII.GetBytes(json);
           // byte[] byteArray = ms.ToArray();
            client.Send(msgBytes, msgBytes.Length);

        }

        public Packet Recieve()
        {
            // then receive data
            var receivedData = client.Receive(ref ep);
            string p = Encoding.ASCII.GetString(receivedData);
            Packet deserializedPacket = JsonConvert.DeserializeObject<Packet>(p);

            return deserializedPacket;
            //int receivedInt = Int32.Parse(Encoding.ASCII.GetString(receivedData));


            //Console.WriteLine("receive data from " + ep.ToString());
            //Console.WriteLine("Received: " + Encoding.ASCII.GetString(receivedData));

        }
        public void Bid(int UserInput)
        {
        //    UserInput = 
        }

      //  static void Main(string[] args)
      //  {
          //  var client = new Client("25.133.204.102");

           // string msg = null;

           // while (msg != "exit")
           // {
              //  Console.Write("Enter image path: ");
              //  msg = Console.ReadLine();

              //  client.send(msg);
          //  }
      //  }
    }
}
