using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MiniPaint
{
    public partial class Auction : Form
    {

        public PlayerInfo playerInfo { get; set; } = new PlayerInfo();
        public Auction()
        {
            InitializeComponent();
            playerInfo.money = 1000;
        }

        private void PlaceBid_Click(object sender, EventArgs e)
        {

            if (playerInfo.money >= Int32.Parse(BidEntry.Text))
            {
                string userIn = BidEntry.Text;
            }

            else
            {
                //error message
            }

        }

        private void BuyCanvas_Click(object sender, EventArgs e)
        {
            if (playerInfo.money >= 500)
            {
                playerInfo.money -= 500;
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
    }
}
