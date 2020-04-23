namespace MiniPaint
{
    partial class Auction
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.PlaceBid = new System.Windows.Forms.Button();
            this.BuyCanvas = new System.Windows.Forms.Button();
            this.BidEntry = new System.Windows.Forms.NumericUpDown();
            this.Notifications = new System.Windows.Forms.Label();
            this.TimerLabel = new System.Windows.Forms.Label();
            this.TitleLabel = new System.Windows.Forms.Label();
            this.DescriptionLabel = new System.Windows.Forms.Label();
            this.MoneyLabel = new System.Windows.Forms.Label();
            this.CurrentHighLabel = new System.Windows.Forms.Label();
            this.InventoryListView = new System.Windows.Forms.ListView();
            this.ArtistLabel = new System.Windows.Forms.Label();
            this.BiddingHistoryRichText = new System.Windows.Forms.RichTextBox();
            this.ChatHistoryRichText = new System.Windows.Forms.RichTextBox();
            this.SendButton = new System.Windows.Forms.Button();
            this.MessageBox = new System.Windows.Forms.TextBox();
            this.ComissionButton = new System.Windows.Forms.Button();
            this.ComissionBar = new System.Windows.Forms.ProgressBar();
            ((System.ComponentModel.ISupportInitialize)(this.BidEntry)).BeginInit();
            this.SuspendLayout();
            // 
            // PlaceBid
            // 
            this.PlaceBid.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(136)))), ((int)(((byte)(52)))));
            this.PlaceBid.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F);
            this.PlaceBid.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(34)))), ((int)(((byte)(13)))));
            this.PlaceBid.Location = new System.Drawing.Point(460, 568);
            this.PlaceBid.Name = "PlaceBid";
            this.PlaceBid.Size = new System.Drawing.Size(183, 87);
            this.PlaceBid.TabIndex = 1;
            this.PlaceBid.Text = "Place Bid";
            this.PlaceBid.UseVisualStyleBackColor = false;
            this.PlaceBid.Click += new System.EventHandler(this.PlaceBid_Click);
            // 
            // BuyCanvas
            // 
            this.BuyCanvas.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.BuyCanvas.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(34)))), ((int)(((byte)(13)))));
            this.BuyCanvas.Location = new System.Drawing.Point(747, 568);
            this.BuyCanvas.Name = "BuyCanvas";
            this.BuyCanvas.Size = new System.Drawing.Size(128, 87);
            this.BuyCanvas.TabIndex = 2;
            this.BuyCanvas.Text = "Buy Canvas  (30 bucks)";
            this.BuyCanvas.UseVisualStyleBackColor = true;
            this.BuyCanvas.Click += new System.EventHandler(this.BuyCanvas_Click);
            // 
            // BidEntry
            // 
            this.BidEntry.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(191)))), ((int)(((byte)(89)))));
            this.BidEntry.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.BidEntry.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.BidEntry.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(34)))), ((int)(((byte)(13)))));
            this.BidEntry.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.BidEntry.Location = new System.Drawing.Point(460, 533);
            this.BidEntry.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.BidEntry.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.BidEntry.Name = "BidEntry";
            this.BidEntry.Size = new System.Drawing.Size(183, 29);
            this.BidEntry.TabIndex = 3;
            this.BidEntry.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.BidEntry.ValueChanged += new System.EventHandler(this.BidEntry_ValueChanged);
            // 
            // Notifications
            // 
            this.Notifications.AutoSize = true;
            this.Notifications.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(34)))), ((int)(((byte)(13)))));
            this.Notifications.Location = new System.Drawing.Point(646, 29);
            this.Notifications.Name = "Notifications";
            this.Notifications.Size = new System.Drawing.Size(35, 13);
            this.Notifications.TabIndex = 4;
            this.Notifications.Text = "label1";
            this.Notifications.Click += new System.EventHandler(this.Notifications_Click);
            // 
            // TimerLabel
            // 
            this.TimerLabel.AutoSize = true;
            this.TimerLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F);
            this.TimerLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(60)))), ((int)(((byte)(67)))));
            this.TimerLabel.Location = new System.Drawing.Point(22, 12);
            this.TimerLabel.Name = "TimerLabel";
            this.TimerLabel.Size = new System.Drawing.Size(67, 29);
            this.TimerLabel.TabIndex = 5;
            this.TimerLabel.Text = "timer";
            // 
            // TitleLabel
            // 
            this.TitleLabel.AutoSize = true;
            this.TitleLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.TitleLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(34)))), ((int)(((byte)(13)))));
            this.TitleLabel.Location = new System.Drawing.Point(215, 112);
            this.TitleLabel.Name = "TitleLabel";
            this.TitleLabel.Size = new System.Drawing.Size(38, 20);
            this.TitleLabel.TabIndex = 6;
            this.TitleLabel.Text = "Title";
            this.TitleLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.TitleLabel.Click += new System.EventHandler(this.label1_Click);
            // 
            // DescriptionLabel
            // 
            this.DescriptionLabel.AutoSize = true;
            this.DescriptionLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.DescriptionLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(34)))), ((int)(((byte)(13)))));
            this.DescriptionLabel.Location = new System.Drawing.Point(196, 228);
            this.DescriptionLabel.Name = "DescriptionLabel";
            this.DescriptionLabel.Size = new System.Drawing.Size(79, 17);
            this.DescriptionLabel.TabIndex = 7;
            this.DescriptionLabel.Text = "Description";
            // 
            // MoneyLabel
            // 
            this.MoneyLabel.AutoSize = true;
            this.MoneyLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MoneyLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(34)))), ((int)(((byte)(13)))));
            this.MoneyLabel.Location = new System.Drawing.Point(456, 21);
            this.MoneyLabel.Name = "MoneyLabel";
            this.MoneyLabel.Size = new System.Drawing.Size(73, 24);
            this.MoneyLabel.TabIndex = 8;
            this.MoneyLabel.Text = "Money";
            this.MoneyLabel.Click += new System.EventHandler(this.MoneyLabel_Click);
            // 
            // CurrentHighLabel
            // 
            this.CurrentHighLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CurrentHighLabel.AutoSize = true;
            this.CurrentHighLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(34)))), ((int)(((byte)(13)))));
            this.CurrentHighLabel.Location = new System.Drawing.Point(202, 62);
            this.CurrentHighLabel.Name = "CurrentHighLabel";
            this.CurrentHighLabel.Size = new System.Drawing.Size(63, 13);
            this.CurrentHighLabel.TabIndex = 9;
            this.CurrentHighLabel.Text = "CurrentHigh";
            this.CurrentHighLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // InventoryListView
            // 
            this.InventoryListView.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(154)))), ((int)(((byte)(54)))));
            this.InventoryListView.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(34)))), ((int)(((byte)(13)))));
            this.InventoryListView.HideSelection = false;
            this.InventoryListView.Location = new System.Drawing.Point(460, 99);
            this.InventoryListView.MultiSelect = false;
            this.InventoryListView.Name = "InventoryListView";
            this.InventoryListView.Size = new System.Drawing.Size(415, 287);
            this.InventoryListView.TabIndex = 10;
            this.InventoryListView.UseCompatibleStateImageBehavior = false;
            this.InventoryListView.DoubleClick += new System.EventHandler(this.InventoryListView_DoubleClick);
            // 
            // ArtistLabel
            // 
            this.ArtistLabel.AutoSize = true;
            this.ArtistLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(34)))), ((int)(((byte)(13)))));
            this.ArtistLabel.Location = new System.Drawing.Point(204, 184);
            this.ArtistLabel.Name = "ArtistLabel";
            this.ArtistLabel.Size = new System.Drawing.Size(61, 13);
            this.ArtistLabel.TabIndex = 11;
            this.ArtistLabel.Text = "Artist Name";
            this.ArtistLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // BiddingHistoryRichText
            // 
            this.BiddingHistoryRichText.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(191)))), ((int)(((byte)(89)))));
            this.BiddingHistoryRichText.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(34)))), ((int)(((byte)(13)))));
            this.BiddingHistoryRichText.Location = new System.Drawing.Point(460, 392);
            this.BiddingHistoryRichText.Name = "BiddingHistoryRichText";
            this.BiddingHistoryRichText.ReadOnly = true;
            this.BiddingHistoryRichText.Size = new System.Drawing.Size(183, 135);
            this.BiddingHistoryRichText.TabIndex = 13;
            this.BiddingHistoryRichText.Text = "";
            // 
            // ChatHistoryRichText
            // 
            this.ChatHistoryRichText.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(93)))), ((int)(((byte)(70)))));
            this.ChatHistoryRichText.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(34)))), ((int)(((byte)(13)))));
            this.ChatHistoryRichText.Location = new System.Drawing.Point(12, 360);
            this.ChatHistoryRichText.Name = "ChatHistoryRichText";
            this.ChatHistoryRichText.ReadOnly = true;
            this.ChatHistoryRichText.Size = new System.Drawing.Size(440, 295);
            this.ChatHistoryRichText.TabIndex = 14;
            this.ChatHistoryRichText.Text = "";
            // 
            // SendButton
            // 
            this.SendButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(93)))), ((int)(((byte)(70)))));
            this.SendButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(34)))), ((int)(((byte)(13)))));
            this.SendButton.Location = new System.Drawing.Point(377, 674);
            this.SendButton.Name = "SendButton";
            this.SendButton.Size = new System.Drawing.Size(75, 23);
            this.SendButton.TabIndex = 15;
            this.SendButton.Text = "Send";
            this.SendButton.UseVisualStyleBackColor = false;
            this.SendButton.Click += new System.EventHandler(this.SendButton_Click);
            // 
            // MessageBox
            // 
            this.MessageBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(34)))), ((int)(((byte)(13)))));
            this.MessageBox.Location = new System.Drawing.Point(12, 676);
            this.MessageBox.Name = "MessageBox";
            this.MessageBox.Size = new System.Drawing.Size(358, 20);
            this.MessageBox.TabIndex = 16;
            // 
            // ComissionButton
            // 
            this.ComissionButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(82)))), ((int)(((byte)(37)))));
            this.ComissionButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(34)))), ((int)(((byte)(13)))));
            this.ComissionButton.Location = new System.Drawing.Point(649, 392);
            this.ComissionButton.Name = "ComissionButton";
            this.ComissionButton.Size = new System.Drawing.Size(146, 48);
            this.ComissionButton.TabIndex = 17;
            this.ComissionButton.Text = "Start Comission";
            this.ComissionButton.UseVisualStyleBackColor = false;
            this.ComissionButton.Click += new System.EventHandler(this.ComissionButton_Click);
            // 
            // ComissionBar
            // 
            this.ComissionBar.Location = new System.Drawing.Point(649, 448);
            this.ComissionBar.Maximum = 30;
            this.ComissionBar.Name = "ComissionBar";
            this.ComissionBar.Size = new System.Drawing.Size(146, 28);
            this.ComissionBar.TabIndex = 18;
            // 
            // Auction
            // 
            this.AcceptButton = this.SendButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(139)))), ((int)(((byte)(89)))));
            this.ClientSize = new System.Drawing.Size(885, 704);
            this.Controls.Add(this.ComissionBar);
            this.Controls.Add(this.ComissionButton);
            this.Controls.Add(this.MessageBox);
            this.Controls.Add(this.SendButton);
            this.Controls.Add(this.ChatHistoryRichText);
            this.Controls.Add(this.BiddingHistoryRichText);
            this.Controls.Add(this.ArtistLabel);
            this.Controls.Add(this.InventoryListView);
            this.Controls.Add(this.CurrentHighLabel);
            this.Controls.Add(this.MoneyLabel);
            this.Controls.Add(this.DescriptionLabel);
            this.Controls.Add(this.TitleLabel);
            this.Controls.Add(this.TimerLabel);
            this.Controls.Add(this.Notifications);
            this.Controls.Add(this.BidEntry);
            this.Controls.Add(this.BuyCanvas);
            this.Controls.Add(this.PlaceBid);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Auction";
            this.Text = "Form1";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Auction_FormClosed);
            this.Load += new System.EventHandler(this.Auction_Load);
            ((System.ComponentModel.ISupportInitialize)(this.BidEntry)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button PlaceBid;
        private System.Windows.Forms.Button BuyCanvas;
        private System.Windows.Forms.NumericUpDown BidEntry;
        private System.Windows.Forms.Label Notifications;
        private System.Windows.Forms.Label TimerLabel;
        private System.Windows.Forms.Label TitleLabel;
        private System.Windows.Forms.Label DescriptionLabel;
        private System.Windows.Forms.Label MoneyLabel;
        private System.Windows.Forms.Label CurrentHighLabel;
        private System.Windows.Forms.ListView InventoryListView;
        private System.Windows.Forms.Label ArtistLabel;
        private System.Windows.Forms.RichTextBox BiddingHistoryRichText;
        private System.Windows.Forms.RichTextBox ChatHistoryRichText;
        private System.Windows.Forms.Button SendButton;
        private System.Windows.Forms.TextBox MessageBox;
        private System.Windows.Forms.Button ComissionButton;
        private System.Windows.Forms.ProgressBar ComissionBar;
    }
}