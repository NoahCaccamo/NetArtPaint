using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Common;

namespace MiniPaint
{
    public partial class InvDisplay : Form
    {
        Image painting;
        string title, artist, description;
        int cost;

        public InvDisplay(Image _painting, string _title, string _artist, string _description, int _cost)
        {
            InitializeComponent();
            painting = _painting;
            title = _title;
            artist = _artist;
            description = _description;
            cost = _cost;

            SetPic(PaintingBox, painting);
            SetText(TitleLabel, title);
            SetText(ArtistLabel, artist);
            SetText(DescriptionLabel, description);
            SetText(ExtraInfoLabel, "This painting was bought for $" + cost);
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {

            Thread th = new Thread((ThreadStart)(() => {
                SaveFileDialog dialog = new SaveFileDialog();
                dialog.Filter = "JPeg Image|*.jpg|Bitmap Image|*.bmp|Gif Image|*.gif";
                dialog.Title = "Save an Image File";
                dialog.FileName = title;
                dialog.ShowDialog();

                // If the file name is not an empty string open it for saving.
                if (dialog.FileName != "")
                {
                    // Saves the Image via a FileStream created by the OpenFile method.
                    System.IO.FileStream fs =
                        (System.IO.FileStream)dialog.OpenFile();
                    switch (dialog.FilterIndex)
                    {
                        case 1:
                            painting.Save(fs,
                              System.Drawing.Imaging.ImageFormat.Jpeg);
                            break;

                        case 2:
                            painting.Save(fs,
                              System.Drawing.Imaging.ImageFormat.Bmp);
                            break;

                        case 3:
                            painting.Save(fs,
                              System.Drawing.Imaging.ImageFormat.Gif);
                            break;
                    }

                    fs.Close();
                }
            }));

            th.SetApartmentState(ApartmentState.STA);
            th.Start();
            th.Join();
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

        private void InvDisplay_FormClosed(object sender, FormClosedEventArgs e)
        {
            Globals.canOpenDisplay = true;
        }

        delegate void SetPicCallback(PictureBox pictureBox, Image image);

        private void SetPic(PictureBox pictureBox, Image image)
        {
            if (pictureBox.InvokeRequired)
            {
                SetPicCallback d = new SetPicCallback(SetPic);
                this.Invoke(d, new object[] { pictureBox, image });
            }
            else
            {
                pictureBox.Image = image;
            }
        }


    }
}
