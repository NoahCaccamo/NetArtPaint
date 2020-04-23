using Common;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;
using static Common.Packet;

namespace Common
{
    public partial class Draw : Form
    {

        bool isCommission;
        private Random rnd = new Random();
        int numColors;
        int[] colorP = new int[4];
        Color[] colorArray = new Color[4];
        string fullSentance;
        private Client client;

        float totalPercent;

        int jackpot = 1000;
        int payout;
        public Draw(Client client, bool _isCommission)
        {
            InitializeComponent();

            this.client = client;

            myBit = new Bitmap(pnl_Draw.Width, pnl_Draw.Height);
             g = pnl_Draw.CreateGraphics();
            isCommission = _isCommission;
            if (isCommission)
            {
                Save.Visible = false;
                AnalyzeButton.Visible = true;
                CollectMoneyButton.Visible = true;
                ClientRequestLabel.Visible = true;

                Color randomColor1 = Color.FromArgb(rnd.Next(256), rnd.Next(256), rnd.Next(256));
                Color randomColor2 = Color.FromArgb(rnd.Next(256), rnd.Next(256), rnd.Next(256));
                Color randomColor3 = Color.FromArgb(rnd.Next(256), rnd.Next(256), rnd.Next(256));
                Color randomColor4 = Color.FromArgb(rnd.Next(256), rnd.Next(256), rnd.Next(256));

                colorArray[0] = randomColor1;
                colorArray[1] = randomColor2;
                colorArray[2] = randomColor3;
                colorArray[3] = randomColor4;

                Color1Button.BackColor = randomColor1;
                Color2Button.BackColor = randomColor2;
                Color3Button.BackColor = randomColor3;
                Color4Button.BackColor = randomColor4;

                numColors = rnd.Next(2, 5);
                int totalP = 100;

                for (int i = 0; i < numColors; i++)
                {
                    if (totalP <= 0)
                    {
                        numColors--;
                        break;
                    }
                    colorP[i] = rnd.Next(1, totalP);
                    totalP -= colorP[i];

                    if (i != numColors - 1)
                    {
                        fullSentance += colorP[i] + "% colour " + (i + 1) + ", ";
                    } else
                    {
                        fullSentance += ", and " + colorP[i] + "% colour " + (i + 1) + ".";
                    }
                }

                SetText(ClientRequestLabel, fullSentance);
            }
        }
        bool startPaint = false;
        Graphics g;
        Bitmap myBit;
        //nullable int for storing Null value
        int? initX = null;
        int? initY = null;
        bool drawSquare = false;
        bool drawRectangle = false;
        bool drawCircle = false;
        //Event fired when the mouse pointer is moved over the Panel(pnl_Draw).
        private void pnl_Draw_MouseMove(object sender, MouseEventArgs e)
        {
            if(startPaint)
            {
                //Setting the Pen BackColor and line Width
                Pen p = new Pen(btn_PenColor.BackColor,float.Parse(cmb_PenSize.Text));
                //Drawing the line.
                g.DrawLine(p, new Point(initX ?? e.X, initY ?? e.Y), new Point(e.X, e.Y));
                initX = e.X;
                initY = e.Y;
            }
        }
        //Event Fired when the mouse pointer is over Panel and a mouse button is pressed
        private void pnl_Draw_MouseDown(object sender, MouseEventArgs e)
        {
            startPaint = true;
            if (drawSquare)
            {
                //Use Solid Brush for filling the graphic shapes
                SolidBrush sb = new SolidBrush(btn_PenColor.BackColor);
                //setting the width and height same for creating square.
                //Getting the width and Heigt value from Textbox(txt_ShapeSize)
                g.FillRectangle(sb, e.X, e.Y, int.Parse(txt_ShapeSize.Text), int.Parse(txt_ShapeSize.Text));
                //setting startPaint and drawSquare value to false for creating one graphic on one click.
                startPaint = false;
                drawSquare = false;
            }
            if(drawRectangle)
            {
                SolidBrush sb = new SolidBrush(btn_PenColor.BackColor);
                //setting the width twice of the height
                g.FillRectangle(sb, e.X, e.Y, 2*int.Parse(txt_ShapeSize.Text), int.Parse(txt_ShapeSize.Text));
                startPaint = false;
                drawRectangle = false;
            }
            if(drawCircle)
            {
                SolidBrush sb = new SolidBrush(btn_PenColor.BackColor);
                g.FillEllipse(sb, e.X, e.Y, int.Parse(txt_ShapeSize.Text), int.Parse(txt_ShapeSize.Text));
                startPaint = false;
                drawCircle = false;
            }
        }
        //Fired when the mouse pointer is over the pnl_Draw and a mouse button is released.
        private void pnl_Draw_MouseUp(object sender, MouseEventArgs e)
        {
            startPaint = false;
            initX = null;
            initY = null;
        }
        //Button for Setting pen Color
        private void button1_Click(object sender, EventArgs e)
        {
            //Open Color Dialog and Set BackColor of btn_PenColor if user click on OK
            ColorDialog c = new ColorDialog();
            if(c.ShowDialog()==DialogResult.OK)
            {
                btn_PenColor.BackColor = c.Color;
            }
        }
        //New 
        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Clearing the graphics from the Panel(pnl_Draw)
            g.Clear(pnl_Draw.BackColor);
            //Setting the BackColor of pnl_draw and btn_CanvasColor to White on Clicking New under File Menu
            pnl_Draw.BackColor = Color.White;
            btn_CanvasColor.BackColor = Color.White;
        }
       //Setting the Canvas Color
        private void btn_CanvasColor_Click_1(object sender, EventArgs e)
        {
            ColorDialog c = new ColorDialog();
            if(c.ShowDialog()==DialogResult.OK)
            {
                pnl_Draw.BackColor = c.Color;
                btn_CanvasColor.BackColor = c.Color;
            }
        }

