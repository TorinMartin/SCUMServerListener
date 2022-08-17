using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SCUMServerListener
{
    public partial class SettingsForm : Form
    {
        bool disableBackground = false;
        bool overlayAll = false;
        int x = 20;
        int y = 20;
        bool showName, showPlayers, showTime, showPing;
        string serverOfflineColor, serverOnlineColor, backgroundColor;
        IDictionary<string, string> settings = new Dictionary<string, string>();

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
            settings = SettingsManager.LoadAllSettings();

            if (ol is null)
            {
                tb_po1.Text = settings["posx"];
                tb_po2.Text = settings["posy"];
            }
            else
            {
                tb_po1.Text = this.x.ToString();
                tb_po2.Text = this.y.ToString();
            }

            this.x = int.Parse(settings["posx"]);
            this.y = int.Parse(settings["posy"]);

            overlayAll = bool.Parse(settings["overlayAllWindows"]);
            disableBackground = bool.Parse(settings["disableBackground"]);

            showName = bool.Parse(settings["showName"]);
            showPlayers = bool.Parse(settings["showPlayers"]);
            showTime = bool.Parse(settings["showTime"]);
            showPing = bool.Parse(settings["showPing"]);

            cb_name.Checked = showName;
            cb_players.Checked = showPlayers;
            cb_time.Checked = showTime;
            cb_ping.Checked = showPing;
            cb_allwindows.Checked = overlayAll;
            cb_background.Checked = disableBackground;

            allwindows_tooltip.ShowAlways = true;
            allwindows_tooltip.SetToolTip(this.cb_allwindows, "Show overlay over all active windows with or without game running. Restart overlay to take effect");

            cb_bgColor.SelectedItem = FormatString(settings["bgColor"]);
            cb_serveroffline.SelectedItem = FormatString(settings["offlineColor"]);
            cb_serveronline.SelectedItem = FormatString(settings["onlineColor"]);
        }

        private string FormatString(string value)
        {
            if (string.IsNullOrEmpty(value)) return string.Empty;

            return value[0].ToString().ToUpper() + value.Substring(1).ToLower();
        }

        private void btn_save_Click(object sender, EventArgs e)
        {

            disableBackground = cb_background.Checked;
            overlayAll = cb_allwindows.Checked;
            showName = cb_name.Checked;
            showPlayers = cb_players.Checked;
            showTime = cb_time.Checked;
            showPing = cb_ping.Checked;
            serverOnlineColor = cb_serveronline.Text;
            serverOfflineColor = cb_serveroffline.Text;
            backgroundColor = cb_bgColor.Text;

            if (!int.TryParse(tb_po1.Text, out x) || !int.TryParse(tb_po2.Text, out y))
            {
                MessageBox.Show("Position Must Be Numeric!");
            }

            if (ol != null)
            {
                ol.disableBackground = disableBackground;
                ol.X = x;
                ol.Y = y;
                ol.showName = showName;
                ol.showPlayers = showPlayers;
                ol.showTime = showTime;
                ol.showPing = showPing;
                ol.onlineColor = cb_serveronline.Text.ToLower();
                ol.offlineColor = cb_serveroffline.Text.ToLower();
                ol.bgColor = cb_bgColor.Text.ToLower();
            }
            SettingsManager.SaveAllSettings(overlayAll, disableBackground, x, y, showName, showPlayers, showTime, showPing, serverOnlineColor, serverOfflineColor, backgroundColor);
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
