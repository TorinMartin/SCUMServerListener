using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SCUMServerListener
{
    public partial class SettingsForm : Form
    {
        bool disableBackground = false;
        bool overlayAll = false;
        int x = 20;
        int y = 20;
        Overlay ol = null;
        public SettingsForm(Overlay ol)
        {
            InitializeComponent();
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.ol = ol;
            if(ol != null)
            {
                this.x = ol.X;
                this.y = ol.Y;
            }
            int[] pos = SettingsManager.LoadPositions();
            tb_po1.Text = pos[0].ToString();
            tb_po2.Text = pos[1].ToString();
            this.x = pos[0];
            this.y = pos[1];
            overlayAll = SettingsManager.LoadWindowPref();
            disableBackground = SettingsManager.LoadTextPref();
            cb_allwindows.Checked = overlayAll;
            cb_background.Checked = disableBackground;
            toolTip1.ShowAlways = true;
            toolTip1.SetToolTip(this.cb_allwindows, "Show overlay over all active windows with or without game running. Restart overlay to take effect");
        }

        private void btn_save_Click(object sender, EventArgs e)
        {

            bool disableBG = cb_background.Checked;
            bool allWindows = cb_allwindows.Checked;

            if (!int.TryParse(tb_po1.Text, out x) || !int.TryParse(tb_po2.Text, out y))
            {
                MessageBox.Show("Position Must Be Numeric!");
            }

            if (ol != null)
            {
                ol.disableBackground = disableBG;
                ol.X = x;
                ol.Y = y;
            }
            SettingsManager.SaveAllSettings(allWindows, disableBG, x, y);
            MessageBox.Show("Settings Saved!", "Saved!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }

        private void btn_up_Click(object sender, EventArgs e)
        {
            y -= 10;
            tb_po2.Text = y.ToString();
            if (ol != null)
                ol.Y = y;
        }

        private void btn_right_Click(object sender, EventArgs e)
        {
            x += 10;
            tb_po1.Text = x.ToString();
            if (ol != null)
                ol.X = x;
        }

        private void btn_down_Click(object sender, EventArgs e)
        {
            y += 10;
            tb_po2.Text = y.ToString();
            if (ol != null)
                ol.Y = y;
        }

        private void btn_left_Click(object sender, EventArgs e)
        {
            x -= 10;
            tb_po1.Text = x.ToString();
            if (ol != null)
                ol.X = x;
        }
    }
}
