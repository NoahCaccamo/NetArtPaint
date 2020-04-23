using Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace server
{
    public class TCPServer
    {
        private static readonly object _lock = new object();
        private static readonly List<TcpClient> clients = new List<TcpClient>();


        public static System.Timers.Timer sTimer;
        public static Stopwatch stopWatch = new Stopwatch();
        public static int totalTimerTime = 60000;

        public static List<Packet> pics = new List<Packet>();
        public static string[] biddingHistory = new string[8];
        public static string[] chatHistory = new string[20];


        public static int HighestBid;
        public static string HighestBidder;
        public static string LastHighestBidder;
        public static int AuctionPos = -1;
        public static Packet currentPic;

        public static TcpClient[] GetClients()
        {
            lock (_lock) return clients.ToArray();
        }

        public static int GetClientCount()
        {
            lock (_lock) return clients.Count;
        }

        public static void RemoveClient(TcpClient client)
        {
            lock (_lock) clients.Remove(client);
        }


        private static void SetTimer()
        {
            sTimer = new System.Timers.Timer();

            sTimer.Interval = 70000;
            sTimer.Elapsed += OnTimedEvent;
            sTimer.AutoReset = false;
            sTimer.Enabled = false;
        }

        private static void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            stopWatch.Stop();
            sTimer.Stop();
            if (AuctionPos < TCPServer.pics.Count - 1)
            {
                Console.WriteLine("BAPPATY BOOP");
                AuctionPos++;
                HighestBid = 0;
                sTimer.Start();
                stopWatch.Restart();
            }
            Console.WriteLine("The Elapsed event was raised at {0:HH:mm:ss.fff}",
                          e.SignalTime);
        }

        static void Main(string[] args)
        {
            //IPAddress ip = IPAddress.Parse("127.0.0.1");
            TcpListener ServerSocket = new TcpListener(IPAddress.Any, 55555);
            ServerSocket.Start();

            SetTimer();

            Console.WriteLine("Server started.");
            while (true)
            {
                TcpClient clientSocket = ServerSocket.AcceptTcpClient();
                Console.WriteLine($"client connected: {clientSocket.Client.RemoteEndPoint}");
                lock (_lock) clients.Add(clientSocket);
                HandleClient client = new HandleClient();
                client.startClient(clientSocket);

                Console.WriteLine($"{GetClientCount()} clients connected");
            }
        }


    }
}
