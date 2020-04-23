using Common;
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
using System.Threading.Tasks;
using System.Threading;
using System.IO;

namespace server
{
    public class HandleClient
    {

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

        TcpClient clientSocket;
        private Thread ctThread;
        public void startClient(TcpClient inClientSocket)
        {
            this.clientSocket = inClientSocket;
            ctThread = new Thread(processClient);
            ctThread.Start();
        }


        private void processClient()
        {
            while (true)
            {
                try
                {
                    var msg = this.jsRecieve();
                    this.send(msg);
                } catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    clientSocket.Close();
                    TCPServer.RemoveClient(clientSocket);
                    ctThread.Abort();
                }
            }
        }

        public Packet jsRecieve()
        {
            //BinaryReader reader = new BinaryReader(clientSocket.GetStream());
            StreamReader sReader = new StreamReader(clientSocket.GetStream(), Encoding.ASCII);
            var p = sReader.ReadLine();
            Packet deserializedPacket = JsonConvert.DeserializeObject<Packet>(p);

            Packet packToSend = new Packet();
            packToSend.Username = deserializedPacket.Username;
            switch (deserializedPacket.Type)
            {
                case (int)pType.SubmitPainting:
                    ImageConverter convertData = new ImageConverter();
                    Image image = (Image)convertData.ConvertFrom(deserializedPacket.Painting);
                    image.Save("C:\\Users\\ncaccamo\\Music\\myBitmap.bmp");
                    TCPServer.pics.Add(deserializedPacket);
                    if (TCPServer.sTimer.Enabled == false)
                    {
                        TCPServer.sTimer.Start();
                        TCPServer.stopWatch.Restart();
                        TCPServer.HighestBid = 0;
                        TCPServer.AuctionPos++;
                        TCPServer.currentPic = TCPServer.pics.ElementAt(TCPServer.AuctionPos);
                    }
                    Console.WriteLine("PICSIZE   " + TCPServer.pics.Count);
                    break;

                case (int)pType.Bid:
                    if (TCPServer.totalTimerTime - (int)TCPServer.stopWatch.ElapsedMilliseconds > 0)
                    {
                        packToSend.Type = HigherBid(deserializedPacket.bid, deserializedPacket.Username);
                    }
                    break;

                case (int)pType.EndBid:
                    packToSend.biddingHistory = TCPServer.biddingHistory;
                    if (deserializedPacket.Username == TCPServer.HighestBidder)
                    {
                        if (TCPServer.pics.Count >= 1)
                        {
                            TCPServer.currentPic = TCPServer.pics.ElementAt(TCPServer.AuctionPos);

                            packToSend.Type = (int)PlayerInfo.recievedType.winBid;
                            packToSend.cost = TCPServer.HighestBid;
                            packToSend.Painting = TCPServer.currentPic.Painting;
                            packToSend.Title = TCPServer.currentPic.Title;
                            packToSend.Description = TCPServer.currentPic.Description;
                            packToSend.Username = TCPServer.currentPic.Username;
                            //packToSend.Painting = GIVE PAINTING
                            TCPServer.HighestBidder = "";
                            TCPServer.LastHighestBidder = "";

                            TCPServer.biddingHistory = newBidEntry(deserializedPacket.Username + " won the painting " + TCPServer.currentPic.Title + " with a bid of " + TCPServer.HighestBid + "!");
                        }
                        packToSend.time = TCPServer.totalTimerTime - (int)TCPServer.stopWatch.ElapsedMilliseconds;
                    }
                    else
                    {
                        packToSend.Type = (int)PlayerInfo.recievedType.loseBid;
                        packToSend.Username = TCPServer.LastHighestBidder;
                        packToSend.time = TCPServer.totalTimerTime - (int)TCPServer.stopWatch.ElapsedMilliseconds;
                        packToSend.Artist = TCPServer.currentPic.Username;
                        packToSend.bid = TCPServer.HighestBid;
                        ///MAKE IT LOSE
                    }
                    break;

                case (int)pType.RequestTime:
                    packToSend.Type = (int)PlayerInfo.recievedType.time;
                    packToSend.time = TCPServer.totalTimerTime - (int)TCPServer.stopWatch.ElapsedMilliseconds;
                    packToSend.chatHistory = TCPServer.chatHistory;
                    if (TCPServer.sTimer.Enabled == true && TCPServer.pics.Count >= 1)
                    {
                        TCPServer.currentPic = TCPServer.pics.ElementAt(TCPServer.AuctionPos);
                        packToSend.Title = TCPServer.currentPic.Title;
                        packToSend.Description = TCPServer.currentPic.Description;
                        packToSend.Username = TCPServer.LastHighestBidder;
                        packToSend.bid = TCPServer.HighestBid;
                        packToSend.Artist = TCPServer.currentPic.Username;
                        packToSend.biddingHistory = TCPServer.biddingHistory;
                    }

                    break;

                case (int)pType.SendChat:
                    packToSend.Type = (int)PlayerInfo.recievedType.upChat;
                    TCPServer.chatHistory = newChatEntry(deserializedPacket.Username + ": " + deserializedPacket.Message);
                    packToSend.chatHistory = TCPServer.chatHistory;
                    break;

                case (int)pType.RequestChat:
                    packToSend.Type = (int)PlayerInfo.recievedType.upChat;
                    packToSend.chatHistory = TCPServer.chatHistory;
                    break;

                    return default;
            }

            return packToSend;
        }

        public void send(Packet msg)
        {
            //  byte[] msgBytes = Encoding.ASCII.GetBytes(msg);

            //BinaryWriter writer = new BinaryWriter(clientSocket.GetStream());
            StreamWriter sWriter = new StreamWriter(clientSocket.GetStream(), Encoding.ASCII);


            string json = JsonConvert.SerializeObject(msg);

            sWriter.WriteLine(json);
            sWriter.Flush();
        }

        public int HigherBid(int UserBid, string UserName)
        {
            if (UserBid > TCPServer.HighestBid)
            {
                TCPServer.HighestBid = UserBid;
                TCPServer.HighestBidder = UserName;
                TCPServer.LastHighestBidder = TCPServer.HighestBidder;
                TCPServer.biddingHistory = newBidEntry(UserName + " in the lead with a bid of " + UserBid);

                return (int)PlayerInfo.recievedType.bidT;
            }
            else
            {
                
                return (int)PlayerInfo.recievedType.bidF;
            }
        }

        

        string[] newBidEntry(string bidText)
        {
            string[] tempArray = new string[TCPServer.biddingHistory.Length];
            Array.Copy(TCPServer.biddingHistory, 0, tempArray, 1, TCPServer.biddingHistory.Length - 1);
            tempArray[0] = bidText;
            return tempArray;
        }

        string[] newChatEntry(string chatText)
        {
            string[] tempArray = new string[TCPServer.chatHistory.Length];
            Array.Copy(TCPServer.chatHistory, 0, tempArray, 1, TCPServer.chatHistory.Length - 1);
            tempArray[0] = chatText;
            return tempArray;
        }

    }
}