        private void btn_Square_Click(object sender, EventArgs e)
        {
            drawSquare = true;
        }

        private void btn_Rectangle_Click(object sender, EventArgs e)
        {
            drawRectangle = true;
        }

        private void btn_Circle_Click(object sender, EventArgs e)
        {
            drawCircle = true;
        }
        //Exit under File Menu
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Do you want to Exit?","Exit",MessageBoxButtons.YesNo,MessageBoxIcon.Information)==DialogResult.Yes)
            {
                Application.Exit();
            }
        }
        //About under Help Menu
        private void aboutMiniPaintToolStripMenuItem_Click(object sender, EventArgs e)
        {
            About a = new About();
            a.ShowDialog();
        }

        private void pnl_Draw_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Save_Click(object sender, EventArgs e)
        {   
            Graphics grap = Graphics.FromImage(myBit);
            Rectangle rect = pnl_Draw.RectangleToScreen(pnl_Draw.ClientRectangle);
            grap.CopyFromScreen(rect.Location, Point.Empty, pnl_Draw.Size);

            if (!isCommission)
            {
                Packet newPacket = new Packet();
                var ms = new MemoryStream();
                myBit.Save(ms, ImageFormat.Jpeg);
                newPacket.Painting = ms.ToArray();

                newPacket.Type = (int)pType.SubmitPainting;
                newPacket.Title = TitleBox.Text;
                newPacket.Description = DescriptionBox.Text;
                newPacket.Username = Globals.playerInfo.username;
                client.Send(newPacket);
                //myBit.Save(@"C:\Users\ncaccamo\Music\test.png", ImageFormat.Png);
                this.Close();
            }
        }

        private int CountPixels(Bitmap bm, Color target_color)
        {
            // Loop through the pixels.
            int matches = 0;
            for (int y = 0; y < bm.Height; y++)
            {
                for (int x = 0; x < bm.Width; x++)
                {
                    if (bm.GetPixel(x, y) == target_color) matches++;
                }
            }
            return matches;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

        private void Draw_Load(object sender, EventArgs e)
        {

        }

        private void Color1Button_Click(object sender, EventArgs e)
        {
            btn_PenColor.BackColor = Color1Button.BackColor;
        }

        private void Color2Button_Click(object sender, EventArgs e)
        {
            btn_PenColor.BackColor = Color2Button.BackColor;
        }

        private void Color3Button_Click(object sender, EventArgs e)
        {
            btn_PenColor.BackColor = Color3Button.BackColor;
        }

        private void Color4Button_Click(object sender, EventArgs e)
        {
            btn_PenColor.BackColor = Color4Button.BackColor;
        }

        private void AnalyzeButton_Click(object sender, EventArgs e)
        {
            AnalyzeButton.Enabled = false;
            CollectMoneyButton.Enabled = true;
            AnalyzeLabel.Visible = true;

            Graphics grap = Graphics.FromImage(myBit);
            Rectangle rect = pnl_Draw.RectangleToScreen(pnl_Draw.ClientRectangle);
            grap.CopyFromScreen(rect.Location, Point.Empty, pnl_Draw.Size);

            int totalPix = myBit.Width * myBit.Height;
            totalPercent = 0;
            for (int i = 0; i < numColors; i++)
            {
                int targetPix = (int)(totalPix * ((float)colorP[i] / 100f));
                int actualPix = CountPixels(myBit, colorArray[i]);

                float percentageRight;
                if (actualPix > targetPix)
                {
                    percentageRight = (((float)actualPix - (float)targetPix) / (float)actualPix) * 100f;
                } else
                {
                    percentageRight = (((float)actualPix - (float)targetPix) / (float)targetPix) * 100f;
                }
                percentageRight = Math.Abs(percentageRight);
                totalPercent += percentageRight;
            }
            totalPercent = totalPercent / numColors;
            totalPercent = 100 - totalPercent;
            payout = (int)(jackpot * (totalPercent/100));
            SetText(AnalyzeLabel, totalPercent + "% correct. You earn $" + payout);
        }


        delegate void SetTextCallback(Label label, string text);
        private void SetText(Label label, string text)
        {
            if (label.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetText);
                this.Invoke(d, new object[] { label, text });
            }
            else
            {
                label.Text = text;
            }
        }

        private void CollectMoneyButton_Click(object sender, EventArgs e)
        {
            Globals.playerInfo.money += payout;
            this.Close();
        }
    }

}
