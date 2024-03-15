using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using SCUMServerListener.API;
using Timer = System.Windows.Forms.Timer;

namespace SCUMServerListener.UI
{
    public partial class Gui : Form
    {
        private const int UpdateIntervalSeconds = 30;
        private bool _overlayEnabled;
        private Server? _server;
        private SettingsForm? _settingsForm;
        private Thread? _overlayThread;
        private Overlay.GameOverlay? _overlay;
        private readonly Timer _updateTimer;
        
        public Gui()
        { 
            InitializeComponent();
            update_progbar.Maximum = UpdateIntervalSeconds;
            update_progbar.Value = 0;
            _overlayEnabled = false;
            _updateTimer = new Timer { Interval = 1000 };
            _updateTimer.Tick += UpdateTimer_Tick;
            _updateTimer.Start();
        }

        private async Task FetchAndUpdateAsync()
        {
            _server = await ApiUtil.QueryServerByIdAsync(_server?.Data.Id ?? AppSettings.Instance.DefaultServerId);
            if (_server is null)
            {
                MessageBox.Show("Unable to fetch server data", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                var isOnline = _server.Data.Attributes.Status == "online";
                var color = isOnline ? System.Drawing.Color.Green : System.Drawing.Color.Red;
                var serverPing = isOnline ? ApiUtil.Ping(_server.Data.Attributes.Ip, 4).ToString(CultureInfo.InvariantCulture) : string.Empty;

                BeginInvoke(() => {
                    name.Text = _server.Data.Attributes.Name;
                    status.Text = isOnline ? "Online" : "Offline";
                    players.Text = isOnline ? $"{_server.Data.Attributes.Players} / {_server.Data.Attributes.MaxPlayers}" : "0";
                    time.Text = isOnline ? _server.Data.Attributes.Details.Time : "00:00";
                    Ping.Text = serverPing;
                    name.ForeColor = color;
                    status.ForeColor = color;
                    players.ForeColor = color;
                    time.ForeColor = color;
                });

                if (!searchbutton.Enabled) searchbutton.Enabled = true;

                if (_overlayEnabled && _overlay is not null)
                {
                    _overlay.Name = _server.Data.Attributes.Name;
                    _overlay.Status = _server.Data.Attributes.Status;
                    _overlay.Players = $"{_server.Data.Attributes.Players} / {_server.Data.Attributes.MaxPlayers}";
                    _overlay.Time = _server.Data.Attributes.Details.Time;
                    _overlay.Ping = serverPing;
                }
            } catch (NullReferenceException)
            {
                MessageBox.Show("There was a problem while retrieving server data!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task<Server?> IterateResultsAsync(List<ServerSearchResult> servers)
        {
            if (servers.Any() is false) return _server;

            foreach (var server in servers)
            {
                var dialogResult = MessageBox.Show(server.Name, "Search Results", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);
                if (dialogResult == DialogResult.Cancel) break;
                
                if (dialogResult == DialogResult.Yes)
                {
                    return await ApiUtil.QueryServerByIdAsync(server.Id);
                }
            }
            MessageBox.Show("End of Results", "Search Results", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return _server;
        }

        private static async Task<List<ServerSearchResult>> SearchAsync(string query) => string.IsNullOrEmpty(query) ? new List<ServerSearchResult>() : await ApiUtil.GetServersAsync(query);

        public void Toggle_Overlay_Btn(bool isDoneDragging) => btn_drag_overlay.Enabled = isDoneDragging;
        
        private async Task ToggleOverlayAsync()
        {
            if (_overlayEnabled is false)
            {
                await StartOverlayAsync();
                return;
            }
            
            StopOverlay();
        }
        
        private async Task StartOverlayAsync()
        {
            if (_overlay is not null)
            {
                if (_overlayThread?.IsAlive is true)
                    StopOverlay();
                else
                    _overlay.Dispose();
            }

            try
            {
                _overlay = new Overlay.GameOverlay(this);
                _overlayThread = new Thread(_overlay.Run) { IsBackground = true };
                _overlayThread.Start();
            }
            catch (Exception ex)
            {
                var message = $"The overlay was unable to start due to an unexpected error: {ex.Message}";
                if (ex is ArgumentOutOfRangeException) message = "SCUM is not running!";
                
                MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _overlayEnabled = true;
            ToggleOverlayBtnText(_overlayEnabled);
            await FetchAndUpdateAsync();
        }

        private void StopOverlay()
        {
            _overlay?.Dispose();
            _overlayThread?.Join();
            _overlayEnabled = false;
            _overlay = null;
            ToggleOverlayBtnText(_overlayEnabled);
        }

        private void ToggleOverlayBtnText(bool isEnabled)
        {
            if (isEnabled) btn_drag_overlay.Show();
            else btn_drag_overlay.Hide();
            
            var btnText = isEnabled ? "Disable Overlay" : "Enable Overlay";
            btn_overlay.Text = btnText;
        }
    }
}
