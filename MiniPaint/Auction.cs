﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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

        Client client = new Client();

        public Auction()
        {
            InitializeComponent();
            Globals.playerInfo.money = 1000;
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
                    Notifications.Text = "BID ACCEPTED";
                    break;

                case ((int)PlayerInfo.recievedType.bidF):
                    Notifications.Text = "BID not accepted";
                    break;

                case ((int)PlayerInfo.recievedType.winBid):

                    break;

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
