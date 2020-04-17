using MiniPaint;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Common
{
    public partial class StartupInfo : Form
    {
        public StartupInfo()
        {
            InitializeComponent();
        }

        private void JoinButton_Click(object sender, EventArgs e)
        {
            Globals.playerInfo.username = UsernameBox.Text;
            Globals.playerInfo.ip = IpBox.Text;

            var AucForm = new Auction();
            AucForm.Show();
            this.Hide();
        }

        private void StartupInfo_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
    }
}
