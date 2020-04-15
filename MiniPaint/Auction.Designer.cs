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
            this.UserBid = new System.Windows.Forms.TextBox();
            this.PlaceBid = new System.Windows.Forms.Button();
            this.BuyCanvas = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // UserBid
            // 
            this.UserBid.Location = new System.Drawing.Point(219, 300);
            this.UserBid.Name = "UserBid";
            this.UserBid.Size = new System.Drawing.Size(100, 20);
            this.UserBid.TabIndex = 0;
            this.UserBid.TextChanged += new System.EventHandler(this.UserBid_TextChanged);
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
            this.BuyCanvas.Location = new System.Drawing.Point(574, 215);
            this.BuyCanvas.Name = "BuyCanvas";
            this.BuyCanvas.Size = new System.Drawing.Size(146, 23);
            this.BuyCanvas.TabIndex = 2;
            this.BuyCanvas.Text = "Buy Canvas (500 bucks)";
            this.BuyCanvas.UseVisualStyleBackColor = true;
            this.BuyCanvas.Click += new System.EventHandler(this.BuyCanvas_Click);
            // 
            // Auction
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.BuyCanvas);
            this.Controls.Add(this.PlaceBid);
            this.Controls.Add(this.UserBid);
            this.Name = "Auction";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Auction_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox UserBid;
        private System.Windows.Forms.Button PlaceBid;
        private System.Windows.Forms.Button BuyCanvas;
    }
}