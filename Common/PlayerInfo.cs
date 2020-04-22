using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class PlayerInfo
    {
        public enum recievedType { bidT, bidF, winBid, loseBid, time, upChat}
        public string username;
        public int money = 1000;
        public string ip;
    }

}
