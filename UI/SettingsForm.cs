using System;
using System.Windows.Forms;

namespace SCUMServerListener
{
    public partial class SettingsForm : Form
    {
        private Overlay ol;

        public SettingsForm(Overlay ol)
        {
            InitializeComponent();
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.ol = ol;

            tb_po1.Text = AppSettings.Instance.PositionX.ToString();
            tb_po2.Text = AppSettings.Instance.PositionY.ToString();

            cb_name.Checked = AppSettings.Instance.ShowName;
            cb_players.Checked = AppSettings.Instance.ShowPlayers;
            cb_time.Checked = AppSettings.Instance.ShowTime;
            cb_ping.Checked = AppSettings.Instance.ShowPing;
            cb_allwindows.Checked = AppSettings.Instance.OverlayAllWindows;
            cb_background.Checked = AppSettings.Instance.DisableBackground;


            allwindows_tooltip.ShowAlways = true;
            allwindows_tooltip.SetToolTip(this.cb_allwindows, "Show overlay over all active windows with or without game running. Restart overlay to take effect");

            cb_bgColor.SelectedItem = FormatString(AppSettings.Instance.BackgroundColor);
            cb_serveroffline.SelectedItem = FormatString(AppSettings.Instance.OfflineColor);
            cb_serveronline.SelectedItem = FormatString(AppSettings.Instance.OnlineColor);
        }

        private string FormatString(string value)
        {
            if (string.IsNullOrEmpty(value)) return string.Empty;

            return value[0].ToString().ToUpper() + value.Substring(1).ToLower();
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(tb_po1.Text, out var x))
            {
                MessageBox.Show("Position X Must Be Numeric!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if(!int.TryParse(tb_po2.Text, out var y))
            {
                MessageBox.Show("Position Y Must Be Numeric!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            AppSettings.Instance.OverlayAllWindows = cb_allwindows.Checked;
            AppSettings.Instance.DisableBackground = cb_background.Checked;
            AppSettings.Instance.PositionX = x;
            AppSettings.Instance.PositionY = y;
            AppSettings.Instance.ShowName = cb_name.Checked;
            AppSettings.Instance.ShowPlayers = cb_players.Checked;
            AppSettings.Instance.ShowTime = cb_time.Checked;
            AppSettings.Instance.ShowPing = cb_ping.Checked;
            AppSettings.Instance.OnlineColor = cb_serveronline.Text.ToLower();
            AppSettings.Instance.OfflineColor = cb_serveroffline.Text.ToLower();
            AppSettings.Instance.BackgroundColor = cb_bgColor.Text.ToLower();

            Configuration.Save(AppSettings.Instance);
            MessageBox.Show("Settings Saved!", "Saved!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }

        private void btn_up_Click(object sender, EventArgs e)
        {
            AppSettings.Instance.PositionY -= 10;
            tb_po2.Text = AppSettings.Instance.PositionY.ToString();
            if (ol != null)
                ol.Y = AppSettings.Instance.PositionY;
        }

        private void btn_right_Click(object sender, EventArgs e)
        {
            AppSettings.Instance.PositionX += 10;
            tb_po1.Text = AppSettings.Instance.PositionX.ToString();
            if (ol != null)
                ol.X = AppSettings.Instance.PositionX;
        }

        private void btn_down_Click(object sender, EventArgs e)
        {
            AppSettings.Instance.PositionY += 10;
            tb_po2.Text = AppSettings.Instance.PositionY.ToString();
            if (ol != null)
                ol.Y = AppSettings.Instance.PositionY;
        }

        private void btn_left_Click(object sender, EventArgs e)
        {
            AppSettings.Instance.PositionX -= 10;
            tb_po1.Text = AppSettings.Instance.PositionX.ToString();
            if (ol != null)
                ol.X = AppSettings.Instance.PositionX;
        }
    }
}
