using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Common;
using System.Threading;

namespace MiniPaint
{
    public partial class Auction : Form
    {

        Client client = new Client(Globals.playerInfo.ip);
         private static System.Timers.Timer cTimer;
        static int auctionTimer = 999;
        bool gotPainting = false;

        public Auction()
        {
            InitializeComponent();
            Globals.playerInfo.money = 5000;
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
                SetText(MoneyLabel, "Money: " + Globals.playerInfo.money);
            }
    }

        void unpack(Packet packet)
        {
            switch(packet.Type)
            {
                case ((int)PlayerInfo.recievedType.bidT):
                    //Notifications.Text = "BID ACCEPTED";
                    SetText(Notifications, "BID ACCEPTED");
                    break;

                case ((int)PlayerInfo.recievedType.bidF):
                    //Notifications.Text = "BID not accepted";
                    SetText(Notifications, "BID NOT ACCEPTED");
                    break;

                case ((int)PlayerInfo.recievedType.winBid):
                    gotPainting = true;
                    Globals.playerInfo.money -= packet.cost;
                    SetText(MoneyLabel, "Money: " + Globals.playerInfo.money);

                    ImageConverter convertData = new ImageConverter();
                    Image image = (Image)convertData.ConvertFrom(packet.Painting);

                    Thread th = new Thread((ThreadStart)(() => {
                        SaveFileDialog dialog = new SaveFileDialog();
                    dialog.Filter = "JPeg Image|*.jpg|Bitmap Image|*.bmp|Gif Image|*.gif";
                    dialog.Title = "Save an Image File";
                    dialog.ShowDialog();

                    // If the file name is not an empty string open it for saving.
                    if (dialog.FileName != "")
                    {
                        // Saves the Image via a FileStream created by the OpenFile method.
                        System.IO.FileStream fs =
                            (System.IO.FileStream)dialog.OpenFile();
                        // Saves the Image in the appropriate ImageFormat based upon the
                        // File type selected in the dialog box.
                        // NOTE that the FilterIndex property is one-based.
                        switch (dialog.FilterIndex)
                        {
                            case 1:
                                image.Save(fs,
                                  System.Drawing.Imaging.ImageFormat.Jpeg);
                                break;

                            case 2:
                                image.Save(fs,
                                  System.Drawing.Imaging.ImageFormat.Bmp);
                                break;

                            case 3:
                                image.Save(fs,
                                  System.Drawing.Imaging.ImageFormat.Gif);
                                break;
                        }

                        fs.Close();
                    }
            }));

                    th.SetApartmentState(ApartmentState.STA);
                    th.Start();
                    th.Join();

                    // image.Save("C:\\Users\\ncaccamo\\Music\\WINNER" + packet.bid +".bmp");
                    ///SAVE THE PAINTING
                    break;

                case ((int)PlayerInfo.recievedType.loseBid):
                    SetText(Notifications, packet.Username + "Won the bid");
                    break;

                case ((int)PlayerInfo.recievedType.time):
                    auctionTimer = packet.time;
                    Debug.WriteLine(auctionTimer);
                    TimeSpan t = TimeSpan.FromMilliseconds(auctionTimer);
                    //TimerLabel.Text = min + ":" + secs;
                    SetText(TimerLabel, "Minutes left: " + t.Minutes + " Seconds left: " + t.Seconds);
                    SetText(TitleLabel, packet.Title);
                    SetText(DescriptionLabel, packet.Description);
                    SetText(CurrentHighLabel, packet.Username + " in the lead with a bid of " + packet.bid);
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
            if (auctionTimer > 0 || gotPainting)
            {
                gotPainting = false;
                packToSend.Type = (int)Packet.pType.RequestTime;
            }else
            {
                packToSend.Type = (int)Packet.pType.EndBid;
            }
            packToSend.Username = Globals.playerInfo.username;

            unpack(client.send(packToSend));

        }


        delegate void SetTextCallback(Label label, string text);

        private void SetText(Label label, string text)
        {
            if (label.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetText);
                this.Invoke(d, new object[] { label, text });
            } else
            {
                label.Text = text;
            }
        }

        private void BidEntry_ValueChanged(object sender, EventArgs e)
        {
            
        }

        private void Auction_Load(object sender, EventArgs e)
        {

        }

        private void Notifications_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void MoneyLabel_Click(object sender, EventArgs e)
        {

        }
    }
}
