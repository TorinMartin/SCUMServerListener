using System;
using System.Windows.Forms;

namespace SCUMServerListener.UI
{
    public partial class SettingsForm : Form
    {
        private readonly Overlay.GameOverlay? _overlay;
        private const int OverlayPositionIncrement = 10;

        public SettingsForm(Overlay.GameOverlay? overlay = null)
        {
            InitializeComponent();
            MaximizeBox = false;
            MinimizeBox = false;
            _overlay = overlay;

            tb_po1.Text = AppSettings.Instance.PositionX.ToString();
            tb_po2.Text = AppSettings.Instance.PositionY.ToString();

            cb_name.Checked = AppSettings.Instance.ShowName;
            cb_players.Checked = AppSettings.Instance.ShowPlayers;
            cb_time.Checked = AppSettings.Instance.ShowTime;
            cb_ping.Checked = AppSettings.Instance.ShowPing;
            cb_allwindows.Checked = AppSettings.Instance.OverlayAllWindows;
            cb_background.Checked = AppSettings.Instance.DisableBackground;

            allwindows_tooltip.ShowAlways = true;
            allwindows_tooltip.SetToolTip(cb_allwindows, "Show overlay over all active windows with or without game running. Restart overlay to take effect");

            cb_bgColor.SelectedItem = FormatString(AppSettings.Instance.BackgroundColor);
            cb_serveroffline.SelectedItem = FormatString(AppSettings.Instance.OfflineColor);
            cb_serveronline.SelectedItem = FormatString(AppSettings.Instance.OnlineColor);
        }

        private static string FormatString(string value) => string.IsNullOrEmpty(value) ? string.Empty : $"{value[0].ToString().ToUpper()}{value[1..].ToLower()}";

        private void Btn_Save_Click(object sender, EventArgs e)
        {
            if (int.TryParse(tb_po1.Text, out var x) is false)
            {
                MessageBox.Show("Position X Must Be Numeric!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if(int.TryParse(tb_po2.Text, out var y) is false)
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

        private void Btn_Up_Click(object sender, EventArgs e)
        {
            AppSettings.Instance.PositionY -= OverlayPositionIncrement;
            tb_po2.Text = AppSettings.Instance.PositionY.ToString();
            if (_overlay != null) _overlay.Y = AppSettings.Instance.PositionY;
        }

        private void Btn_Right_Click(object sender, EventArgs e)
        {
            AppSettings.Instance.PositionX += OverlayPositionIncrement;
            tb_po1.Text = AppSettings.Instance.PositionX.ToString();
            if (_overlay != null) _overlay.X = AppSettings.Instance.PositionX;
        }

        private void Btn_Down_Click(object sender, EventArgs e)
        {
            AppSettings.Instance.PositionY += OverlayPositionIncrement;
            tb_po2.Text = AppSettings.Instance.PositionY.ToString();
            if (_overlay != null) _overlay.Y = AppSettings.Instance.PositionY;
        }

        private void Btn_Left_Click(object sender, EventArgs e)
        {
            AppSettings.Instance.PositionX -= OverlayPositionIncrement;
            tb_po1.Text = AppSettings.Instance.PositionX.ToString();
            if (_overlay != null) _overlay.X = AppSettings.Instance.PositionX;
        }
    }
}
