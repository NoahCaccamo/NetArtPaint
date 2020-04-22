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
using System.Drawing.Text;

namespace MiniPaint
{
    public partial class Auction : Form
    {
        //PrivateFontCollection pfc = new PrivateFontCollection();
        
        //Font gameFont = new Font(fontFamily, 16, FontStyle.Regular, GraphicsUnit.Pixel);

        List<Packet> invPics = new List<Packet>();


        Client client = new Client(Globals.playerInfo.ip);
        private static System.Timers.Timer cTimer;
        private static System.Timers.Timer chatTimer;
        static int auctionTimer = 999;
        bool gotPainting = false;
        bool gotPayout = false;

        InvDisplay displayWindow = null;

        public Auction()
        {
            InitializeComponent();
            Globals.playerInfo.money = 5000;
            startTimer();
            //startChatTimer();
          //  pfc.AddFontFile(System.Windows.Forms.Application.StartupPath + "\\Resources\\" + "GameFont.ttf");
           // Console.WriteLine(System.Windows.Forms.Application.StartupPath);
        }

        private void PlaceBid_Click(object sender, EventArgs e)
        {
            int userBid = Int32.Parse(BidEntry.Text);

            if (Globals.playerInfo.money >= userBid && auctionTimer > 0)
            {
                Packet packetToSend = new Packet();
                packetToSend.Username = Globals.playerInfo.username;
                packetToSend.Type = (int)Packet.pType.Bid;
                packetToSend.bid = userBid;

                client.send(packetToSend);
                unpack(client.Recieve());

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
                var frm = new Draw(false);
                frm.Location = this.Location;
                frm.StartPosition = FormStartPosition.Manual;
                frm.FormClosing += delegate { this.Show(); };
                frm.Show();
                SetText(MoneyLabel, "Money: " + Globals.playerInfo.money);
            }
        }

        void unpack(Packet packet)
        {
            switch (packet.Type)
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
                    UpdateTimer(packet);
                    Array.Reverse(packet.biddingHistory);
                    SetTextHistory(BiddingHistoryRichText, packet.biddingHistory);
                    if (!gotPainting)
                    {
                        gotPainting = true;
                        Globals.playerInfo.money -= packet.cost;
                        SetText(MoneyLabel, "Money: " + Globals.playerInfo.money);

                        ImageConverter convertData = new ImageConverter();
                        Image image = (Image)convertData.ConvertFrom(packet.Painting);

                        invPics.Add(packet);
                        UpdateInvList();
                    }
                    //InventoryListView.LargeImageList = imageList;



                    // image.Save("C:\\Users\\ncaccamo\\Music\\WINNER" + packet.bid +".bmp");
                    ///SAVE THE PAINTING
                    break;

                case ((int)PlayerInfo.recievedType.loseBid):
                    SetText(Notifications, packet.Username + "Won the bid");
                    Array.Reverse(packet.biddingHistory);
                    SetTextHistory(BiddingHistoryRichText, packet.biddingHistory);
                    if (!gotPayout && packet.Artist == Globals.playerInfo.username)
                    {
                        gotPayout = true;
                        int reducedCut = (int)(packet.bid * 0.8);
                        Globals.playerInfo.money += reducedCut;
                        SetText(MoneyLabel, "Money: " + Globals.playerInfo.money);
                    }


                    UpdateTimer(packet);
                    break;

                case ((int)PlayerInfo.recievedType.time):
                    UpdateTimer(packet);
                    SetText(TitleLabel, packet.Title);
                    SetText(ArtistLabel, "By: " + packet.Artist);
                    SetText(DescriptionLabel, packet.Description);
                    SetText(CurrentHighLabel, packet.Username + " in the lead with a bid of " + packet.bid);
                    Array.Reverse(packet.biddingHistory);
                    SetTextHistory(BiddingHistoryRichText, packet.biddingHistory);
                    Array.Reverse(packet.chatHistory);
                    SetTextHistory(ChatHistoryRichText, packet.chatHistory);
                    break;

                case ((int)PlayerInfo.recievedType.upChat):
                    Array.Reverse(packet.chatHistory);
                    SetTextHistory(ChatHistoryRichText, packet.chatHistory);
                    break;
            }
        }


        void UpdateTimer(Packet packet)
        {
            auctionTimer = packet.time;
            Debug.WriteLine(auctionTimer);
            TimeSpan t = TimeSpan.FromMilliseconds(auctionTimer);
            //TimerLabel.Text = min + ":" + secs;

            int secs = (t.Seconds >= 0) ? t.Seconds : 0;

            SetText(TimerLabel, "Minutes left: " + t.Minutes + " Seconds left: " + secs);
        }

        void startTimer()
        {
            cTimer = new System.Timers.Timer();
            cTimer.Interval = 250;
            cTimer.Elapsed += FetchServerTime;
            cTimer.AutoReset = true;
            cTimer.Enabled = true;
        }

        void startChatTimer()
        {
            chatTimer = new System.Timers.Timer();
            chatTimer.Interval = 501;
            chatTimer.Elapsed += FetchChatHistory;
            chatTimer.AutoReset = true;
            chatTimer.Enabled = true;
        }

