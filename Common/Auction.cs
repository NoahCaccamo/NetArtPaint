using Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Common
{
    public partial class Auction : Form
    {

        public static Auction mainScreen = null;



        public Auction()
        {
            InitializeComponent();
            Globals.playerInfo.money = 1000;
            mainScreen = this;
        }

        private void PlaceBid_Click(object sender, EventArgs e)
        {
            int bidAmmount = Int32.Parse(BidEntry.Text);
            if (Globals.playerInfo.money >= bidAmmount)
            {
                Packet bidPacket = new Packet();
                bidPacket.Type = (int)Packet.pType.Bid;
                bidPacket.Username = Globals.playerInfo.username;
                bidPacket.bid = bidAmmount;


                Globals.client.send(bidPacket);
            }

            else
            {
                //error message
            }

        }

        private void BuyCanvas_Click(object sender, EventArgs e)
        {
            if (Globals.playerInfo.money >= 500)
            {
                Globals.playerInfo.money -= 500;
                var frm = new Draw();
                frm.Location = this.Location;
                frm.StartPosition = FormStartPosition.Manual;
                frm.FormClosing += delegate { this.Show(); };
                frm.Show();
            }
    }

        private void BidEntry_ValueChanged(object sender, EventArgs e)
        {
            
        }

        public void test()
        {

        }
    }
}
