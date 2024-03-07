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
        
        private int _counter;
        private bool _overlayEnabled;
        private Server? _server;
        private SettingsForm? _settingsForm;
        private Thread? _overlayThread;
        private Overlay.Overlay? _overlay;
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

        private async Task StartOverlayAsync()
        {
            if (_overlay is not null)
            {
                if (_overlayThread?.IsAlive is true)
                    StopOverlay();
                else
                    _overlay.Dispose();
            }

            _overlay = new Overlay.Overlay(this);

            _overlayThread = new Thread(_overlay.Run) { IsBackground = true };
            _overlayThread.Start();

            if (!_overlay.IsCreated)
            {
                StopOverlay();
                return;
            }

            _overlayEnabled = true;
            btn_overlay.Text = "Disable Overlay";
            btn_drag_overlay.Show();
            await FetchAndUpdateAsync();
        }

        private void StopOverlay()
        {
            _overlay?.Dispose();
            _overlayThread?.Join();
            _overlayEnabled = false;
            _overlay = null;
            btn_overlay.Text = "Enable Overlay";
            btn_drag_overlay.Hide();
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

        private async Task SearchAsync()
        {
            var servers = await ApiUtil.GetServersAsync(searchbox.Text);
            if (!servers.Any())
            {
                MessageBox.Show("End of Results", "Search Results", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            _server = await IterateResultsAsync(servers);

            UpdateTimer_Reset();

            await FetchAndUpdateAsync();
        }

        private void SearchButton_Click(object sender, EventArgs e)
        {
            _ = SearchAsync();
        }

        private void UpdateTimer_Reset()
        {
            update_progbar.Value = 0;
            _counter = 0;
        }

        private async Task UpdateTimerTickAsync()
        {
            if (_server is null) await FetchAndUpdateAsync();
            if (_counter >= 30)
            {
                update_progbar.Value = 0;
                _counter = 0;
                await FetchAndUpdateAsync();
            }
            _counter++;
            update_progbar.Value = _counter;

            if (_overlayEnabled && _overlay is not null)
            {
                if (AppSettings.Instance.OverlayAllWindows) return;
                try
                {
                    _overlay.SetWindowVisibility();
                    if (_overlay.HasProcessExited())
                        StopOverlay();
                }
                catch (System.ComponentModel.Win32Exception ex)
                {
                    StopOverlay();
                    if (ex.Message.Contains("Access is denied"))
                        MessageBox.Show("Please run as administrator in order for overlay to stick to SCUM game window!", "Access Denied!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void UpdateTimer_Tick(object sender, EventArgs e)
        {
            _ = UpdateTimerTickAsync();
        }

        private void SetDft_Btn_Click(object sender, EventArgs e)
        {
            AppSettings.Instance.DefaultServerId = _server?.Data.Id ?? AppSettings.Instance.DefaultServerId;
            if (!Configuration.Save(AppSettings.Instance))
            {
                MessageBox.Show("Unable to save default server!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            MessageBox.Show("Default Server Saved!", "Saved!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }

        private async Task ToggleOverlayAsync()
        {
            if (!_overlayEnabled)
            {
                await StartOverlayAsync();
            }
            else
            {
                StopOverlay();
            }
        }
        private void Btn_Overlay_Click(object sender, EventArgs e)
        {
            _ = ToggleOverlayAsync();
        }

        private void OverlaySettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _settingsForm = new SettingsForm(_overlay ?? new Overlay.Overlay(this));
            _settingsForm.Show();
        }

        private void Btn_Drag_Overlay_Click(object sender, EventArgs e)
        {
            if (_overlay is null || !_overlayEnabled) return;

            Toggle_Overlay_Btn(false);
            _overlay.DragOverlay();
        }

        public void Toggle_Overlay_Btn(bool isDoneDragging) => btn_drag_overlay.Enabled = isDoneDragging;
    }
}
