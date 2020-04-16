using Common;
using Newtonsoft.Json;
using System;
using System.Drawing;
using System.Net;
using System.Net.Sockets;
using System.Text;
using static Common.Packet;

namespace server
{
    public class Server
    {
        private UdpClient udpServer;
        private int port;
        private IPEndPoint remoteEP;
        public int HighestBid;
        public string HighestBidder;

        public Server(int port = 55555)
        {
            udpServer = new UdpClient(port);
            this.port = port;
            remoteEP = new IPEndPoint(IPAddress.Any, port);
        }

        //public string receive()
        //{
        //    var data = udpServer.Receive(ref remoteEP);
        //    ImageConverter convertData = new ImageConverter();
        //    Image image = (Image)convertData.ConvertFrom(data);
        //    image.Save("C:\\Users\\ncaccamo\\Music\\myBitmap.bmp");
        //    //string msg = Encoding.ASCII.GetString(data);
        //    //Console.WriteLine("Received: " + msg);
        //    return "Done";
        //}

        public Packet jsRecieve()
        {

            var data = udpServer.Receive(ref remoteEP);
            string p = Encoding.ASCII.GetString(data);
            Packet deserializedPacket = JsonConvert.DeserializeObject<Packet>(p);

            Packet packToSend = new Packet();
            packToSend.Username = deserializedPacket.Username;
            switch (deserializedPacket.Type)
            {
                case (int)pType.SubmitPainting:
                    ImageConverter convertData = new ImageConverter();
                    Image image = (Image)convertData.ConvertFrom(deserializedPacket.Painting);
                    image.Save("C:\\Users\\ncaccamo\\Music\\myBitmap.bmp");
                    break;

                case (int)pType.Bid:
                    packToSend.Type = HigherBid(deserializedPacket.bid, deserializedPacket.Username);

                    break;

                case (int)pType.EndBid:
                    if (deserializedPacket.Username == HighestBidder)
                    {
                        packToSend.Type = (int)PlayerInfo.recievedType.winBid;
                        packToSend.cost = HighestBid;
                        //packToSend.Painting = GIVE PAINTING
                    }
                    else
                    {
                        ///MAKE IT LOSE
                    }
                    break;
                    return default;
            }


            return packToSend;
        }

        public void send(Packet msg)
        {
            //  byte[] msgBytes = Encoding.ASCII.GetBytes(msg);
            string json = JsonConvert.SerializeObject(msg);
            byte[] msgBytes = Encoding.ASCII.GetBytes(json);
            udpServer.Send(msgBytes, msgBytes.Length, remoteEP);
        }

        static void Main(string[] args)
        {
            var server = new Server();

            Packet msg = null;

            while (true)
            {
                msg = server.jsRecieve();
                server.send(msg);
            }
        }


        public int HigherBid(int UserBid, string UserName)
        {
            if (UserBid > HighestBid)
            {
                HighestBid = UserBid;
                HighestBidder = UserName;
                return (int)PlayerInfo.recievedType.bidT;
            }
            else
            {
                
                return (int)PlayerInfo.recievedType.bidF;
            }
        }

    }
}
