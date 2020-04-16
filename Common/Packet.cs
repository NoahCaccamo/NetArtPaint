using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class Packet
    {
        public enum pType { SubmitPainting, Bid, SyncTime, EndBid, RecieveMoney, PayForPainting }
        public int Type;
        public string Username;
        public byte[] Painting;
        public string Title;
        public string Description;
        public int bid;
        public int cost;
    }
}
