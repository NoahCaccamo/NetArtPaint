using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class Packet
    {
        public enum pType { SubmitPainting, Bid }
        public int Type;
        public byte[] Painting;
        public string Title;
        public string Description;
        public int bid;
    }
}
