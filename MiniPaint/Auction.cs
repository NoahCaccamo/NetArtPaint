using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Common;

namespace MiniPaint
{
    public partial class Auction : Form
    {

        Client client = new Client(Globals.playerInfo.ip);
         private static System.Timers.Timer cTimer;
        static int auctionTimer;

        public Auction()
        {
            InitializeComponent();
            Globals.playerInfo.money = 1000;
            startTimer();
        }

        private void PlaceBid_Click(object sender, EventArgs e)
        {
            int userBid = Int32.Parse(BidEntry.Text);

            if (Globals.playerInfo.money >= userBid)
            {
                Packet packetToSend = new Packet();
                packetToSend.Username = Globals.playerInfo.username;
                packetToSend.Type = (int)Packet.pType.Bid;
                packetToSend.bid = userBid;

                unpack(client.send(packetToSend));
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

        void unpack(Packet packet)
        {
            switch(packet.Type)
            {
                case ((int)PlayerInfo.recievedType.bidT):
                    //Notifications.Text = "BID ACCEPTED";
                    SetText("BID ACCEPTED");
                    break;

                case ((int)PlayerInfo.recievedType.bidF):
                    //Notifications.Text = "BID not accepted";
                    SetText("BID NOT ACCEPTED");
                    break;

                case ((int)PlayerInfo.recievedType.winBid):
                    
                    break;

                case ((int)PlayerInfo.recievedType.loseBid):

                    break;

                case ((int)PlayerInfo.recievedType.time):
                    auctionTimer = packet.time;
                    Debug.WriteLine(auctionTimer);
                    TimeSpan t = TimeSpan.FromMilliseconds(auctionTimer);
                    //TimerLabel.Text = min + ":" + secs;
                    SetText("Minutes left: " + t.Minutes + " Seconds left: " + t.Seconds);
                    break;
            }
        }

        void startTimer()
        {
            cTimer = new System.Timers.Timer();
            cTimer.Interval = 1000;
            cTimer.Elapsed += FetchServerTime;
            cTimer.AutoReset = true;
            cTimer.Enabled = true;
        }

        private void FetchServerTime(Object source, System.Timers.ElapsedEventArgs e)
        {
            Packet packToSend = new Packet();
            packToSend.Type = (int)Packet.pType.RequestTime;
            packToSend.Username = Globals.playerInfo.username;

            unpack(client.send(packToSend));

        }


        delegate void SetTextCallback(string text);

        private void SetText(string text)
        {
            if (this.Notifications.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetText);
                this.Invoke(d, new object[] { text });
            } else
            {
                this.Notifications.Text = text;
            }
        }

        private void BidEntry_ValueChanged(object sender, EventArgs e)
        {
            
        }

        private void Auction_Load(object sender, EventArgs e)
        {

        }
    }
}
