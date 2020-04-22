using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class Packet
    {
        public enum pType { SubmitPainting, Bid, EndBid, RecieveMoney, PayForPainting, RequestTime, RequestChat, SendChat }
        public int Type;
        public string Username;
        public byte[] Painting;
        public string Title;
        public string Description;
        public int bid;
        public int cost;
        public int time;
        public string Artist;
        public string Message;

        public string[] biddingHistory = new string[8];
        public string[] chatHistory = new string[8];
    }
}
