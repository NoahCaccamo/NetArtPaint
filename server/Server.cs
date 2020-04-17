﻿using Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Timers;
using static Common.Packet;
using System.Linq;

namespace server
{
    public class Server
    {
        static List<Packet> pics = new List<Packet>();


        private UdpClient udpServer;
        private int port;
        private IPEndPoint remoteEP;
        public int HighestBid;
        public string HighestBidder;
        static int AuctionPos = -1;
        Packet currentPic;

        private static Timer sTimer;
        private static Stopwatch stopWatch = new Stopwatch();
        public int totalTimerTime = 10000;

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
                    pics.Add(deserializedPacket);
                    Console.WriteLine("PICSIZE   " + pics.Count);
                    break;

                case (int)pType.Bid:
                    packToSend.Type = HigherBid(deserializedPacket.bid, deserializedPacket.Username);

                    break;

                case (int)pType.EndBid:
                    if (deserializedPacket.Username == HighestBidder)
                    {
                        currentPic = pics.ElementAt(AuctionPos);

                        packToSend.Type = (int)PlayerInfo.recievedType.winBid;
                        packToSend.cost = HighestBid;
                        packToSend.Painting = currentPic.Painting;
                        packToSend.Title = currentPic.Title;
                        packToSend.Description = currentPic.Description;
                        packToSend.Username = currentPic.Username;
                        //packToSend.Painting = GIVE PAINTING
                    }
                    else
                    {
                        packToSend.Type = (int)PlayerInfo.recievedType.loseBid;
                        packToSend.Username = HighestBidder;
                        ///MAKE IT LOSE
                    }
                    break;

                case (int)pType.RequestTime:
                    packToSend.Type = (int)PlayerInfo.recievedType.time;
                    packToSend.time = totalTimerTime - (int)stopWatch.ElapsedMilliseconds;

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

            SetTimer();
            stopWatch.Start();
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

        private static void SetTimer()
        {
            sTimer = new System.Timers.Timer();

            sTimer.Interval = 20000;
            sTimer.Elapsed += OnTimedEvent;
            sTimer.AutoReset = true;
            sTimer.Enabled = true;
        }

        private static void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            if (AuctionPos < pics.Count)
            {
                Console.WriteLine("BAPPATY BOOP");
                AuctionPos++;
            }
            stopWatch.Restart();
            Console.WriteLine("The Elapsed event was raised at {0:HH:mm:ss.fff}",
                          e.SignalTime);
        }



    }


}
