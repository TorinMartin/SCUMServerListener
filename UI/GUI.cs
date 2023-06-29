using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using Timer = System.Windows.Forms.Timer;
using System.Linq;

namespace SCUMServerListener
{
    public partial class GUI : Form
    {
        private const int UPDATE_EVERY_SECONDS = 30;

        private Server _server = null;
        private int _counter;
        private bool _overlayEnabled;
        private Timer _updateTimer;
        private SettingsForm _settingsForm;
        private Thread _overlayThread;
        private Overlay _overlay;

        public GUI()
        { 
            InitializeComponent();
            update_progbar.Maximum = UPDATE_EVERY_SECONDS;
            update_progbar.Value = 0;
            CreateTimer();
            Task.Run(() => Update());
            _overlayEnabled = false;
        }

        private async Task StartOverlay()
        {
            if (_overlay is not null)
            {
                if (_overlayThread.IsAlive)
                    StopOverlay();
                else
                    _overlay.Dispose();
            }

            _overlay = new Overlay(this);

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
            await Update();
        }

        private void StopOverlay()
        {
            _overlay.Dispose();
            _overlayThread.Join();
            _overlayEnabled = false;
            _overlay = null;
            btn_overlay.Text = "Enable Overlay";
            btn_drag_overlay.Hide();
        }

        private void CreateTimer()
        {
            _updateTimer = new Timer() { Interval = 1000 };
            _updateTimer.Tick += new EventHandler(updateTimer_Tick);
            _updateTimer.Start();
        }

        private async Task Update()
        {
            var server = await ServerData.RetrieveData(_server);
            if(server is null)
            {
                MessageBox.Show("Unable to fetch server data", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            _server = server;

            Action UpdateUIElements;

            try
            {
                var isOnline = _server.Status == "online";
                var color = isOnline ? System.Drawing.Color.Green : System.Drawing.Color.Red;
                var serverPing = isOnline ? ServerData.Ping(_server.Ip, 4).ToString() : string.Empty;

                UpdateUIElements = new Action(() => {
                    name.Text = _server.Name;
                    status.Text = isOnline ? "Online" : "Offline";
                    players.Text = isOnline ? $"{_server.Players} / {_server.MaxPlayers}" : "0";
                    time.Text = isOnline ? _server.Time : "00:00";
                    Ping.Text = serverPing;
                    name.ForeColor = color;
                    status.ForeColor = color;
                    players.ForeColor = color;
                    time.ForeColor = color;
                });

                if (!searchbutton.Enabled) searchbutton.Enabled = true;

                if (_overlayEnabled)
                {
                    _overlay.Name = _server.Name;
                    _overlay.Status = _server.Status;
                    _overlay.Players = $"{_server.Players} / {_server.MaxPlayers}";
                    _overlay.Time = _server.Time;
                    _overlay.Ping = serverPing;
                }
            } catch (NullReferenceException)
            {
                MessageBox.Show("There was a problem while retrieving server data!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Run on UI thread
            this.BeginInvoke(UpdateUIElements);
        }

        private Server IterateResults(IEnumerable<dynamic> servers)
        {
            if (servers is null) return _server;

            foreach (var server in servers)
            {
                DialogResult dialogResult = MessageBox.Show(server.Name, "Search Results", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);
                if (dialogResult == DialogResult.Yes)
                {
                    return server;
                }
                else if (dialogResult == DialogResult.Cancel)
                {
                    break;
                }
            }
            MessageBox.Show("End of Results", "Search Results", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return _server;
        }

        private async Task SearchAsync()
        {
            var searchInput = searchbox.Text;
            var lookUpString = ServerData.GetLookupString(searchInput);

            var servers = await ServerData.GetServers(lookUpString);
            if (!servers.Any())
            {
                MessageBox.Show("End of Results", "Search Results", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            _server = IterateResults(servers);

            updateTimer_Reset();

            await Update();
        }

        private void searchbutton_Click(object sender, EventArgs e)
        {
            _ = SearchAsync();
        }

        private void updateTimer_Reset()
        {
            update_progbar.Value = 0;
            _counter = 0;
        }

        private async Task updateTimerTickAsync()
        {
            if (_counter >= 30)
            {
                update_progbar.Value = 0;
                _counter = 0;
                await Update();
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

        private void updateTimer_Tick(object sender, EventArgs e)
        {
            _ = updateTimerTickAsync();
        }

        private void setdft_btn_Click(object sender, EventArgs e)
        {
            AppSettings.Instance.DefaultServerId = _server.ID;
            if (!Configuration.Save(AppSettings.Instance))
            {
                MessageBox.Show("Unable to save default server!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            MessageBox.Show("Default Server Saved!", "Saved!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }

        private async Task toggleOverlayAsync()
        {
            if (!_overlayEnabled)
            {
                await StartOverlay();
            }
            else
            {
                StopOverlay();
            }
        }
        private void btn_overlay_Click(object sender, EventArgs e)
        {
            _ = toggleOverlayAsync();
        }

        private void overlaySettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _settingsForm = new SettingsForm(_overlay);
            _settingsForm.Show();
        }

        private void btn_drag_overlay_Click(object sender, EventArgs e)
        {
            if (_overlay is null || !_overlayEnabled) return;

            toggle_overlay_btn(false);
            _overlay.DragOverlay();
        }

        public void toggle_overlay_btn(bool isDoneDragging) => btn_drag_overlay.Enabled = isDoneDragging;

        public void InvokeOnUIThread(Action del) => this.BeginInvoke(del);

    }
}
