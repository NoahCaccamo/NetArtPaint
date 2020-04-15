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
            ((System.ComponentModel.ISupportInitialize)(this.BidEntry)).BeginInit();
            this.SuspendLayout();
            // 
            // PlaceBid
            // 
            this.PlaceBid.Location = new System.Drawing.Point(232, 326);
            this.PlaceBid.Name = "PlaceBid";
            this.PlaceBid.Size = new System.Drawing.Size(75, 23);
            this.PlaceBid.TabIndex = 1;
            this.PlaceBid.Text = "Place Bid";
            this.PlaceBid.UseVisualStyleBackColor = true;
            this.PlaceBid.Click += new System.EventHandler(this.PlaceBid_Click);
            // 
            // BuyCanvas
            // 
            this.BuyCanvas.Location = new System.Drawing.Point(517, 212);
            this.BuyCanvas.Name = "BuyCanvas";
            this.BuyCanvas.Size = new System.Drawing.Size(147, 23);
            this.BuyCanvas.TabIndex = 2;
            this.BuyCanvas.Text = "Buy Canvas  (500 bucks)";
            this.BuyCanvas.UseVisualStyleBackColor = true;
            this.BuyCanvas.Click += new System.EventHandler(this.BuyCanvas_Click);
            // 
            // BidEntry
            // 
            this.BidEntry.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.BidEntry.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.BidEntry.Location = new System.Drawing.Point(211, 275);
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
            this.BidEntry.Size = new System.Drawing.Size(120, 20);
            this.BidEntry.TabIndex = 3;
            this.BidEntry.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.BidEntry.ValueChanged += new System.EventHandler(this.BidEntry_ValueChanged);
            // 
            // Auction
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.BidEntry);
            this.Controls.Add(this.BuyCanvas);
            this.Controls.Add(this.PlaceBid);
            this.Name = "Auction";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.BidEntry)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button PlaceBid;
        private System.Windows.Forms.Button BuyCanvas;
        private System.Windows.Forms.NumericUpDown BidEntry;
    }
}