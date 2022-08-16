using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;
using ServerListener;
using System.Threading;

namespace SCUMServerListener
{
    public partial class GUI : Form
    {
        private string ServerID = "16315624";
        private int counter = 0;
        private System.Windows.Forms.Timer updateTimer;

        private SettingsForm settingsForm;

        private Thread t_overlay;
        private Overlay ol;
        private bool overlayEnabled = false;

        public GUI()
        {
            InitializeComponent();
            this.MaximizeBox = false; 
            this.ServerID = loadDefault();
            CreateTimer();
            counter = 30;
            update_tooltip.ShowAlways = true;
            update_tooltip.SetToolTip(this.update_progbar, "Server Status will update every 30 seconds...");
        }

        private async void StartOverlay()
        {
            ol = new Overlay();
            t_overlay = new Thread(new ThreadStart(ol.Run));
            t_overlay.IsBackground = true;
            t_overlay.Start();
            if (ol.isCreated)
            {
                overlayEnabled = true;
                btn_overlay.Text = "Disable Overlay";
                await Task.Run(() => Update());
            }
            else
            {
                StopOverlay();
            }
        }

        private void StopOverlay()
        {
            ol.Dispose();
            t_overlay.Join();
            btn_overlay.Text = "Enable Overlay";
            overlayEnabled = false;
            ol = null;
        }

        private void CreateTimer()
        {
            updateTimer = new System.Windows.Forms.Timer();
            updateTimer.Tick += new EventHandler(updateTimer_Tick);
            updateTimer.Interval = 1000;
            updateTimer.Start();
            update_progbar.Maximum = 30;
            update_progbar.Value = 0;
        }

        private void Update()
        {
            Dictionary<Data, string>? Results;
            if(!ServerData.RetrieveData(this.ServerID, out Results) || Results is null || Results.Count == 0)
            {
                MessageBox.Show("Unable to fetch server data", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Action UpdateUIElements;

            try
            {
                Results.TryGetValue(Data.Ip, out var ip);
                Results.TryGetValue(Data.Port, out var port);
                Results.TryGetValue(Data.Name, out var title);
                Results.TryGetValue(Data.Status, out var serverStatus);
                Results.TryGetValue(Data.Players, out var serverPlayers);
                Results.TryGetValue(Data.MaxPlayers, out var serverMaxPlayers);
                Results.TryGetValue(Data.Time, out var serverTime);

                var color = serverStatus == "online" ? System.Drawing.Color.Green : System.Drawing.Color.Red;

                UpdateUIElements = new Action(() => {
                    name.Text = title;
                    name.ForeColor = color;
                    status.ForeColor = color;
                    players.ForeColor = color;
                    _ = serverStatus == "online" ? status.Text = "Online" : status.Text = "Offline";
                    _ = serverStatus == "online" ? players.Text = $"{serverPlayers} / {serverMaxPlayers}" : players.Text = "0";
                    _ = serverStatus == "online" ? time.Text = serverTime : time.Text = "00:00";
                    _ = serverStatus == "online" ? Ping.Text = ServerData.Ping(ip, 4).ToString() : Ping.Text = String.Empty;
                });

                if (overlayEnabled)
                {
                    ol.Name = title;
                    ol.Status = serverStatus;
                    ol.Players = $"{serverPlayers} / {serverMaxPlayers}";
                    ol.Time = serverTime;
                    ol.Ping = serverStatus == "online" ? ServerData.Ping(ip, 4).ToString() : String.Empty;
                }
            } catch (System.NullReferenceException)
            {
                MessageBox.Show("Unable to fetch server data", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Run on UI thread
            this.BeginInvoke(UpdateUIElements);
            this.BeginInvoke(new Action(() => { searchbutton.Enabled = true; }));
        }

        private string IterateResults(List<Server> servers)
        {
            if (servers is null) return this.ServerID;

            foreach (Server server in servers)
            {
                DialogResult dialogResult = MessageBox.Show(server.Name, "Search Results", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);
                if (dialogResult == DialogResult.Yes)
                {
                    return server.ID;
                }
                else if (dialogResult == DialogResult.Cancel)
                {
                    break;
                }
            }
            MessageBox.Show("End of Results", "Search Results", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return this.ServerID;
        }

        private async void searchbutton_Click(object sender, EventArgs e)
        {
            String SearchInput = searchbox.Text;
            String LookUpString = ServerData.GetLookupString(SearchInput);

            List<Server> servers;

            if(!ServerData.GetServers(LookUpString, out servers))
            {
                MessageBox.Show("End of Results", "Search Results", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            this.ServerID = IterateResults(servers);

            updateTimer_Reset();

            if (ServerID != "scum")
            {
                searchbutton.Enabled = false;
                await Task.Run(() => Update());
            }
        }

        private void updateTimer_Reset()
        {
            update_progbar.Value = 0;
            counter = 0;
        }

        private async void updateTimer_Tick(object sender, EventArgs e)
        {
            if (ol is not null)
            {
                if (!ol.overlayAllWindows)
                {
                    ol.SetWindowVisibility();
                    if (ol.HasProcessExited())
                        StopOverlay();
                }
            }

            if (counter >= 30)
            {
                update_progbar.Value = 0;
                counter = 0;
                await Task.Run(() => Update()); 
            }
            counter++;
            update_progbar.Value = counter;
        }

        private string loadDefault() => SettingsManager.LoadDefault();

        private void setdft_btn_Click(object sender, EventArgs e) => SettingsManager.SetDefault(this.ServerID);

        private void btn_overlay_Click(object sender, EventArgs e)
        {
            if (!overlayEnabled)
            {
                StartOverlay();
            }
            else
            {
                StopOverlay();
            }
        }

        private void overlaySettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            settingsForm = new SettingsForm(ol);
            settingsForm.Show();
        }
    }
}
