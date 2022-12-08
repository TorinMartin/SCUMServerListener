using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SCUMServerListener
{
    public partial class SettingsForm : Form
    {
        private bool disableBackground = false;
        private bool overlayAll = false;
        private int x = 20;
        private int y = 20;
        private bool showName, showPlayers, showTime, showPing;
        private string serverOfflineColor, serverOnlineColor, backgroundColor;

        private Overlay ol;
        private GUI gui;

        public SettingsForm(Overlay ol, GUI gui)
        {
            InitializeComponent();
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.ol = ol;
            this.gui = gui;
            if(ol != null)
            {
                this.x = ol.X;
                this.y = ol.Y;
            }
            //settings = SettingsManager.LoadAllSettings();

            if (ol is null)
            {
                tb_po1.Text = gui.appSettings.PositionX.ToString();
                tb_po2.Text = gui.appSettings.PositionY.ToString();
            }
            else
            {
                tb_po1.Text = this.x.ToString();
                tb_po2.Text = this.y.ToString();
            }

            this.x = gui.appSettings.PositionX;
            this.y = gui.appSettings.PositionY;

            overlayAll = gui.appSettings.OverlayAllWindows;
            disableBackground = gui.appSettings.DisableBackground;

            showName = gui.appSettings.ShowName;
            showPlayers = gui.appSettings.ShowPlayers;
            showTime = gui.appSettings.ShowTime;
            showPing = gui.appSettings.ShowPing;

            cb_name.Checked = showName;
            cb_players.Checked = showPlayers;
            cb_time.Checked = showTime;
            cb_ping.Checked = showPing;
            cb_allwindows.Checked = overlayAll;
            cb_background.Checked = disableBackground;

            allwindows_tooltip.ShowAlways = true;
            allwindows_tooltip.SetToolTip(this.cb_allwindows, "Show overlay over all active windows with or without game running. Restart overlay to take effect");

            cb_bgColor.SelectedItem = FormatString(gui.appSettings.BackgroundColor);
            cb_serveroffline.SelectedItem = FormatString(gui.appSettings.OfflineColor);
            cb_serveronline.SelectedItem = FormatString(gui.appSettings.OnlineColor);
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

            gui.appSettings.OverlayAllWindows = overlayAll;
            gui.appSettings.DisableBackground = disableBackground;
            gui.appSettings.PositionX = x;
            gui.appSettings.PositionY = y;
            gui.appSettings.ShowName = showName;
            gui.appSettings.ShowPlayers = showPlayers;
            gui.appSettings.ShowTime = showTime;
            gui.appSettings.ShowPing = showPing;
            gui.appSettings.OnlineColor = cb_serveronline.Text.ToLower();
            gui.appSettings.OfflineColor = cb_serveroffline.Text.ToLower();
            gui.appSettings.BackgroundColor = cb_bgColor.Text.ToLower();

            Configuration.Save(gui.appSettings);
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
