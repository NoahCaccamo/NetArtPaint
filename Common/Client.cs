using Common;
using Newtonsoft.Json;
using System;
using System.Drawing;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Common
{
    public class Client
    {
        private TcpClient client;
        private IPEndPoint ep;

        private StreamReader _sReader;
        private StreamWriter _sWriter;

        /// <param name="ip"> The ip of the server to connect to </param>
        /// <param name="port"> Port the server is running on </param>
        public Client(string ip = "127.0.0.1", int port = 55555)
        {
            client = new TcpClient(ip, port);
        }

        public void Send(Packet packet)
        {
            //BinaryWriter writer = new BinaryWriter(client.GetStream(), Encoding.ASCII);
            _sWriter = new StreamWriter(client.GetStream(), Encoding.ASCII);
            string json = JsonConvert.SerializeObject(packet);

           //writer.Write(json);
            //writer.Flush();
            
            _sWriter.WriteLine(json);
            _sWriter.Flush();
            
        }

        public Packet Recieve()
        {
           // BinaryReader reader = new BinaryReader(client.GetStream(), Encoding.ASCII);

            
            _sReader = new StreamReader(client.GetStream(), Encoding.ASCII);

            var p = _sReader.ReadToEnd();
            Packet deserializedPacket = JsonConvert.DeserializeObject<Packet>(p);
            return deserializedPacket;
        }
    }
}
