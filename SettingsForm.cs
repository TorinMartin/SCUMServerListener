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
                this.x = ol.GetX();
                this.y = ol.GetY();
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
            if (cb_background.Checked)
            {
                SettingsManager.SaveTextPref(true);
                if (ol != null)
                    ol.disableBackground = true;
            }
            else
            {
                SettingsManager.SaveTextPref(false);
                if(ol != null)
                    ol.disableBackground = false;
            }

            if (cb_allwindows.Checked)
            {
                SettingsManager.SaveWindowPref(true);
            }
            else
            {
                SettingsManager.SaveWindowPref(false);
            }

            if (ol != null)
            {
                ol.SetX(x);
                ol.SetY(y);
            }

            if (!int.TryParse(tb_po1.Text, out x) || !int.TryParse(tb_po2.Text, out y))
            {
                MessageBox.Show("Position Must Be Numeric!");
            }
            else
            {
                SettingsManager.SavePositions(x, y);
            }
        }

        private void btn_up_Click(object sender, EventArgs e)
        {
            y -= 10;
            tb_po2.Text = y.ToString();
            if (ol != null)
                ol.SetY(y);
        }

        private void btn_right_Click(object sender, EventArgs e)
        {
            x += 10;
            tb_po1.Text = x.ToString();
            if (ol != null)
                ol.SetX(x);
        }

        private void btn_down_Click(object sender, EventArgs e)
        {
            y += 10;
            tb_po2.Text = y.ToString();
            if (ol != null)
                ol.SetY(y);
        }

        private void btn_left_Click(object sender, EventArgs e)
        {
            x -= 10;
            tb_po1.Text = x.ToString();
            if (ol != null)
                ol.SetX(x);
        }
    }
}