        private void FetchServerTime(Object source, System.Timers.ElapsedEventArgs e)
        {
            Packet packToSend = new Packet();
            if (auctionTimer > 0)
            {
                gotPainting = false;
                gotPayout = false;
                packToSend.Type = (int)Packet.pType.RequestTime;
            } else
            {
                packToSend.Type = (int)Packet.pType.EndBid;
            }
            packToSend.Username = Globals.playerInfo.username;


            client.send(packToSend);
            unpack(client.Recieve());
            
            //chatUpdater();
            if (ComissionBar.Value < ComissionBar.Maximum)
            {
                SetProgressBar(ComissionBar, ComissionBar.Value + 1);
            } else
            {
                EnableComissionButton();
            }

        }

        private void FetchChatHistory(Object source, System.Timers.ElapsedEventArgs e)
        {
            chatUpdater();
        }


        void chatUpdater()
        {
            Packet packToSend = new Packet();
            packToSend.Type = (int)Packet.pType.RequestChat;
            packToSend.Username = Globals.playerInfo.username;
            client.send(packToSend);
            unpack(client.Recieve());
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

        delegate void SetImageListCallback(ListView invList, ImageList imageList, Packet packet, string key, Image image);
        private void SetImageList(ListView invList, ImageList imageList, Packet packet, string key, Image image)
        {
            if (invList.InvokeRequired)
            {
                SetImageListCallback d = new SetImageListCallback(SetImageList);
                this.Invoke(d, new object[] { invList, imageList, packet, key, image });
            }
            else
            {
                imageList.Images.Add(key, image);

                invList.LargeImageList = imageList;
                var listViewItem = invList.Items.Add(packet.Title);
                listViewItem.ImageKey = key;
            }
        }


        delegate void ClearInvListCallback(ListView invList);

        private void ClearInvList(ListView invList)
        {
            if (invList.InvokeRequired)
            {
                ClearInvListCallback d = new ClearInvListCallback(ClearInvList);
                this.Invoke(d, new object[] { invList });
            }
            else
            {
                invList.Clear();
            }
        }


        void UpdateInvList()
        {
            ClearInvList(InventoryListView);
            var imageList = new ImageList();

            for (int i = 0; i < invPics.Count; i++)
            {
                imageList.ImageSize = new Size(128, 128);
                ImageConverter convertData = new ImageConverter();
                Image image = (Image)convertData.ConvertFrom(invPics[i].Painting);
                string iKey = invPics[i].Title + i;


                SetImageList(InventoryListView, imageList, invPics[i], iKey, image);
            }
        }

        delegate void SetTextHistoryCallback(RichTextBox listView, string[] text);

        private void SetTextHistory(RichTextBox listView, string[] text)
        {
            if (listView.InvokeRequired)
            {
                SetTextHistoryCallback d = new SetTextHistoryCallback(SetTextHistory);
                this.Invoke(d, new object[] { listView, text });
            }
            else
            {
                listView.Text = (string.Join(Environment.NewLine, text));
            }
        }

        delegate void SetProgressBarCallback(ProgressBar pb, int val);

        private void SetProgressBar(ProgressBar pb, int val)
        {
            if (pb.InvokeRequired)
            {
                SetProgressBarCallback d = new SetProgressBarCallback(SetProgressBar);
                this.Invoke(d, new object[] { pb, val });
            }
            else
            {
                pb.Value = val;
            }
        }

        delegate void EnableComissionButtonCallback();
        private void EnableComissionButton()
        {
            if (ComissionButton.InvokeRequired)
            {
                EnableComissionButtonCallback d = new EnableComissionButtonCallback(EnableComissionButton);
                this.Invoke(d, new object[] {  });
            }
            else
            {
                ComissionButton.Enabled = true;
            }
        }


        private void BidEntry_ValueChanged(object sender, EventArgs e)
        {

        }

        private void Auction_Load(object sender, EventArgs e)
        {
            //InventoryListView.View = View.LargeIcon;
        }


        /*
        private void Auction_FormClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Application.Exit();
        }
        */

        private void Notifications_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void MoneyLabel_Click(object sender, EventArgs e)
        {

        }

        private void InventoryListView_MouseClick(object sender, MouseEventArgs e)
        {

        }

        private void InventoryListView_DoubleClick(object sender, EventArgs e)
        {
            if (Globals.canOpenDisplay)
            {
                int i = InventoryListView.FocusedItem.Index;

                Packet thisPainting = invPics[i];

                ImageConverter convertData = new ImageConverter();
                Image image = (Image)convertData.ConvertFrom(thisPainting.Painting);


                displayWindow = new InvDisplay(image, thisPainting.Title, thisPainting.Username, thisPainting.Description);
                displayWindow.Location = this.Location;
                displayWindow.StartPosition = FormStartPosition.Manual;
                displayWindow.FormClosing += delegate { this.Show(); };
                displayWindow.Show();
                Globals.canOpenDisplay = false;
            }
        }

        private void SendButton_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(MessageBox.Text))
            {
                string userMessage = MessageBox.Text;
                Packet packetToSend = new Packet();
                packetToSend.Type = (int)Packet.pType.SendChat;
                packetToSend.Username = Globals.playerInfo.username;
                packetToSend.Message = userMessage;

                MessageBox.Clear();

                client.send(packetToSend);
                unpack(client.Recieve());
            }
        }

        private void Auction_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void ComissionButton_Click(object sender, EventArgs e)
        {
            ComissionButton.Enabled = false;
            ComissionBar.Value = 0;
            var frm = new Draw(true);
            frm.Location = this.Location;
            frm.StartPosition = FormStartPosition.Manual;
            frm.FormClosing += delegate { this.Show(); };
            frm.Show();
        }
    }
}
